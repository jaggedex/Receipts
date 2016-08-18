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

namespace RecipesWebApp.Controllers
{
    public class RecipeController : BaseController
    {
        // GET: Recipe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListAppetizers()
        {
            var appetizers = this.db.Recipes.Where(a => a.Type == "Предястие").ToList();
            return View(appetizers);

        }

        public ActionResult ListMainDishes()
        {
            var mainDishes = this.db.Recipes.Where(a => a.Type == "Основно ястие").ToList();
            return View(mainDishes);
        }

        public ActionResult ListDesserts()
        {
            var desserts = this.db.Recipes.Where(a => a.Type == "Десерт").ToList();
            return View(desserts);
        }
        public ActionResult Create()
        {
            var products = this.db.Products.Select(p => new SelectListItem() {
                Text = p.ProductName,
                Value = p.ID.ToString(),
                Selected = false
            }).ToList();


            return View(new RecipeInputViewModel { SelectProducts = products });
        }
        [HttpPost]
        public ActionResult Create(RecipeInputViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
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
                var a = new Recipe()
                {
                    AuthorId = this.User.Identity.GetUserId(),
                    Title = model.Title,
                    Description = model.Description,
                    Type = model.Type,
                    Products = selectedProducts
                };
                this.db.Recipes.Add(a);
                this.db.SaveChanges();

            }
            return Redirect("~/Recipe/");
        }
        public ActionResult Details(int id)
        {
            var recipe = this.db.Recipes.Where(x => x.ID == id).Select(RecipeInputViewModel.ViewModel).ToList();
            return View(recipe[0]);
        }

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

        public ActionResult Edit(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var recipeToEdit = this.db.Recipes.Where(recipe => recipe.ID == id).Select(RecipeInputViewModel.ViewModel).FirstOrDefault();

            var products = this.db.Products.Select(p => new SelectListItem()
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
        [HttpPost]
        public ActionResult Edit(RecipeInputViewModel model)
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

            return Redirect("/Recipe/Index");
        }
    }
}