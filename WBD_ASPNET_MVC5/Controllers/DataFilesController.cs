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

        public ActionResult Upload() 
        {
            var data = new DataFile();
            data.Id = Guid.NewGuid().ToString();
            data.UploadDate = DateTime.Now;
            data.FileReference = "aasdasda";
            return View(data);
        }

        //
        // POST: /DataFiles/
        [HttpPost]
        //public async Task<ActionResult> Index([Bind(Include = "Id,DataName,FileReference,DataCategory,Description,UploadDate")]DataFile datafile, HttpPostedFileBase upload)
        public ActionResult Upload([Bind(Include = "Id,DataName,FileReference,DataCategory,Description,UploadDate")]DataFile datafile, HttpPostedFileBase upload)
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
                    upload.SaveAs("C:/Users/Forrest/Desktop/ " + FileName);
                    datafile.FileReference = "C:/Users/Forrest/Desktop/ " + FileName;
                }
                //db.DataFiles.Add(DataFile)
                db.DataFiles.Add(datafile);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            //return RedirectToAction("Projects", "Accounts");
            return Download(datafile);
        }

        public FileResult Download(DataFile datafile)
        {
            return File(datafile.FileReference, System.Net.Mime.MediaTypeNames.Application.Octet);
        }
	}
}