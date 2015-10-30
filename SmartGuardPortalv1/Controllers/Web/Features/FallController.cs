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
    
    [Authorize(Roles="User")]
    public class FallController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Feature/

        public ActionResult Index()
        {
            return View(db.Falls.ToList());
        }

        //
        // GET: /Feature/Details/5

        public ActionResult Details(int id = 0)
        {
            Memory memory = db.Memories.Find(id);
            if (memory == null)
            {
                return HttpNotFound();
            }
            return View(memory);
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
        public ActionResult Create(Memory memory)
        {
            if (ModelState.IsValid)
            {
                db.Memories.Add(memory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memory);
        }

        //
        // GET: /Feature/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Memory Memory = db.Memories.Find(id);
            if (Memory == null)
            {
                return HttpNotFound();
            }
            return View(Memory);
        }

        //
        // POST: /Feature/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Memory memory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memory);
        }

        //
        // GET: /Feature/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Memory Memory = db.Memories.Find(id);
            if (Memory == null)
            {
                return HttpNotFound();
            }
            return View(Memory);
        }

        //
        // POST: /Feature/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Memory memory = db.Memories.Find(id);
            db.Memories.Remove(memory);
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