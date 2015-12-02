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
    
    [Authorize(Roles="Contact")]
    public class ContactScheduleController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Feature/

        public ActionResult Index()
        {
            return View(db.ContactSchedules.ToList());
        }

        //
        // GET: /Feature/Details/5

        public ActionResult Details(int id = 0)
        {
            ContactSchedule contactSchedule = db.ContactSchedules.Find(id);
            if (contactSchedule == null)
            {
                return HttpNotFound();
            }
            return View(contactSchedule);
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
        public ActionResult Create(ContactSchedule contactSchedule)
        {
            if (ModelState.IsValid)
            {
                db.ContactSchedules.Add(contactSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactSchedule);
        }

        //
        // GET: /Feature/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ContactSchedule contactSchedule = db.ContactSchedules.Where(i => i.fkUserId == id).First();
            if (contactSchedule == null)
            {
                return HttpNotFound();
            }
            return View(contactSchedule);
        }

        //
        // POST: /Feature/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactSchedule contactSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile","Account");
            }
            return View(contactSchedule);
        }

        //
        // GET: /Feature/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ContactSchedule contactSchedule = db.ContactSchedules.Find(id);
            if (contactSchedule == null)
            {
                return HttpNotFound();
            }
            return View(contactSchedule);
        }

        //
        // POST: /Feature/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactSchedule contactSchedule = db.ContactSchedules.Find(id);
            db.ContactSchedules.Remove(contactSchedule);
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