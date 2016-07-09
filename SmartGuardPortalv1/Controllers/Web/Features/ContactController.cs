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
            //int userId = WebMatrix.WebData.WebSecurity.CurrentUserId;
            Contact contact = db.Contacts.Where(i => i.ContactId == id).First();
            //ContactSchedule contactSchedule = db.ContactSchedules.Where(i => i. == id).First();
            if (contact == null)
            {
                return HttpNotFound();
                
            }
            
            return View(contact);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Contact");
            }
            return View(contact);
        }

        public ActionResult MoveDown(int id = 0)
        {
            Contact contact = db.Contacts.Find(id);
            int userId = contact.fkUserId;

            List<Contact> contacts = db.Contacts.Where(i => i.fkUserId == userId).OrderBy(i => i.Rank).ToList();
            short counter = 0;
            foreach (Contact tempContact in contacts)
            {
                tempContact.Rank = counter;
                db.Entry(tempContact).State = EntityState.Modified;
                //db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                counter++;
            }

            //short counter = 0;

            if (contact.Rank < counter)
            {
                Contact cont = contacts.Where(i => i.ContactId == id).FirstOrDefault();
                Contact temp = contacts.Where(i => i.Rank == (cont.Rank + 1)).FirstOrDefault();
                temp.Rank = (short)(cont.Rank);
                //temp.Rank = contact.Rank;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();

                cont.Rank = (short)(cont.Rank + 1);
                db.Entry(cont).State = EntityState.Modified;
                db.SaveChanges();
            }
         


            return RedirectToAction("Index");
        }

        public ActionResult MoveUp(int id = 0)
        {
            Contact contact = db.Contacts.Find(id);
            int userId = contact.fkUserId;

            List<Contact> contacts = db.Contacts.Where(i => i.fkUserId == userId).OrderBy(i => i.Rank).ToList();
            short counter = 0;
            foreach(Contact tempContact in contacts)
            {
                tempContact.Rank = counter;
                db.Entry(tempContact).State = EntityState.Modified;
                //db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                counter++;
            }

            //short counter = 0;

            if(contact.Rank > 0)
            {
                Contact cont = contacts.Where(i => i.ContactId == id).FirstOrDefault();
                Contact temp = contacts.Where(i => i.Rank == (cont.Rank-1)).FirstOrDefault();
                temp.Rank = (short)(cont.Rank);
                //temp.Rank = contact.Rank;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();

                cont.Rank = (short)(cont.Rank-1);
                db.Entry(cont).State = EntityState.Modified;
                db.SaveChanges();
            }

            
            /*
            
            */
            

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
                try
                {
                    SmartGuardPortalv1.Models.UserProfile prof = db.UserProfiles.Where(i => i.Email == contact.Email).FirstOrDefault();
                    if (prof != null)
                    {
                        db.Contacts.Add(contact);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        RegisterModel regMod = new RegisterModel();
                        regMod.Email = contact.Email;
                        regMod.FirstName = contact.FirstName;
                        regMod.LastName = contact.LastName;
                        regMod.UserName = contact.Email;
                        regMod.UserType = 1;
                        AccountController contr = new AccountController();
                        contr.Register(regMod);

                        db.Contacts.Add(contact);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex) { }
            
                
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
            else if (contact.ContactId == id)
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