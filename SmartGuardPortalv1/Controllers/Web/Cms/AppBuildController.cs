using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartGuardPortalv1.Models;

namespace SmartGuardPortalv1.Controllers
{
    
    
    public class AppBuildController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Feature/
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.AppBuilds.ToList());
        }

        //
        // GET: /Feature/Details/5
        [Authorize(Roles = "User")]
        public ActionResult Details(int id = 0)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        //
        // GET: /Feature/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult DownloadLatest()
        {
            AppBuild app = db.AppBuilds.Where(i => i.isActive).OrderByDescending(i => i.timeStamp).FirstOrDefault();
            byte[] file = app.Apk;
            //string type = db.MedicalRecords.Where(i => i.MedicalId == id).First().FileType;
            return File(file, "apk", app.AppVersion + ".apk");
        }

        //[Authorize(Roles = "User,Administrator")]
        public ActionResult Download(int id = 0)
        {
            AppBuild app = db.AppBuilds.Where(i => i.AppBuildId == id).First();
            byte[] file = app.Apk;
            //string type = db.MedicalRecords.Where(i => i.MedicalId == id).First().FileType;
            return File(file, "apk", app.AppVersion + ".apk");
        }

        [Authorize(Roles = "User,Administrator")]
        public ActionResult Documentation(int id = 0)
        {
            AppBuild app = db.AppBuilds.Where(i => i.AppBuildId == id).First();
            byte[] file = app.AppDocumentation;
            //string type = db.MedicalRecords.Where(i => i.MedicalId == id).First().FileType;
            return File(file, "pdf", app.AppVersion + ".pdf");
        }

        //
        // POST: /Feature/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(AppBuild appBuild, List<HttpPostedFileBase> files)//([Bind(Exclude = "Apk,AppDocumentation")]AppBuild appBuild, HttpPostedFileBase Apk, HttpPostedFileBase AppDocument)
        {
            if (ModelState.IsValid)
            {
                if (files[0] != null)
                {
                    //var file = Request.Files[0];
                    if (files[0].ContentLength > 0)
                    {
                        var content = new byte[files[0].ContentLength];
                        files[0].InputStream.Read(content, 0, files[0].ContentLength);
                        appBuild.Apk = content;

                        //medical.FileType = System.Web.MimeMapping.GetMimeMapping(Apk.FileName);
                        // the rest of your db code here
                    }
                    if (files[1].ContentLength > 0)
                    {
                        var content = new byte[files[1].ContentLength];
                        files[1].InputStream.Read(content, 0, files[1].ContentLength);
                        appBuild.AppDocumentation = content;

                        //medical.FileType = System.Web.MimeMapping.GetMimeMapping(Apk.FileName);
                        // the rest of your db code here
                    }
                }

                appBuild.timeStamp = DateTime.Now;
                db.AppBuilds.Add(appBuild);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appBuild);
        }
         

        //
        // GET: /Feature/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        //
        // POST: /Feature/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        //
        // GET: /Feature/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AppBuild app = db.AppBuilds.Find(id);
            if (app == null)
            {
                return HttpNotFound();
            }
            return View(app);
        }

        //
        // POST: /Feature/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppBuild app = db.AppBuilds.Find(id);
            db.AppBuilds.Remove(app);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}