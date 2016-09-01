using RecipesWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipesWebApp.Controllers
{
    [RequireHttps]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var appetizers = this.db.Recipes.Include(r => r.Author).Where(a => a.Type == "Предястие").OrderByDescending(x => x.Date).Take(3).ToList();
            var mainDishes = this.db.Recipes.Include(r => r.Author).Where(a => a.Type == "Основно ястие").OrderByDescending(x => x.Date).Take(3).ToList();
            var desserts = this.db.Recipes.Include(r => r.Author).Where(a => a.Type == "Десерт").OrderByDescending(x => x.Date).Take(3).ToList();

            return View(new RecipeCollectionsViewModel { Appetizers = appetizers, MainDishes = mainDishes, Desserts = desserts});
        }

        
    }
}