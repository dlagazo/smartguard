using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartGuardPortalv1.Models;
using WebMatrix.WebData;

namespace SmartGuardPortalv1.Controllers
{
    
    [Authorize(Roles="User")]
    public class TrackController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Feature/

        public ActionResult Index()
        {
            int userId = (int)WebSecurity.CurrentUserId;
    
            return View(db.Tracks.Where(i => i.fkUserId == userId).ToList());
        }

        public ActionResult LocateOn()
        {
            int userId = (int)WebSecurity.CurrentUserId;
            LocateStatus status = db.LocateDevicesStatus.Where(i => i.fkUserId == userId).FirstOrDefault();
            if(status == null)
            {
                LocateStatus temp = new LocateStatus();
                temp.fkUserId = userId;
                temp.isMissing = true;
                db.LocateDevicesStatus.Add(temp);
                db.SaveChanges();
            }
            else 
            {
                status.isMissing = true;
                db.Entry(status).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult LocateOff()
        {
            int userId = (int)WebSecurity.CurrentUserId;
            LocateStatus status = db.LocateDevicesStatus.Where(i => i.fkUserId == userId).FirstOrDefault();
            if (status == null)
            {
                LocateStatus temp = new LocateStatus();
                temp.fkUserId = userId;
                temp.isMissing = false;
                db.LocateDevicesStatus.Add(temp);
                db.SaveChanges();
            }
            else
            {
                status.isMissing = false;
                db.Entry(status).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Feature/Details/5

        

        public ActionResult Delete(int id = 0)
        {
            Track track = db.Tracks.Find(id);
            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        //
        // POST: /Feature/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Track track = db.Tracks.Find(id);
            db.Tracks.Remove(track);
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