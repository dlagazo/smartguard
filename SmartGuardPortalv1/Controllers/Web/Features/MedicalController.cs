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
    
    
    public class MedicalController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Feature/
        [Authorize(Roles = "User,Contact")]
        public ActionResult Index()
        {
            return View(db.MedicalRecords.ToList());
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
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "User,Contact")]
        public ActionResult Download(int id = 0)
        {
            byte[] file = db.MedicalRecords.Where(i => i.MedicalId == id).First().MedicalFile;
            string type = db.MedicalRecords.Where(i => i.MedicalId == id).First().FileType;
            return File(file, type);
        }
        //
        // POST: /Feature/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create([Bind(Exclude = "MedicalFile")]Medical medical, HttpPostedFileBase MedicalFile)
        {
            if (ModelState.IsValid)
            {
                if (MedicalFile != null)
                {
                    //var file = Request.Files[0];
                    if (MedicalFile.ContentLength > 0)
                    {
                        var content = new byte[MedicalFile.ContentLength];
                        MedicalFile.InputStream.Read(content, 0, MedicalFile.ContentLength);
                        medical.MedicalFile = content;

                        medical.FileType = System.Web.MimeMapping.GetMimeMapping(MedicalFile.FileName);
                        // the rest of your db code here
                    }
                }

                medical.TimeStamp = DateTime.Now;
                db.MedicalRecords.Add(medical);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medical);
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
            Medical medical = db.MedicalRecords.Find(id);
            if (medical == null)
            {
                return HttpNotFound();
            }
            return View(medical);
        }

        //
        // POST: /Feature/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medical medical = db.MedicalRecords.Find(id);
            db.MedicalRecords.Remove(medical);
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