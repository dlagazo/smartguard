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
    
    [Authorize(Roles="Administrator")]
    public class FallProfileController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Feature/

        public ActionResult Index()
        {
            return View(db.FallProfiles.ToList());
        }

        //
        // GET: /Feature/Details/5

        public ActionResult Details(int id = 0)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
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
        public ActionResult Create(FallProfile fp)
        {
            if (ModelState.IsValid)
            {
                db.FallProfiles.Add(fp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fp);
        }

        //
        // GET: /Feature/Edit/5

        public ActionResult Edit(int id = 0)
        {
            FallProfile fp = db.FallProfiles.Find(id);
            if (fp == null)
            {
                return HttpNotFound();
            }
            return View(fp);
        }

        //
        // POST: /Feature/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FallProfile fp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fp);
        }

        //
        // GET: /Feature/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        //
        // POST: /Feature/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Country country = db.Countries.Find(id);
            db.Countries.Remove(country);
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