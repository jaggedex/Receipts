using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecipesWebApp.Models;
using System.Collections;
using RecipesWebData;
using Microsoft.AspNet.Identity;
using System.Net;
using RecipesWebApp.Extensions;
using RecipeWebData;
using PagedList;
using PagedList.Mvc;

namespace RecipesWebApp.Controllers
{
    public class RecipeController : BaseController
    {
        // GET: Recipe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListAppetizers(int? page)
        {
            var appetizers = this.db.Recipes.Where(a => a.Type == "Предястие").OrderByDescending(x => x.Date).ToList().ToPagedList(page ?? 1, 5);
            return View(appetizers);

        }

        public ActionResult ListMainDishes(int? page)
        {
            var mainDishes = this.db.Recipes.Where(a => a.Type == "Основно ястие").OrderByDescending(x=>x.Date).ToList().ToPagedList(page ?? 1, 5);
            return View(mainDishes);
        }

        public ActionResult ListDesserts(int? page)
        {
            var desserts = this.db.Recipes.Where(a => a.Type == "Десерт").OrderByDescending(x => x.Date).ToList().ToPagedList(page ?? 1, 5);
            return View(desserts);
        }
        [Authorize]
        public ActionResult Create()
        {
            var products = this.db.Products.OrderBy(x => x.ProductName).Select(p => new SelectListItem() {
                Text = p.ProductName,
                Value = p.ID.ToString(),
                Selected = false
            }).ToList();


            return View(new RecipeInputViewModel { SelectProducts = products });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecipeInputViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var newProductsName = new List<string>();
                var newConfirmProduct = new List<ProductsConfirm>();
                if (model.newProduct != null)
                {
                    
                    newProductsName = model.newProduct.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (var pr in newProductsName)
                    {
                        var prod = new ProductsConfirm() { ProductName = pr };
                        this.db.ProductsConfirm.Add(prod);

                    }
                    this.db.SaveChanges();
                    
                    foreach (var pr in newProductsName)
                    {
                        var selectedNewProducts = this.db.ProductsConfirm.Where(x => x.ProductName == pr).FirstOrDefault();
                        newConfirmProduct.Add(selectedNewProducts);
                    }

                }
                var selectedProducts = new List<Product>();
                foreach (var product in model.SelectProducts)
                {
                    if (product.Selected && product.Value != null && product.Text != null)
                    {
                        var pr = this.db.Products.Where(prd => prd.ID.ToString() == product.Value).FirstOrDefault();
                        selectedProducts.Add(pr);
                    }
                }
                // TODO ДА СЕ СЪЗДАВА В МЕЖДИННА ТАБЛИЦА ОТ КОЯТО АДМИНА ДА ГИ ПРЕХВЪРЛЯ !!!!
                var newRecipe = new RecipeConfirm()
                {
                    AuthorId = this.User.Identity.GetUserId(),
                    Title = model.Title,
                    Description = model.Description,
                    Type = model.Type,
                    Products = selectedProducts,
                    ProductsConfirm = newConfirmProduct
                };
                this.db.RecipesConfirm.Add(newRecipe);
                this.db.SaveChanges();
                this.AddNotification("Рецептата предстои да бъде одобрена от администратор.", NotificationType.SUCCESS);
                switch (model.Type)
                {
                    case "Предястие":
                        return Redirect("~/Recipe/ListAppetizers");
                    case "Основно ястие":
                        return Redirect("~/Recipe/ListMainDishes");
                    case "Десерт":
                        return Redirect("~/Recipe/ListDesserts");
                }
            }
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var recipe = this.db.Recipes.Where(x => x.ID == id).Select(RecipeInputViewModel.ViewModel).FirstOrDefault();
            recipe.CurrentUserId = this.User.Identity.GetUserId();
            recipe.User = this.User.Identity.GetUserName();

