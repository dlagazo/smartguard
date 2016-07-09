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
    
    
    public class FallController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Feature/
        [Authorize(Roles = "User, Contact")]
        public ActionResult Index()
        {
            if(User.IsInRole("User"))
                return View(db.Falls.Where(i => i.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId).ToList());
            else
            {
                int contactId = WebMatrix.WebData.WebSecurity.CurrentUserId;
                string contactEmail = db.UserProfiles.Where(i => i.UserId == contactId).FirstOrDefault().Email;
                if(contactEmail != null)
                {
                    int userId = db.Contacts.Where(i => i.Email == contactEmail).FirstOrDefault().fkUserId;
                    if(userId != null)
                        return View(db.Falls.Where(i => i.fkUserId == userId).ToList());
                }

                return View();
            }
        }

        


        //
        // GET: /Feature/Details/5
        [Authorize(Roles = "User")]
        public ActionResult Details(int id = 0)
        {
            Fall fall = db.Falls.Find(id);
            if (fall == null)
            {
                return HttpNotFound();
            }
            else if (fall.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId)
            {
                return View(fall);
            }
            else
                return HttpNotFound();
        }








        [Authorize(Roles = "User")]
        public ActionResult Delete(int id = 0)
        {
            Fall fall = db.Falls.Find(id);
            if (fall == null)
            {
                return HttpNotFound();
            }
            else if (fall.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId)
            {
                return View(fall);
            }
            else
                return HttpNotFound();
            
        }
        [Authorize(Roles = "User")]
        public ActionResult DeleteAll()
        {
            IEnumerable<Fall> falls = db.Falls.Where(i => i.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId);
            db.Falls.RemoveRange(falls);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /Feature/Delete/5
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fall fall = db.Falls.Find(id);
            db.Falls.Remove(fall);
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