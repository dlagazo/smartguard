using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartGuardPortalv1.Models;
using System.Web.Security;
using WebMatrix.WebData;

namespace SmartGuardPortalv1.Controllers
{
    
    
    public class VitalInfoController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Feature/
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            int userId = (int)WebSecurity.CurrentUserId;
            return View(db.VitalInfos.Where(i=>i.fkUserId == userId).ToList());
        }

        

        

        
        //
        // GET: /Feature/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            return View();
        }

        

        //
        // POST: /Feature/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create(VitalInfo vi)
        {
            if (ModelState.IsValid)
            {
                vi.fkUserId = WebSecurity.CurrentUserId;
                db.VitalInfos.Add(vi);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(vi);
        }

        

        //
        // GET: /Feature/Edit/5
        [Authorize(Roles = "User")]
        public ActionResult Edit(int id = 0)
        {
            VitalInfo vital = db.VitalInfos.Find(id);
            
                
                if (vital == null)
                {
                    return HttpNotFound();
                }
                
                return View(vital);
                
                    
            
        }

        //
        // POST: /Feature/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Edit(VitalInfo vital)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vital).State = EntityState.Modified;
                db.SaveChanges();
                
                    return RedirectToAction("Index");
                
            }

            return View(vital);
        }

        //
        // GET: /Feature/Delete/5
        [Authorize(Roles = "User")]
        public ActionResult Delete(int id = 0)
        {
           VitalInfo vital = db.VitalInfos.Find(id);
            

                if (vital == null)
                {
                    return HttpNotFound();
                }

                return View(vital);

            
            
        }

        //
        // POST: /Feature/Delete/5
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VitalInfo vital = db.VitalInfos.Find(id);
            db.VitalInfos.Remove(vital);
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