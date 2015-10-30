﻿using System;
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
    public class SubscriptionController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Feature/

        public ActionResult Index()
        {
            return View(db.Subscriptions.ToList());
        }

        //
        // GET: /Feature/Details/5

        public ActionResult Details(int id = 0)
        {
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
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
        public ActionResult Create(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                db.Subscriptions.Add(subscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subscription);
        }

        //
        // GET: /Feature/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        //
        // POST: /Feature/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Feature feature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feature);
        }

        //
        // GET: /Feature/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        //
        // POST: /Feature/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feature feature = db.Features.Find(id);
            db.Features.Remove(feature);
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