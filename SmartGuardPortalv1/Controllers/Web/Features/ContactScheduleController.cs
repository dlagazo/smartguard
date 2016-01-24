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

        
        //
        // POST: /Feature/Create

        
        //
        // GET: /Feature/Edit/5

        public ActionResult Edit(int id = 0)
        {
            int userId = WebMatrix.WebData.WebSecurity.CurrentUserId;
            
            ContactSchedule contactSchedule = db.ContactSchedules.Where(i => i.fkUserId == id).First();
            if (contactSchedule == null)
            {
                return HttpNotFound();
            }
            else if(contactSchedule.fkUserId == userId)
                return View(contactSchedule);
            else
                return HttpNotFound();
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

        

        //
        // POST: /Feature/Delete/5

       
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}