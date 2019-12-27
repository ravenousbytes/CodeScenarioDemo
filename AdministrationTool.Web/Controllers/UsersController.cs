using AdministrationTool.Web.Models;
using AdministrationTool.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdministrationTool.Data.Models;
using AutoMapper;

namespace AdministrationTool.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserData db;
        private readonly IMapper mapper;

        public UsersController(IUserData db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult IndexMVC(string orderby)
        {
            var users = db.GetAll();
            var model = mapper.Map<IEnumerable<UserViewModel>>(users);
            SortModel(ref orderby, ref model);
            return View(model);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        private void SortModel(ref string orderby, ref IEnumerable<UserViewModel> model)
        {
            if (string.IsNullOrEmpty(orderby))
                orderby = "";

            ViewBag.PrincipalSort = orderby.Equals("principalname", StringComparison.OrdinalIgnoreCase) ? "principalnamedesc" : "principalname";
            ViewBag.FirstNameSort = orderby.Equals("firstname", StringComparison.OrdinalIgnoreCase) ? "firstnamedesc" : "firstname";
            ViewBag.LastNameSort = orderby.Equals("lastname", StringComparison.OrdinalIgnoreCase) ? "lastnamedesc" : "lastname";
            ViewBag.EmailSort = orderby.Equals("email", StringComparison.OrdinalIgnoreCase) ? "emaildesc" : "email";
            ViewBag.TelephoneNumberSort = orderby.Equals("telephonenumber", StringComparison.OrdinalIgnoreCase) ? "telephonenumberdesc" : "telephonenumber";
            ViewBag.EnabledSort = orderby.Equals("enabled", StringComparison.OrdinalIgnoreCase) ? "enableddesc" : "enabled";
            ViewBag.TitleSort = orderby.Equals("title", StringComparison.OrdinalIgnoreCase) ? "titledesc" : "title";
            ViewBag.ManagerSort = orderby.Equals("manager", StringComparison.OrdinalIgnoreCase) ? "managerdesc" : "manager";

            switch (orderby?.ToLower())
            {
                case "principalname":
                    model = model.OrderBy(u => u.PrincipalName);
                    break;
                case "firstname":
                    model = model.OrderBy(u => u.FirstName);
                    break;
                case "lastname":
                    model = model.OrderBy(u => u.LastName);
                    break;
                case "email":
                    model = model.OrderBy(u => u.Email);
                    break;
                case "telephonenumber":
                    model = model.OrderBy(u => u.TelephoneNumber);
                    break;
                case "title":
                    model = model.OrderBy(u => u.Title);
                    break;
                case "manager":
                    model = model.OrderBy(u => u.Manager?.LastName).OrderBy(u => u.Manager?.FirstName);
                    break;
                case "enabled":
                    model = model.OrderBy(u => u.Enabled);
                    break;
                case "principalnamedesc":
                    model = model.OrderByDescending(u => u.PrincipalName);
                    break;
                case "firstnamedesc":
                    model = model.OrderByDescending(u => u.FirstName);
                    break;
                case "lastnamedesc":
                    model = model.OrderByDescending(u => u.LastName);
                    break;
                case "emaildesc":
                    model = model.OrderByDescending(u => u.Email);
                    break;
                case "telephonenumberdesc":
                    model = model.OrderByDescending(u => u.TelephoneNumber);
                    break;
                case "managerdesc":
                    model = model.OrderByDescending(u => u.Manager?.LastName).OrderByDescending(u => u.Manager?.FirstName);
                    break;
                case "enableddesc":
                    model = model.OrderByDescending(u => u.Enabled);
                    break;
                case "titledesc":
                    model = model.OrderByDescending(u => u.Title);
                    break;
                default:
                    model = model.OrderBy(u => u.LastName);
                    break;
            }
        }

        [HttpGet]
        public ActionResult Details(string principalname)
        {
            var user = !string.IsNullOrWhiteSpace(principalname) ? db.Get(principalname) : null;
            if(user == null)
            {
                return View("NotFound");
            }
            var model = mapper.Map<UserViewModel>(user);
            model.Initialize(db);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new UserViewModel(db);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel user)
        {
            if (user != null)
                user.Initialize(db);
            user.ManagerPrincipalName = db.Get(user.ManagerId)?.PrincipalName;
            ModelState.Clear();
            TryValidateModel(user);
            if( ModelState.IsValid)
            {
                user.Manager = mapper.Map<UserViewModel>(db.Get(user.ManagerId));
                db.Add(mapper.Map<User>(user));
                TempData["Message"] = $"Added user {user.LastName}, {user.FirstName}";
                return RedirectToAction("Details", new { id = user.Id });
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(string principalname)
        {
            var user = !string.IsNullOrWhiteSpace(principalname) ? db.Get(principalname) : null;
            if (user == null)
            {
                return View("NotFound");
            }
            var model = mapper.Map<UserViewModel>(user);
            model.Initialize(db);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel user)
        {
            user.ManagerPrincipalName = db.Get(user.ManagerId)?.PrincipalName;
            ModelState.Clear();
            ValidateModel(user);
            if (ModelState.IsValid)
            {
                user.Manager = mapper.Map<UserViewModel>(db.Get(user.ManagerId));
                db.Update(mapper.Map<User>(user));
                TempData["Message"] = $"Updated user {user.LastName}, {user.FirstName}";
                return RedirectToAction("Details", new { id = user.Id });
            }
            user.Initialize(db);
            return View(user);
        }

        [HttpGet]
        public ActionResult OrganizationChart()
        {
            //    var users = db.GetAll();
            //    var model = UserViewModel.GetAll(users);
            //    //owners = owners.Where(u => u.Manager.Id == u.Id);
            //    //List<UserViewModel> model = new List<UserViewModel>();
            //    //UserViewModel owner = null;
            //    //foreach(var temp in owners)
            //    //{
            //    //    owner = new UserViewModel(temp, db);
            //    //}
            //    //model = owner.DirectReports as List<UserViewModel>;
            //    //if (model.Count <= 0)
            //    //{
            //    //    TempData["Message"] = "Sorry, there is no owner of the organization.";
            //    //}
            //    return View(model.ToList());

            return View();
            }
        }
}