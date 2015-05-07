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

        public ActionResult Index() 
        {
            return View();
        }

        //
        // POST: /DataFiles/
        [HttpPost]
        public async Task<ActionResult> Index([Bind(Include = "Id,DataName,FileReference,DataCategory,Description,UploadDate")] HttpPostedFileBase upload)
        {    
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    string path = Path.Combine("C:/Users/Forrest/Desktop/", Path.GetFileName(upload.FileName));
                    upload.SaveAs(path);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Home", "Accounts");
        }
	}
}