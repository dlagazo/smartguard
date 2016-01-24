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
    
    [Authorize(Roles="User, Contact")]
    public class FallController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Feature/

        public ActionResult Index()
        {
            return View(db.Falls.Where(i => i.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId).ToList());
        }

        //
        // GET: /Feature/Details/5

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

        public ActionResult DeleteAll()
        {
            IEnumerable<Fall> falls = db.Falls.Where(i => i.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId);
            db.Falls.RemoveRange(falls);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /Feature/Delete/5

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