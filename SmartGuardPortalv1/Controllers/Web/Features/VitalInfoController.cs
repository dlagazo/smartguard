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
        // GET: /Feature/Details/5
        [Authorize(Roles = "User")]
        public ActionResult Details(int id = 0)
        {

            
                Memory memory = db.Memories.Find(id);
                if (memory == null)
                {
                    return HttpNotFound();
                }
                return View(memory);
           
            
           
                   
                
            
            
            
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Details(Memory memory)
        {


            if (ModelState.IsValid)
            {


                //memory.MemoryDates = "Edited";
                db.Entry(memory).State = EntityState.Modified;
                db.SaveChanges();
                
            }

            return View(memory);
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
            Memory memory = db.Memories.Find(id);
            
                
                if (memory == null)
                {
                    return HttpNotFound();
                }
                
                return View(memory);
                
                    
            
        }

        //
        // POST: /Feature/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
        public ActionResult Delete(int id = 0)
        {
            Memory memory = db.Memories.Find(id);
            

                if (memory == null)
                {
                    return HttpNotFound();
                }

                return View(memory);

            
            
        }

        //
        // POST: /Feature/Delete/5
        [Authorize(Roles = "User")]
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