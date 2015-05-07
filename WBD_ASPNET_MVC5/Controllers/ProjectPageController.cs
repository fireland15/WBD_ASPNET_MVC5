using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WBD_ASPNET_MVC5.Controllers
{
    public class ProjectPageController : Controller
    {
        public ActionResult Details(string id)
        {
            return View();
        }
        //
        // GET: /AddUserToProject/
        public ActionResult AddUser()
        {
            return View();
        }
	}
}