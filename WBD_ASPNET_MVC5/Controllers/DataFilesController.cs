using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
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
                var fileList1 = from file in db.DataFiles select file;
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
                var fileList = db.DataFiles.ToList().Where(datafile => datafile.UploaderID == uID);
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
            data.UploaderID = User.Identity.GetUserId();
            return View(data);
        }

        //
        // POST: /DataFiles/
        [HttpPost]
        //public async Task<ActionResult> Index([Bind(Include = "Id,DataName,FileReference,DataCategory,Description,UploadDate")]DataFile datafile, HttpPostedFileBase upload)
        public ActionResult Upload([Bind(Include = "Id,DataName,FileReference,DataCategory,Description,UploadDate,UploaderID")]DataFile datafile, HttpPostedFileBase upload)
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
                }
                //db.DataFiles.Add(DataFile)
                db.DataFiles.Add(datafile);
                db.SaveChanges();
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
	}
}