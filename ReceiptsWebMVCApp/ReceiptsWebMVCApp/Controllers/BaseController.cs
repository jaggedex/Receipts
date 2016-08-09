using Microsoft.AspNet.Identity;
using ReceiptsWebMVCData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReceiptsWebMVCApp.Controllers
{
    public class BaseController : Controller
    {
        
       protected ApplicationDbContext db = new ApplicationDbContext();

        public bool IsAdmin()
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = (currentUserId != null && (this.User.IsInRole("Administrator") || this.User.IsInRole("MasterAdministrator")));
            return isAdmin;
        }
         
    }
}