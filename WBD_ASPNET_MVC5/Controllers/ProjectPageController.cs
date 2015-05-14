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
            
            var model = new DataProjectListViewModel();
            model.data = new List<DataFile>();
            model.project = project;

            var sd = db.DataFiles.ToList().Where(df => df.UploaderID == User.Identity.GetUserId());
            foreach (var w in sd) {
                if (db.DataProjectAssociations.Find(w.Id, id) == null)
                {
                    model.data.Add(w);
                }
            }

            if (!model.data.Any())
            {
                ViewBag.EmptyModel = "You have no files that are not already a part of this project. Upload more files then add to the project.";
            }

            return View(model);
        }

        public ActionResult ViewProjectData(string id)
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

            var model = new DataProjectListViewModel();
            model.data = new List<DataFile>();
            model.project = project;

            var sd = db.DataProjectAssociations.ToList().Where(df => df.ProjectId == project.Id);
            if (sd != null)
            {
                foreach (var w in sd)
                {
                    var x = db.DataFiles.Find(w.DataId);
                    if (x != null)
                    {
                        model.data.Add(x);
                    }
                }
            }
            else
            {
                return RedirectToAction("Details", "ProjectPage", new { id = id });
            }
            return View(model);
        }

        public ActionResult LinkDataToProject(string id, string dataid)
        {
            var DPA = new DataProjectAssoc();
            DPA.DataId = dataid;
            DPA.ProjectId = id;

            var exists = db.DataProjectAssociations.Find(DPA.DataId, DPA.ProjectId);
            if (exists == null)
            {
                db.DataProjectAssociations.Add(DPA);
                db.SaveChanges();
            }

            return RedirectToAction("ViewProjectData", "ProjectPage", new { id = id });
        }
    }
}