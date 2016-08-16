using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecipesWebApp.Models;
using System.Collections;
using RecipesWebData;
using Microsoft.AspNet.Identity;

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
           
            
            return View(new RecipeInputViewModel { Products = products});
        }
        [HttpPost]
        public ActionResult Create(RecipeInputViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var selectedProducts = new List<Product>();
                foreach (var product in model.Products)
                {
                    if (product.Selected)
                    {
                        var pr = this.db.Products.Where(prd => prd.ID.ToString() == product.Value).FirstOrDefault();
                        selectedProducts.Add(pr);
                    }
                }
                if (model.Type == "Предястие")
                {
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
                else if (true)
                {
                    return View();
                }
                else
                {
                    return View();
                }
            }
            return Redirect("~/Recipe/ListAppetizers");
        }
    }
}