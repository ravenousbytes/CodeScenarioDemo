using AdministrationTool.Data.Services;
using AdministrationTool.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdministrationTool.Web.Controllers
{
    //TODO: Require authentication for real user management [Authorize("Administrator")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeViewModel();
            return View(model);
        }
    }
}