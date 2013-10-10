using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dotnet.openvehicletracker.org.Controllers
{
    public class HomeController : BaseOVTController
    {
        public HomeController() : this(null) { }
        public HomeController(dotnet.openvehicletracker.org.Models.Entities.IOVTContext entities = null) : base(entities) { }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            ViewBag.Count = Entities.Organizations.Count();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
