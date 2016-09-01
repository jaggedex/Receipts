using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using RecipesWebApp.Extensions;
using RecipesWebApp.Models;
using RecipesWebData;
using RecipeWebData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipesWebApp.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index(int? page)
        {
            var allRecipes = this.db.RecipesConfirm.ToList().ToPagedList(page ?? 1, 5);
            var AllUsers = this.db.Users.Include(x => x.Roles);
            var adminRoleId = this.db.Roles.Where(x => x.Name == "Administrators").FirstOrDefault();
            var administrators = new List<string>();
            foreach (var admin in AllUsers)
            {
                foreach (var role in admin.Roles)
                {
                    if (role.RoleId == adminRoleId.Id )
                    {
                        administrators.Add(admin.UserName);
                    }
                }
            }            

            ViewBag.Administrators = administrators;
            return View(allRecipes);
        }

        public ActionResult DeleteProduct()
        {
            var products = this.db.Products;
            
            return View(products);
        }
        [Authorize(Roles = "Administrators, MasterAdministrators")]
        public ActionResult DeleteSelectedProduct(int productId)
        {
           
            var productToDelete = this.db.Products.Where(x => x.ID == productId).FirstOrDefault();
            if (productToDelete != null)
            {
                this.db.Products.Remove(productToDelete);
                this.AddNotification("Продукта е изтрит успешно.", NotificationType.SUCCESS);
                db.SaveChanges();
            }
            
            return Redirect("/Admin/DeleteProduct");
        }
        public ActionResult Confirm(int id)
        {
            var recipe = this.db.RecipesConfirm.Find(id);
            var editedRecipe = new RecipeViewModel()
            {
                Title = recipe.Title,
                Description = recipe.Description,
                Type = recipe.Type,
                AuthorId = recipe.AuthorId,
                Date = recipe.Date,
                ID = recipe.ID,
                Products = recipe.Products.Select(y => new SelectListItem()
                {
                    Text = y.ProductName,
                    Value = y.ID.ToString()
                }).ToList(),
                ProductsConfirm = recipe.ProductsConfirm.Select(y => new SelectListItem()
                {
                    Text = y.ProductName,
                    Value = y.ID.ToString()
                }).ToList(),
                Image = recipe.Image
            };
            ViewBag.Image = recipe.Image;
            return View(editedRecipe);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Confirm(int id, RecipeViewModel model, string ChoosenFile)
        {
            byte[] image = null;
            if (ChoosenFile != null)
            {
                var converted = ChoosenFile.Substring(ChoosenFile.IndexOf(',') + 1);
                image = Convert.FromBase64String(converted);
            }


            if (IsAdmin())
            {
                var recipeToRemove = this.db.RecipesConfirm.Find(id);
                var productsToBeDeleted = new List<ProductsConfirm>();
                if (model.ProductsConfirm != null)
                {
                    foreach (var product in model.ProductsConfirm)
                    {
                        var newProduct = new Product() { ProductName = product.Text };
                        bool alreadyContainsInDb = false;
                        foreach (var productInDb in this.db.Products)
                        {
                            if (product.Text == productInDb.ProductName)
                            {
                                alreadyContainsInDb = true;
                                break;
                            }
                        }
                        if (!alreadyContainsInDb)
                        {
                            this.db.Products.Add(newProduct);
                        }

                        var productToDelete = this.db.ProductsConfirm.Where(x => x.ID.ToString() == product.Value).FirstOrDefault();
                        productsToBeDeleted.Add(productToDelete);

                    }
                }
                this.db.SaveChanges();
                var newAddedProducts = new List<Product>();
                var oldProducts = new List<Product>();
                if (model.Products != null)
                {
                    foreach (var pr in model.Products)
                    {
                        var product = this.db.Products.Find(int.Parse(pr.Value));
                        oldProducts.Add(product);
                    }
                }

                if (model.ProductsConfirm != null)
                {
                    foreach (var product in model.ProductsConfirm)
                    {
                        var pr = this.db.Products.Where(x => x.ProductName == product.Text).FirstOrDefault();
                        newAddedProducts.Add(pr);
                    }
                }
                var newRecipe = new Recipe()
                {
                    Title = model.Title,
                    Description = model.Description,
                    AuthorId = model.AuthorId,
                    Type = model.Type,
                    Products = oldProducts,
                    Image = image
                };
                foreach (var item in newAddedProducts)
                {
                    newRecipe.Products.Add(item);
                }
                this.db.Recipes.Add(newRecipe);
                this.db.SaveChanges();
                foreach (var pr in productsToBeDeleted)
                {
                    this.db.ProductsConfirm.Remove(pr);
                }
                this.db.SaveChanges();
                this.db.RecipesConfirm.Remove(recipeToRemove);
                this.db.SaveChanges();
            }
            this.AddNotification("Рецептата е одобрена.", NotificationType.SUCCESS);
            return Redirect("/Admin/Index");
            
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var recipeToDelete = this.db.RecipesConfirm.Find(id);

            if (recipeToDelete == null)
            {
                return HttpNotFound();
            }

            if (IsAdmin())
            {
                this.db.RecipesConfirm.Remove(recipeToDelete);
                this.db.SaveChanges();
                this.AddNotification("Рецептата не беше одобрена.", NotificationType.SUCCESS);
            }
           
            return Redirect("/Admin/Index");
            
        }

        public ActionResult ChangeRoles()
        {
            return PartialView("_ChangeUsersRoles");
        }
        [HttpPost]
        public ActionResult ChangeRoles(string UserSearched, string RoleChange)
        {
            var userRoleMaster = this.db.Roles.Where(x => x.Name == "MasterAdministrators").FirstOrDefault();
            var userID = this.db.Users.Where(x => x.UserName == UserSearched).FirstOrDefault();
            if (userID != null && !userID.Roles.Contains(userID.Roles.Where(x => x.RoleId == userRoleMaster.Id).FirstOrDefault()))
            {
                
                if (RoleChange == "Administrators")
                {
                    if (userID != null)
                    {
                        var context = new ApplicationDbContext();
                        AddUserToRole(context, UserSearched, RoleChange);

                    }
                    else
                    {
                        Redirect("/Admin/Index");
                        this.AddNotification("Потребителя не съществува!", NotificationType.ERROR);
                    }
                }
                else
                {

                    if (userID != null)
                    {
                        var userRole = this.db.Roles.Where(x => x.Name == "Administrators").FirstOrDefault();

                        if (userID.Roles.Contains(userID.Roles.Where(x => x.RoleId == userRole.Id).FirstOrDefault()))
                        {
                            userID.Roles.Remove(userID.Roles.Where(x => x.RoleId == userRole.Id).FirstOrDefault());
                            this.AddNotification("Статуса е променен.", NotificationType.SUCCESS);
                        }
                        else
                        {
                            this.AddNotification("Статуса не е променен.", NotificationType.WARNING);
                        }

                    }
                    else
                    {
                        Redirect("/Admin/Index");
                        this.AddNotification("Потребителя не съществува!", NotificationType.ERROR);
                    }
                }
            }
            else
            {
                this.AddNotification("Статуса не е променен.", NotificationType.WARNING);
            }
            this.db.SaveChanges();
            return Redirect("/Admin/Index");
        }



        private void AddUserToRole(ApplicationDbContext context, string userName, string roleName)
        {
            var user = context.Users.First(u => u.UserName == userName);
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var addAdminRoleResult = userManager.AddToRole(user.Id, roleName);
            if (!addAdminRoleResult.Succeeded)
            {

                Redirect("/Admin/Index");
                this.AddNotification("Статуса не е променен.", NotificationType.WARNING);
            }
            else
            {
                this.AddNotification("Статуса е променен.", NotificationType.SUCCESS);
            }

        }

    }
}