            return View(recipe);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(RecipeInputViewModel model)
        {
            var rating = new Rating()
            {
                Vote = model.Rating,
                AuthorId = this.User.Identity.GetUserId()
            };
            this.db.SaveChanges();

            var recipe = this.db.Recipes.Find(model.Id);

            recipe.Ratings.Add(rating);
            this.db.SaveChanges();
            this.AddNotification("Вие оценихте рецептата успешно.", NotificationType.SUCCESS);
            return Redirect("/Recipe/Details/" + model.Id);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var recipeToDelete = this.db.Recipes.Find(id);

            if (recipeToDelete == null)
            {
                return HttpNotFound();
            }

            if (recipeToDelete.AuthorId == User.Identity.GetUserId() || IsAdmin())
            {
                this.db.Recipes.Remove(recipeToDelete);
                this.db.SaveChanges();
                this.AddNotification("Рецептата е премахната успешно.", NotificationType.SUCCESS);
            }
            return Redirect("/Recipe/Index");
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var recipeToEdit = this.db.Recipes.Where(recipe => recipe.ID == id).Select(RecipeInputViewModel.ViewModel).FirstOrDefault();

            var products = this.db.Products.OrderBy(x => x.ProductName).Select(p => new SelectListItem()
            {
                Text = p.ProductName,
                Value = p.ID.ToString(),
                Selected = false
            }).ToList();

            foreach (var product in recipeToEdit.Products)
            {
                foreach (var pr in products)
                {
                    if (product.ID == int.Parse(pr.Value))
                    {
                        pr.Selected = true;
                    }
                }
            }

            recipeToEdit.SelectProducts = products;
            if (this.User.Identity.GetUserId() == recipeToEdit.AuthorId || IsAdmin())
            {
                return View(recipeToEdit); 
            }
            else
            {
                this.AddNotification("Нямате право да променяте тази рецепта.", NotificationType.WARNING);
                return Redirect("/Recipe/Index");
                

            }
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include ="Products,SelectProducts,Title,Description,Type,Date,AuthorId")]*/RecipeInputViewModel model)
        {
            var editedRecipe = this.db.Recipes.Find(model.Id);

            var selectedProducts = new List<Product>();
            foreach (var product in model.SelectProducts)
            {
                if (product.Selected && product.Value != null && product.Text != null)
                {
                    var pr = this.db.Products.Where(prd => prd.ID.ToString() == product.Value).FirstOrDefault();
                    selectedProducts.Add(pr);
                }
            }
            editedRecipe.Products.Clear();
            editedRecipe.Title = model.Title;
            editedRecipe.Description = model.Description;
            editedRecipe.Type = model.Type;
            editedRecipe.Products = selectedProducts;
            editedRecipe.Date = DateTime.Now;
            editedRecipe.AuthorId = this.User.Identity.GetUserId();

            this.db.SaveChanges();
            this.AddNotification("Вие променихте успешно вашата рецепта.", NotificationType.SUCCESS);
            
            return Redirect("/Recipe/Details/" + model.Id);
        }
        [Authorize]
        public ActionResult AddComment()
        {
            return PartialView("_ShowComments");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(string CommentText, int id)
        {
            var newComment = new Comment()
            {
                Text = CommentText,
                AuthorId = this.User.Identity.GetUserId(),
                AuthorName = this.User.Identity.GetUserName()
            };
            this.db.SaveChanges();

            var recipeToAddComment = this.db.Recipes.Find(id);
            recipeToAddComment.Comments.Add(newComment);
            this.db.SaveChanges();
            this.AddNotification("Коментарът ви е добавен успешно.", NotificationType.SUCCESS);
            return Redirect("/Recipe/Details/" + id);
        }

        public ActionResult My()
        {
            var currentUser = this.User.Identity.GetUserId();
            var myRecipes = this.db.Recipes.Where(x => x.AuthorId == currentUser).ToList();
            return View(myRecipes);
        }
      
    }
}

