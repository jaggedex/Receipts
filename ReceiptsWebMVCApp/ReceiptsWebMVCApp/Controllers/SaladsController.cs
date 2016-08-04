using ReceiptsWebMVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ReceiptsWebMVCData;

namespace ReceiptsWebMVCApp.Controllers
{
    public class SaladsController : Controller
    {
        // GET: Salads
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var salads = db.Salads.Select(SaladViewModels.ViewModel).First();
            
            return View(salads);
        }
    }
}