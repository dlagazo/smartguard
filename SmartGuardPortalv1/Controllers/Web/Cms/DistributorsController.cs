using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartGuardPortalv1.Models;
using System.Web.Security;
using WebMatrix.WebData;

namespace SmartGuardPortalv1.Controllers.Web.Cms
{
    [Authorize(Roles = "Administrator, Distributor" )]
    public class DistributorsController : Controller
    {
        private UsersContext db = new UsersContext();

        // GET: Distributors
        public ActionResult Index()
        {
            return View(db.Distributors.ToList());
        }

        // GET: Distributors/Details/5
        public ActionResult Details()
        {
            
            return View();
        }

        // GET: Distributors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Distributors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,lat,lng,address,city,country,telephone,mobile, email")] Distributor distributor)
        {
            if (ModelState.IsValid)
            {
                int userId = (int)WebSecurity.CurrentUserId;
                distributor.fkUserId = userId;
                distributor.stamp = DateTime.Now;
                db.Distributors.Add(distributor);
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            return View(distributor);
        }

        // GET: Distributors/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Distributor distributor = db.Distributors.Find(id);
            if (distributor == null)
            {
                return HttpNotFound();
            }
            return View(distributor);
        }

        // POST: Distributors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,lat,lng,address,city,country,stamp,fkUserId,telephone,mobile,email")] Distributor distributor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(distributor).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Details");
            }
            return View(distributor);
        }

        // GET: Distributors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Distributor distributor = db.Distributors.Find(id);
            if (distributor == null)
            {
                return HttpNotFound();
            }
            return View(distributor);
        }

        // POST: Distributors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Distributor distributor = db.Distributors.Find(id);
            db.Distributors.Remove(distributor);
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
