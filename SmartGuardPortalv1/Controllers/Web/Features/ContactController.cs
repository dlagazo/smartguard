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
    public class ContactController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Feature/

        public ActionResult Index()
        {
            return View(db.Contacts.Where(i => i.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId).ToList());
        }

        //
        // GET: /Feature/Details/5

        public ActionResult Details(int id = 0)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            else if (contact.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId)
                return View(contact);
            else
                return HttpNotFound();
            
        }

        public ActionResult MoveDown(int id = 0)
        {
            Contact contact = db.Contacts.Find(id);
            int rank = contact.Rank;

            Contact tempContact = db.Contacts.FirstOrDefault(i => i.Rank == (rank + 1));
            tempContact.Rank = (short) rank;

            contact.Rank = (short) (rank + 1);

            db.Entry(tempContact).State = EntityState.Modified;
            db.Entry(contact).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult MoveUp(int id = 0)
        {
            Contact contact = db.Contacts.Find(id);
            int rank = contact.Rank;

            Contact tempContact = db.Contacts.FirstOrDefault(i => i.Rank == (rank-1));
            tempContact.Rank = (short)rank;

            contact.Rank = (short)(rank - 1);

            db.Entry(tempContact).State = EntityState.Modified;
            db.Entry(contact).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
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
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                UserProfile up = db.UserProfiles.Where(i => i.Email == contact.Email).FirstOrDefault();
                if (up != null)
                {
                    contact.FirstName = up.FirstName;
                    contact.LastName = up.LastName;
                    if (contact.Mobile == null)
                    {
                        UserInformation ui = db.UserInfos.Where(i => i.fkUserId == up.UserId).FirstOrDefault();
                        contact.Mobile = ui.Phone;
                    }
                }
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        //
        // GET: /Feature/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            else if (contact.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId)
                return View(contact);
            else
                return HttpNotFound();
        }

        //
        // POST: /Feature/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                UserProfile up = db.UserProfiles.Where(i => i.Email == contact.Email).FirstOrDefault();
                if (up != null)
                {
                    contact.FirstName = up.FirstName;
                    contact.LastName = up.LastName;
                    if (contact.Mobile == null)
                    {
                        UserInformation ui = db.UserInfos.Where(i => i.fkUserId == up.UserId).FirstOrDefault();
                        contact.Mobile = ui.Phone;
                    }
                }
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        //
        // GET: /Feature/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            else if (contact.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId)
                return View(contact);
            else
                return HttpNotFound();
        }

        //
        // POST: /Feature/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId)
            {
                db.Contacts.Remove(contact);
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