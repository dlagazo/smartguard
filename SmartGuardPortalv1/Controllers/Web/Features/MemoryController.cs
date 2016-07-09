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
    
    
    public class MemoryController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Feature/
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            return View(db.Memories.Where(i => i.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId).ToList());
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
                else if(memory.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId)
                    return View(memory);
                else
                    return HttpNotFound();
            
           
                   
                
            
            
            
        }

        public Boolean isAllowed(int id = 0)
        {
            if(Roles.IsUserInRole("Contact"))
            {
                UserProfile profile = db.UserProfiles.Find(WebSecurity.CurrentUserId);
                Memory memory = db.Memories.Find(id);
                int ownerId = memory.fkUserId;
                
                foreach(Contact contact in db.Contacts.Where(i => i.fkUserId == ownerId))
                {
                    if (contact.Email == profile.Email)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
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

        [Authorize(Roles = "Contact")]
        public ActionResult ContactCreate(int id = 0)
        {
            return View();
        }

        //
        // POST: /Feature/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create(Memory memory)
        {
            if (ModelState.IsValid)
            {
                db.Memories.Add(memory);
                db.SaveChanges();

                return RedirectToAction("Details/" + memory.MemoryId);
            }

            return View(memory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Contact")]
        public ActionResult ContactCreate(Memory memory)
        {
            if (ModelState.IsValid)
            {
                db.Memories.Add(memory);
                db.SaveChanges();

                return RedirectToAction("Details/" + memory.MemoryId);
            }

            return RedirectToAction("AllMemory","Home");
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
                else if(memory.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId)
                    return View(memory);
                else
                    return HttpNotFound();
                    
            
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
                else if(memory.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId)
                    return View(memory);
                else
                    return HttpNotFound();

            
            
        }

        //
        // POST: /Feature/Delete/5
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Memory memory = db.Memories.Find(id);
            if (memory.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId)
            {
                db.Memories.Remove(memory);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}