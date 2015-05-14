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
            var user = UserManager.FindById(puVM.project.OwnerID);
            puVM.OwnerFirstLastName = user.FirstName + " " + user.LastName;

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
                if (user == null)
                {
                    ViewBag.error = "Username does not exist.";
                    return View(model);
                }
                if (user != null)
                {
                    var exists = db.UserProjectAssoc.Find(user.Id, model.projectID);
                    if (exists != null)
                    {
                        ViewBag.error = "User is already a part of the project.";
                        return View(model);
                    }
                    UPA.UserId = user.Id;
                    UPA.DateAdded = DateTime.Today;
                    db.UserProjectAssoc.Add(UPA);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ViewProjectUsers", "ProjectPage", new { id = model.projectID });
        }

        public ActionResult ViewProjectUsers(string id)
        {
            var UserList = db.UserProjectAssoc.ToList().Where(entry => entry.ProjectId == id);
            List<ProjectUsersListViewModel> PUVM = new List<ProjectUsersListViewModel>();
            foreach (var entry in UserList)
            {
                var p = new ProjectUsersListViewModel();
                p.ProjectId = id;
                p.Username = UserManager.FindById(entry.UserId).UserName;
                p.AddedOn = db.UserProjectAssoc.Find(entry.UserId, id).DateAdded;
                PUVM.Add(p);
            }

            if (!UserList.Any())
            {
                return RedirectToAction("Details", "ProjectPage", new { id = id });
            }

            return View(PUVM);
        }

        public ActionResult AddData(string id)
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
            
            var model = new List<DataProjectListViewModel>();

            var sd = db.DataFiles.ToList().Where(df => df.UploaderID == User.Identity.GetUserId());

            foreach (var w in sd) {
                var x = new DataProjectListViewModel();
                x.data.AddToProject = 0;
                x.data.datafile = w;
                model.Add(x);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddData(List<DataProjectListViewModel> model)
        {
            foreach (var x in model) {
                if (x.data.AddToProject == true) {
                    var DPA = new DataProjectAssoc();
                    DPA.ProjectId = x.project.Id;
                    DPA.DataId = x.data.datafile.Id;
                }

              
                //db.UserProjectAssoc.Add(UPA);
                //db.SaveChanges();
            }
            return RedirectToAction("ViewProjectUsers", "ProjectPage", new { id = model.First().project.Id });
        }
    }
}