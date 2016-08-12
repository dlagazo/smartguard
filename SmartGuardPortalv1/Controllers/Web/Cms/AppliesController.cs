using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartGuardPortalv1.Models;

namespace SmartGuardPortalv1.Controllers.Web.Cms
{
    
    public class AppliesController : Controller
    {
        private UsersContext db = new UsersContext();

        // GET: Applies
        [Authorize(Roles = "Administrator, Distributor")]
        public ActionResult Index()
        {
            return View(db.Applies.ToList());
        }

        [Authorize(Roles = "Administrator, Distributor")]
        // GET: Applies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apply apply = db.Applies.Find(id);
            if (apply == null)
            {
                return HttpNotFound();
            }
            return View(apply);
        }

        // GET: Applies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Applies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,lastName,firstName,address,city,country,zip,email,phone,mobile,cpRelation,cpLastName,cpFirstName,cpMobile,cpEmail,packages,payFreq,payMethod,fkDistroId")] Apply apply)
        {
            if (ModelState.IsValid)
            {
                apply.stamp = DateTime.Now;
                apply.status = 0;
                db.Applies.Add(apply);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return RedirectToAction("Create", "ContactUs");
        }

        // GET: Applies/Edit/5
        [Authorize(Roles = "Administrator, Distributor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apply apply = db.Applies.Find(id);
            if (apply == null)
            {
                return HttpNotFound();
            }
            return View(apply);
        }

        // POST: Applies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Distributor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,company,lastName,firstName,address,city,country,zip,email,phone,mobile,cpLastName,cpFirstName,cpMobile,cpEmail,packages,payFreq,payMethod")] Apply apply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apply);
        }

        // GET: Applies/Delete/5
        [Authorize(Roles = "Administrator, Distributor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apply apply = db.Applies.Find(id);
            if (apply == null)
            {
                return HttpNotFound();
            }
            return View(apply);
        }

        [Authorize(Roles = "Administrator, Distributor")]
        // POST: Applies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apply apply = db.Applies.Find(id);
            db.Applies.Remove(apply);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
