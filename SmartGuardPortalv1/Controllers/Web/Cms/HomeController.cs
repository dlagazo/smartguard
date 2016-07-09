using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartGuardPortalv1.Filters;
using WebMatrix.WebData;
using System.Web.Security;
using SmartGuardPortalv1.Models;

namespace SmartGuardPortalv1.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        private UsersContext db = new UsersContext();

        public ActionResult Email()
        {
            return View();
        }

        public ActionResult Data()
        {
            return View();
        }
        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Index()
        {
            
            if(WebSecurity.IsAuthenticated)
            {
                if(Roles.GetRolesForUser(WebSecurity.CurrentUserName).Contains("Administrator"))
                    return RedirectToAction("Administrator", "Home", new  { userName = WebSecurity.CurrentUserName });
                else if (Roles.GetRolesForUser(WebSecurity.CurrentUserName).Contains("User"))
                    return RedirectToAction("Module", "Home", new { userName = WebSecurity.CurrentUserName });
                else if (Roles.GetRolesForUser(WebSecurity.CurrentUserName).Contains("Contact"))
                    return RedirectToAction("FallModule", "Home", new { userName = WebSecurity.CurrentUserName });
            }
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            
            return View();
        }

        [Authorize(Roles = "User")]
        public ActionResult Modules()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Landing()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [Authorize(Roles = "Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Wearing()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Overview()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Contact")]
        public ActionResult AllModules()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [Authorize(Roles = "Contact")]
        public ActionResult AllMyContacts()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [Authorize(Roles = "Contact")]
        public ActionResult AllMemory()
        {
            return View(db.Memories.Where(i => i.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId).Where(i => i.MemoryType == 0 || i.MemoryType == 2).ToList());
        }

        [Authorize(Roles = "Contact")]
        public ActionResult AllCoaching()
        {
            return View(db.Memories.Where(i => i.fkUserId == WebMatrix.WebData.WebSecurity.CurrentUserId).Where(i => i.MemoryType == 1 || i.MemoryType == 3 || i.MemoryType == 4).ToList());
        }

        [Authorize(Roles = "Contact")]
        public ActionResult AllNavigation()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [Authorize(Roles = "Contact")]
        public ActionResult AllContacts()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [Authorize(Roles = "Contact")]
        public ActionResult AllVital()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [Authorize(Roles = "Contact")]
        public ActionResult AllFall()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Emergency()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Door()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Updates()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult HomeAreaAlarm()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [Authorize(Roles = "User,Contact")]
        public ActionResult Communication()
        {
            

            return View();
        }

        [InitializeSimpleMembership]
        [Authorize(Roles = "User")]
        public ActionResult Welcome()
        {
            ViewBag.Title = "My Dashboard";
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [InitializeSimpleMembership]
        [Authorize(Roles="User")]
        public ActionResult Module()
        {
            try{

                ChargeData charge = db.Charges.Where(i => i.fkUserId == WebSecurity.CurrentUserId).FirstOrDefault();
                if (charge == null)
                {
                    ChargeData temp = new ChargeData();
                    temp.fkUserId = WebSecurity.CurrentUserId;
                    temp.ChargeTimeStamp = DateTime.Now;
                    temp.ChargePct = 100;

                    db.Charges.Add(temp);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {

            }
            
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [InitializeSimpleMembership]
        [Authorize(Roles = "User")]
        public ActionResult FallModule()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Administrator()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "LocalAdministrator")]
        public ActionResult Local()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "ContentAdministrator")]
        public ActionResult Content()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles="Contact")]
        public ActionResult ContactMain()
        {
            int userId = (int)WebSecurity.CurrentUserId;
            string email = db.UserProfiles.Where(i => i.UserId == userId).First().Email;
            List<Contact> myContacts = db.Contacts.Where(i => i.Email == email).ToList();
            List<UserProfile> users = new List<UserProfile>();
            foreach (Contact contact in myContacts)
            {
                users.Add(db.UserProfiles.Where(i => i.UserId == contact.fkUserId).First());
            }
            return View(users);
        }
    }
}
