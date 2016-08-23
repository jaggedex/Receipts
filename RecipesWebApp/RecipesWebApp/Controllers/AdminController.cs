using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RecipesWebApp.Extensions;
using RecipesWebApp.Models;
using RecipesWebData;
using RecipeWebData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipesWebApp.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            var allRecipes = this.db.RecipesConfirm.ToList();

            return View(allRecipes);
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

            return View(editedRecipe);
        }

        [HttpPost]
        public ActionResult Confirm(int id, RecipeViewModel model)
        {
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
                foreach (var pr in model.Products)
                {
                    var product = this.db.Products.Find(int.Parse(pr.Value));
                    oldProducts.Add(product);
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
                    Products = oldProducts
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
            var userID = this.db.Users.Where(x => x.UserName == UserSearched).FirstOrDefault();
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
                        this.AddNotification("Потребителя вече е с избраната роля.", NotificationType.WARNING);
                    }
                    userID.Roles.Remove(userID.Roles.Where(x => x.RoleId == userRole.Id).FirstOrDefault());
                }
                else
                {
                    Redirect("/Admin/Index");
                    this.AddNotification("Потребителя не съществува!", NotificationType.ERROR);
                }
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
                this.AddNotification("Потребителя вече е с избраната роля.", NotificationType.WARNING);

            }
        }

    }
}


