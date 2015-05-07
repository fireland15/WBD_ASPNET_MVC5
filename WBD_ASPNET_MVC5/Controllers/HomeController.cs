using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WBD_ASPNET_MVC5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Meet The Team";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Get in touch with us!";

            return View();
        }
    }
}