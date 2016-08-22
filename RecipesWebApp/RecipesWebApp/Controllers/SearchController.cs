using RecipesWebApp.Models;
using RecipesWebData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipesWebApp.Controllers
{
    public class SearchController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string type)
        {
            var desired = new RecipeInputViewModel() { Type = type };
            return View(desired);
        }
        [HttpGet]
        public ActionResult SearchByProducts(string type)
        {
            var products = this.db.Products.Select(p => new SelectListItem()
            {
                Text = p.ProductName,
                Value = p.ID.ToString(),
                Selected = false
            }).ToList();

            var desiredProducts = new RecipeInputViewModel()
            {
                SelectProducts = products,
            };
            return this.PartialView("_SearchByProducts", desiredProducts);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchByProducts(RecipeInputViewModel model)
        {
            var matchedRecipeWithoutOne = new List<Recipe>();
            var matchedRecipes = new List<Recipe>();
            var recipes = this.db.Recipes.Where(y => y.Type == model.Type);

            var prdsList = new List<string>(); 

            
            var desiredProducts = new List<SelectListItem>();
            foreach (var item in model.SelectProducts)
            {
                
                if (item.Selected)
                {
                    desiredProducts.Add(item);
                }
            }

            foreach (var recipe in recipes)
            {
                foreach (var prod in recipe.Products)
                {
                    prdsList.Add(prod.ProductName);
                }

                int counter = 0;
                foreach (var desiredProduct in desiredProducts)
                {
                    if (prdsList.Contains(desiredProduct.Text))
                    {
                        counter++;
                    }
                   // Тук може да се пипне логиката :)
                }
                if (counter == recipe.Products.Count())
                {
                    matchedRecipes.Add(recipe);
                }

                if (counter == recipe.Products.Count() - 1)
                {
                    matchedRecipeWithoutOne.Add(recipe);
                }
                prdsList.Clear();
            }
            return View(new SearchViewModel { MatchedByAll = matchedRecipes, MatchedWithoutOne = matchedRecipeWithoutOne });
            
        }
        [HttpGet]
        public ActionResult SearchByMainProduct(string type)
        {
            var desire = new RecipeInputViewModel() { Type  = type}; 
            return this.PartialView("_SearchByMainProduct", desire);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchByMainProduct(RecipeInputViewModel model, string searchByProduct)
        {
            var recipes = this.db.Recipes.Where(y => y.Type == model.Type);
            var prdsList = new List<string>();
            var matchedByOneProduct = new List<Recipe>();

            foreach (var recipe in recipes)
            {
                foreach (var prod in recipe.Products)
                {
                    prdsList.Add(prod.ProductName);
                }
                if (prdsList.Contains(searchByProduct))
                {
                    matchedByOneProduct.Add(recipe);
                }
                prdsList.Clear();
            }

            return View(new SearchViewModel { MatchedByOneProduct = matchedByOneProduct });
        }
    }
}