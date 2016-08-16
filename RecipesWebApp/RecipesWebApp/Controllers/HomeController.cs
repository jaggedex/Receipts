using RecipesWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipesWebApp.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var appetizers = this.db.Recipes.Where(a => a.Type == "Предястие").Take(3).ToList();
            var mainDishes = this.db.Recipes.Where(a => a.Type == "Основно ястие").Take(3).ToList();
            var desserts = this.db.Recipes.Where(a => a.Type == "Десерт").Take(3).ToList();

            return View(new RecipeCollectionsViewModel { Appetizers = appetizers, MainDishes = mainDishes, Desserts = desserts});
        }

        
    }
}