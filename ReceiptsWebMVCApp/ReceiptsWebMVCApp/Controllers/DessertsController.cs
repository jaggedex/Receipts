using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReceiptsWebMVCApp.Controllers
{
    public class DessertsController : Controller
    {
        // GET: Desserts
        public ActionResult Index()
        {
            return View();
        }
    }
}