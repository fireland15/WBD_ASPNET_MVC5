using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WBD_ASPNET_MVC5.Models;

namespace WBD_ASPNET_MVC5.Controllers
{
    public class DataFilesController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        public DataFilesController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public DataFilesController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public ActionResult Index(string Sorting_Order)
        {
            var uID = User.Identity.GetUserId();
            if (uID != null)
            {
                ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";
                ViewBag.SortingDate = Sorting_Order == "Upload_Date" ? "Date_Description" : "Date";
                ViewBag.SortingType = String.IsNullOrEmpty(Sorting_Order) ? "Data_Type" : "";
                //var fileList1 = from file in db.DataFiles select file;
                var fileList1 = db.DataFiles.ToList().Where(datafile => datafile.UploaderID == uID);
                switch (Sorting_Order)
                {
                    case "Name_Description":
                        fileList1 = fileList1.OrderByDescending(file => file.DataName);
                        break;
                    case "Date_Enroll":
                        fileList1 = fileList1.OrderBy(file => file.UploadDate);
                        break;
                    case "Date_Description":
                        fileList1 = fileList1.OrderByDescending(file => file.UploadDate);
                        break;
                    case "Data_Type":
                        fileList1 = fileList1.OrderBy(file => file.DataCategory);
                        break;
                    default:
                        fileList1 = fileList1.OrderBy(file => file.DataName);
                        break;
                }
                //return View(fileList);
                return View(fileList1);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Upload() 
        {
            var data = new DataFile();
            data.Id = Guid.NewGuid().ToString();
            data.UploadDate = DateTime.Now;
            data.FileReference = "aasdasda";
            data.ImageReference = "aasdasda";
            data.UploaderID = User.Identity.GetUserId();
            return View(data);
        }

        //
        // POST: /DataFiles/
        [HttpPost]
        //public async Task<ActionResult> Index([Bind(Include = "Id,DataName,FileReference,DataCategory,Description,UploadDate")]DataFile datafile, HttpPostedFileBase upload)
        public ActionResult Upload([Bind(Include = "Id,DataName,FileReference,DataCategory,Description,UploadDate,UploaderID,ImageReference")]DataFile datafile, HttpPostedFileBase upload)
        {    
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    //datafile.FileReference = System.IO.Path.GetFullPath(upload.FileName);
                    //datafile.DataName = System.IO.Path.GetFileName(upload.FileName);
                    //datafile.DataCategory = System.IO.Path.GetExtension(upload.FileName);
                    //string path = Path.Combine("C:/Users/i7/Desktop/", Path.GetFileName(upload.FileName));
                    //upload.SaveAs(path);
                    string FileName = upload.FileName;
                    string FileContentType = upload.ContentType;
                    byte[] filebytes = new byte[upload.ContentLength];
                    //upload.InputStream.Read(filebytes, 0, Convert.ToInt32(upload.ContentLength));
                    //System.IO.File.WriteAllBytes("c:/users/i7/desktop/test.jpeg", filebytes);
                    upload.SaveAs(Server.MapPath("~").ToString() + "/FILES/" + FileName);
                    datafile.FileReference = Server.MapPath("~").ToString() + "/FILES/" + FileName;
                    datafile.ImageReference = "~/FILES/" + FileName;
                }
                //db.DataFiles.Add(DataFile)
                db.DataFiles.Add(datafile);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                    {
                        // Get entry

                        DbEntityEntry entry = item.Entry;
                        string entityTypeName = entry.Entity.GetType().Name;

                        // Display or log error messages

                        foreach (DbValidationError subItem in item.ValidationErrors)
                        {
                            string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                     subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                            Console.WriteLine(message);
                        }
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                entry.State = EntityState.Detached;
                                break;
                            case EntityState.Modified:
                                entry.CurrentValues.SetValues(entry.OriginalValues);
                                entry.State = EntityState.Unchanged;
                                break;
                            case EntityState.Deleted:
                                entry.State = EntityState.Unchanged;
                                break;
                        }
                        db.SaveChanges();
                    }
                }
                //return RedirectToAction("Index");
            }
            //return RedirectToAction("Projects", "Accounts");
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string fileId)
        {
            DataFile df = db.DataFiles.Find(fileId);
            db.DataFiles.Remove(df);
            db.SaveChanges();
            System.IO.File.Delete(df.FileReference);
            return RedirectToAction("Index");
        }

        public FileResult Download(string fileId)
        {
            var file = db.DataFiles.Find(fileId);
            return File(file.FileReference, System.IO.Path.GetExtension(file.FileReference).ToLower(), file.DataName + System.IO.Path.GetExtension(file.FileReference).ToLower());
        }

        public ActionResult View()
        {
            string filepath = Server.MapPath("~") + "/FILES/MSA.txt";
            string content = string.Empty;

            content += "<html><head><style type=\"text/css\"></style></head><body><pre style=\"word-wrap: break-word; white-space: pre-wrap; font: consola;\">";

            try
            {
                using (var stream = new StreamReader(filepath))
                {
                    content = stream.ReadToEnd();
                    content = content.Replace(System.Environment.NewLine, "<br/>");
                }
            }

                
            catch (Exception exc)
            {
                return Content("Uh oh!");
            }
            content+="</pre></body></html>";
            return Content(content);
        }
	}
}