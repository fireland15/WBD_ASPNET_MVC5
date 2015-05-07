using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WBD_ASPNET_MVC5.Models;

namespace WBD_ASPNET_MVC5.Controllers
{
    public class ProjectPageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        public ProjectPageController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public ProjectPageController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        //
        // GET: /AddUserToProject/
        public ActionResult AddUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            var model = new ProjectUsernameViewModel();
            model.projectID = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddUser(ProjectUsernameViewModel m)
        {
            if (m.Username != null)
            {
                var UPA = new UserProjectAssociation();
                UPA.ProjectId = m.projectID;
                var user = UserManager.FindByName(m.Username);
                if (user != null)
                {
                    UPA.UserId = user.Id;
                    db.UserProjectAssoc.Add(UPA);
                }
            }
            return View();
        }
    }
}