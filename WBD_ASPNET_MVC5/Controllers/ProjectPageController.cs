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
            var puVM = new ProjectUserViewModel();
            puVM.project = db.Projects.Find(id);
            if (puVM.project == null)
            {
                return HttpNotFound();
            }
            puVM.user = UserManager.FindById(puVM.project.OwnerID);

            return View(puVM);
        }
        //
        // GET: /AddUserToProject/
        public ActionResult AddUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            var model = new ProjectUsernameViewModel();
            model.projectID = id;
            model.projectName = project.ProjectName;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddUser([Bind(Include="projectID,projectName,Username")]ProjectUsernameViewModel model)
         {
            if (model.Username != null)
            {
                var UPA = new UserProjectAssociation();
                UPA.ProjectId = model.projectID;
                var user = UserManager.FindByName(model.Username);
                if (user != null)
                {
                    UPA.UserId = user.Id;
                    db.UserProjectAssoc.Add(UPA);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Details", "ProjectPage", new { id = model.projectID });
        }
    }
}