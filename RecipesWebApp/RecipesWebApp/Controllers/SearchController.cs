using RecipesWebApp.Models;
using RecipesWebData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var products = this.db.Products.OrderBy(x =>x.ProductName).Select(p => new SelectListItem()
            {
                Text = p.ProductName,
                Value = p.ID.ToString(),
                Selected = false
            }).ToList();

            var desiredProducts = new RecipeInputViewModel()
            {
                SelectProducts = products,
                Type = type
            };
            return View("ByProducts", desiredProducts);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchByProducts(RecipeInputViewModel model)
        {
            var matchedRecipeWithoutOne = new List<Recipe>();
            var matchedRecipes = new List<Recipe>();
            var recipes = this.db.Recipes.Include(a => a.Author).Where(y => y.Type == model.Type);

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

                if (counter == recipe.Products.Count() - 1 && counter!=0)
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
            return View("ByMainProduct", desire);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SearchByMainProduct(RecipeInputViewModel model, string searchByProduct)
        {
            var recipes = this.db.Recipes.Include(a =>a.Author).Where(y => y.Type == model.Type);
            var prdsList = new List<string>();
            var matchedByOneProduct = new List<Recipe>();

            foreach (var recipe in recipes)
            {
                foreach (var prod in recipe.Products)
                {
                    prdsList.Add(prod.ProductName.ToLower());
                }
                if (prdsList.Contains(searchByProduct.ToLower()))
                {
                    matchedByOneProduct.Add(recipe);
                }
                prdsList.Clear();
            }

            return View(new SearchViewModel { MatchedByOneProduct = matchedByOneProduct });
        }
        [ValidateInput(false)]
        public ActionResult SearchByTitle(string searchingPhrase)
        {
            var matchingRecipesByTitle = new List<Recipe>();
            if (!string.IsNullOrEmpty(searchingPhrase))
            {
                var allRecipes = this.db.Recipes.Include(a => a.Author);
                foreach (var recipe in allRecipes)
                {

                    if (recipe.Title.ToLower().Contains(searchingPhrase.ToLower()))
                    {
                        matchingRecipesByTitle.Add(recipe);
                    }
                }
            }
            return View(matchingRecipesByTitle);
        }
    }
}