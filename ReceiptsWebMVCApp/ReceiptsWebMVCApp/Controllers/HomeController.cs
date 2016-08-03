using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReceiptsWebMVCApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page44446.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page21.";

            return View();
        }
    }
}