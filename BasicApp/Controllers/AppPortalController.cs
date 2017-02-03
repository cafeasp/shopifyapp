using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicApp.Controllers
{
    public class AppPortalController : Controller
    {
        // GET: AppPortal
        public ActionResult Index()
        {
            return View();
        }
    }
}