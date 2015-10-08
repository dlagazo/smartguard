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
        
        public ActionResult Index()
        {
            /*
            if(WebSecurity.CurrentUserName!= null)
            {
                if(Roles.GetRolesForUser(WebSecurity.CurrentUserName).Contains("Administrator"))
                    return RedirectToAction("Welcome", "Home", new  { userName = WebSecurity.CurrentUserName });
                else if (Roles.GetRolesForUser(WebSecurity.CurrentUserName).Contains("User"))
                    return RedirectToAction("Module", "Home", new { userName = WebSecurity.CurrentUserName });
                if (Roles.GetRolesForUser(WebSecurity.CurrentUserName).Contains("Contact"))
                    return RedirectToAction("FallModule", "Home", new { userName = WebSecurity.CurrentUserName });
            }
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            */
            return View();
        }

        public ActionResult Landing()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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
        public ActionResult Authorized()
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
