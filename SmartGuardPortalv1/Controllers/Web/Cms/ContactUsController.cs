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
    
    
    public class ContactUsController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Feature/
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.ContactUs.ToList());
        }

        //
        // GET: /Feature/Details/5

        public ActionResult Details(int id = 0)
        {
            ContactUs contactUs = db.ContactUs.Find(id);
            if (contactUs == null)
            {
                return HttpNotFound();
            }
            return View(contactUs);
        }

        //
        // GET: /Feature/Create
        
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Feature/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactUs contactus)
        {
            if (ModelState.IsValid)
            {
                contactus.TimeStamp = DateTime.Now;
                db.ContactUs.Add(contactus);
                db.SaveChanges();
                return RedirectToAction("Index", "Home", null);
            }

            return View(contactus);
        }

        //
        // GET: /Feature/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id = 0)
        {
            ContactUs contactus = db.ContactUs.Find(id);
            if (contactus == null)
            {
                return HttpNotFound();
            }
            return View(contactus);
        }

        //
        // POST: /Feature/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(ContactUs contactus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactus);
        }

        //
        // GET: /Feature/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id = 0)
        {
            ContactUs contactus = db.ContactUs.Find(id);
            if (contactus == null)
            {
                return HttpNotFound();
            }
            return View(contactus);
        }

        //
        // POST: /Feature/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactUs contactus = db.ContactUs.Find(id);
            db.ContactUs.Remove(contactus);
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