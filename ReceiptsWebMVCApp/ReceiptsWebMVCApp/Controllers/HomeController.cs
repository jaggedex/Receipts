using ReceiptsWebMVCApp.Models;
using ReceiptsWebMVCData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReceiptsWebMVCApp.Controllers
{
    public class HomeController : BaseController
    {
      

        public ActionResult Index()
        {
            var salads = this.db.Salads
                .OrderBy(s => s.Rating)
                .ThenByDescending(e => e.Date)
                .Select(FoodViewModels.Salad);

            var meals = this.db.Meals.OrderBy(s => s.Rating).ThenByDescending(e => e.Date).Select(FoodViewModels.Meal);

            var deserts = this.db.Desserts.OrderBy(s => s.Rating).ThenByDescending(e => e.Date).Select(FoodViewModels.Dessert);



            return View(new ReceiptsViewModels { Deserts = deserts, Meals = meals, Salads = salads });
        }
        

    }
}