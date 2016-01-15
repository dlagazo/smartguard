using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using SmartGuardPortalv1.Filters;
using SmartGuardPortalv1.Models;
using System.Net.Mail;
using System.Net;
using System.Data.Entity;


namespace SmartGuardPortalv1.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        private UsersContext db = new UsersContext();
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.Title = "Login";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult MobileLogin(LoginModel model, string returnUrl)
        {
            return null;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            ViewBag.Title = "Login";
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                //return RedirectToLocal(returnUrl);
                if(Roles.GetRolesForUser(model.UserName).Contains("User"))
                    return RedirectToAction("Module", "Home", new  { userName = WebSecurity.CurrentUserName });
                else if (Roles.GetRolesForUser(model.UserName).Contains("Contact"))
                {
                    //int userId = WebSecurity.GetUserId(model.UserName);
                    //tring mySched = db.ContactSchedules.Where(i => i.fkUserId == userId).First().ContactSchedules;
                    
                        return RedirectToAction("Contact", "Home", new { userName = WebSecurity.CurrentUserName });
                    
                }
                else if (Roles.GetRolesForUser(model.UserName).Contains("Administrator"))
                    return RedirectToAction("Administrator", "Home", new { userName = WebSecurity.CurrentUserName });
                else if (Roles.GetRolesForUser(model.UserName).Contains("ContentAdministrator"))
                    return RedirectToAction("Content", "Home", new { userName = WebSecurity.CurrentUserName });
                else if (Roles.GetRolesForUser(model.UserName).Contains("LocalAdministrator"))
                    return RedirectToAction("Local", "Home", new { userName = WebSecurity.CurrentUserName });
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult AdminAccount()
        {
            ViewBag.Title = "Register";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AdminAccount(RegisterModel model)
        {
            ViewBag.Title = "Register";
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.UserType == 2)
                    {
                        WebSecurity.CreateUserAndAccount(model.UserName, "smartguard",
                        propertyValues: new
                        {
                            LastName = model.LastName,
                            Country = model.Country,
                            FirstName = model.FirstName,
                            Email = model.Email
                        });
                        if (!Roles.GetRolesForUser(model.UserName).Contains("ContentAdministrator"))
                        {
                            Roles.AddUsersToRoles(new[] { model.UserName }, new[] { "ContentAdministrator" });
                        }
                        
                        
                        db.SaveChanges();
                        return RedirectToAction("Login", "Account");
                    }

                    else if (model.UserType == 3)
                    {
                        WebSecurity.CreateUserAndAccount(model.UserName, "smartguard",
                        propertyValues: new
                        {
                            LastName = model.LastName,
                            Country = model.Country,
                            FirstName = model.FirstName,
                            Email = model.Email
                        });
                        if (!Roles.GetRolesForUser(model.UserName).Contains("LocalAdministrator"))
                        {
                            Roles.AddUsersToRoles(new[] { model.UserName }, new[] { "LocalAdministrator" });
                        }


                        db.SaveChanges();
                        return RedirectToAction("Login", "Account");
                    }
                    if (model.UserType == 4)
                    {
                        WebSecurity.CreateUserAndAccount(model.UserName, "smartguard",
                        propertyValues: new
                        {
                            LastName = model.LastName,
                            Country = model.Country,
                            FirstName = model.FirstName,
                            Email = model.Email
                        });
                        if (!Roles.GetRolesForUser(model.UserName).Contains("Administrator"))
                        {
                            Roles.AddUsersToRoles(new[] { model.UserName }, new[] { "Administrator" });
                        }


                        db.SaveChanges();
                        return RedirectToAction("Login", "Account");
                    }

                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            return View(model);
        }
        
        [AllowAnonymous]
        public ActionResult Forgot()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Reset(RegisterModel model)
        {
            

            if (model.Email != null)
            {
                UserProfile profile = db.UserProfiles.Where(i => i.Email == model.Email).FirstOrDefault();
                //string username = Membership.GetUserNameByEmail(model.Email);
                
                if(profile != null)
                {
                    var token = WebSecurity.GeneratePasswordResetToken(profile.UserName);
                    // create a link with this token and send email
                    string password = Membership.GeneratePassword(12, 0);
                    // link directed to an action with form to capture password
                    WebSecurity.ResetPassword(token, password);

                    var emailMessage = new SendGrid.SendGridMessage();
                    emailMessage.From = new MailAddress("mailer-no-reply@smartguard");
                    emailMessage.AddTo(model.Email);

                    

                    emailMessage.Subject = "Smart Guard temporary password";
                    emailMessage.Html =  
                        "<p>You have successfully reset your password. Your temporary password is: </p> <br/><br/>" +
                        
                        "<p>" + password + "</p><br/><br/>" +

                        "<p>Please log in using your temporary password and change it immediately.</p>" +
                        "<p>If you did not reset your password, please report it to us immediately.</p>" +

                        "<br/><br/>Regards, <br/> Smart Guard Team";

                    sendEmail(emailMessage);
                }
                
                
            }


            return RedirectToAction("Index", "Home");
        }

        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id = 0)
        {
            int userId = WebSecurity.CurrentUserId;
            UserProfile profile = db.UserProfiles.Find(userId);
            UserInformation info = db.UserInfos.Where(i => i.fkUserId == userId).FirstOrDefault();
            RegisterModel reg = new RegisterModel();
            reg.Address = info.Address;
            reg.BirthDate = info.BirthDate;
            reg.City = info.City;
            reg.Country = profile.Country;
            reg.Email = profile.Email;
            reg.FirstName = profile.FirstName;
            reg.LastName = profile.LastName;
            reg.Phone = info.Phone;
            reg.UserName = profile.UserName;
            reg.Zip = info.Zip;
            reg.FkTitle = info.FkTitle;
            reg.Gender = info.Gender;
            reg.Hand = info.Hand;
            return View(reg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegisterModel model)
        {
            UserProfile profile = new UserProfile();
            UserInformation info = db.UserInfos.Where(i => i.fkUserId == WebSecurity.CurrentUserId).FirstOrDefault();

            db.UserInfos.Remove(info);
            

            profile.Country = model.Country;
            profile.FirstName = model.FirstName;
            profile.LastName = model.LastName;
            profile.UserName = WebSecurity.CurrentUserName;
            profile.UserId = WebSecurity.CurrentUserId;
            profile.Email = model.Email;

            info = new UserInformation();
            info.Address = model.Address;
            info.BirthDate = model.BirthDate;
            info.City = model.City;
            info.FkTitle = model.FkTitle;
            info.fkUserId = WebSecurity.CurrentUserId;
            info.Gender = model.Gender;
            info.Hand = model.Hand;
            info.Phone = model.Phone;
            info.Zip = model.Zip;



                db.UserInfos.Add(info);
                //db.Entry(info).State = EntityState.Modified;
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile", "Account", null);
            
            
            return RedirectToAction("Profile", "Account");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.Title = "Register";
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            ViewBag.Title = "Register";
            if (ModelState.IsValid)
            {
                // Attempt to register the user

                //Random temporary password generator
                string tempPassword = System.Web.Security.Membership.GeneratePassword(12,0);
                try
                {
                    
                    WebSecurity.CreateUserAndAccount(model.UserName, tempPassword, 
                        propertyValues: new { LastName = model.LastName, Country = model.Country,
                            FirstName = model.FirstName, Email = model.Email});
                            /*
                            FkTitle = model.FkTitle, BirthDate = model.BirthDate, ,
                            Phone = model.Phone, Address = model.Address, City = model.City, Country = model.Country,
                            Zip = model.Zip, Gender = model.Gender, Hand = model.Hand});
                             */
                    var emailMessage = new SendGrid.SendGridMessage();
                    emailMessage.From = new MailAddress("mailer-no-reply@smartguard");
                    emailMessage.AddTo(model.Email);

                    string salutation = "";
                    if (model.FkTitle == 0)
                        salutation = "Mr.";
                    else if (model.FkTitle == 1)
                        salutation = "Ms.";
                    else if (model.FkTitle == 2)
                        salutation = "Mrs.";
                    else if (model.FkTitle == 3)
                        salutation = "Dr.";

                    emailMessage.Subject = "Smart Guard temporary password";
                    emailMessage.Html = "Dear " + salutation + " " + model.LastName + ", <br/><br/>" +
                        "<p>Thank you for registering. Your registration information:</p> <br/><br/>" +
                        "<p>username: " + model.UserName + "</p><br/>" +
                        "<p>password: " + tempPassword + "</p><br/><br/>" +

                        "<p>Please log in using your temporary password and change it immediately.</p>" +
                        "<p>If you did not register, please disregard this message.</p>" + 

                        "<br/><br/>Regards, <br/> Smart Guard Team";

                    sendEmail(emailMessage);
                    //WebSecurity.Login(model.UserName, tempPassword);
                    if (model.UserType == 0)
                    {
                        if (!Roles.GetRolesForUser(model.UserName).Contains("User"))
                        {
                            Roles.AddUsersToRoles(new[] { model.UserName }, new[] { "User" });
                        }
                        UserInformation ui = new UserInformation();
                        ui.Address = model.Address;
                        ui.BirthDate = model.BirthDate;
                        ui.City = model.City;
                        ui.FkTitle = model.FkTitle;
                        ui.fkUserId = WebSecurity.GetUserId(model.UserName);
                        ui.Gender = model.Gender;
                        ui.Hand = model.Hand;
                        ui.Phone = model.Phone;
                        ui.Zip = model.Zip;
                        db.UserInfos.Add(ui);

                        //db.SaveChanges();

                        ChargeData charge = new ChargeData();
                        charge.ChargePct = 100;
                        charge.fkUserId = WebSecurity.GetUserId(model.UserName);
                        charge.ChargeTimeStamp = System.DateTime.Now;

                        Place place = new Place();
                        place.fkUserId = WebSecurity.GetUserId(model.UserName);
                        place.PlaceName = "Home";
                        place.PlaceLat = "48.4620863";
                        place.PlaceLong = "13.8696551";

                        Contact contact = new Contact();
                        contact.Email = "primary@smartguard.com";
                        contact.FirstName = "Fallback";
                        contact.LastName = "";
                        contact.Rank = 1;
                        contact.Relationship = "Emergency";
                        contact.Mobile = "911";
                        contact.fkUserId = WebSecurity.GetUserId(model.UserName);

                        Memory wake = new Memory();
                        wake.fkUserId = WebSecurity.GetUserId(model.UserName);
                        
                        wake.MemoryFreq = 0;
                        wake.MemoryInstructions = "Please input your wake up settings";
                        wake.MemoryName = "Wake";
                        wake.MemoryDates = "Sun Dec 06 2015 08:00:54 GMT+0800,Mon Dec 07 2015 08:00:54 GMT+0800,Tue Dec 08 2015 08:00:54 GMT+0800,Wed Dec 09 2015 08:00:54 GMT+0800,Thu Dec 10 2015 08:00:54 GMT+0800,Fri Dec 11 2015 08:00:54 GMT+0800,Sat Dec 12 2015 08:00:54 GMT+0800,";
                        wake.MemoryType = 0;

                        Memory sleep = new Memory();
                        sleep.fkUserId = WebSecurity.GetUserId(model.UserName);
                        sleep.MemoryType = 0;
                        sleep.MemoryFreq = 0;
                        sleep.MemoryInstructions = "Please input your sleep settings";
                        sleep.MemoryName = "Sleep";
                        sleep.MemoryDates = "Sun Dec 06 2015 20:00:01 GMT+0800,Mon Dec 07 2015 20:00:01 GMT+0800,Tue Dec 08 2015 20:00:01 GMT+0800,Wed Dec 09 2015 20:00:01 GMT+0800,Thu Dec 10 2015 20:00:01 GMT+0800,Fri Dec 11 2015 20:00:01 GMT+0800,Sat Dec 12 2015 20:00:01 GMT+0800,";

                        Memory fitminutes = new Memory();
                        fitminutes.fkUserId = WebSecurity.GetUserId(model.UserName);
                        
                        fitminutes.MemoryFreq = 0;
                        fitminutes.MemoryInstructions = "Please input your sleep settings";
                        fitminutes.MemoryName = "Sleep";
                        fitminutes.MemoryDates = "Sun Dec 06 2015 10:00:00 GMT+0800,Mon Dec 07 2015 10:00:01 GMT+0800,Tue Dec 08 2015 10:00:01 GMT+0800,Wed Dec 09 2015 10:00:01 GMT+0800,Thu Dec 10 2015 10:00:01 GMT+0800,Fri Dec 11 2015 10:00:01 GMT+0800,Sat Dec 12 2015 10:00:01 GMT+0800,";

                        VitalInfo name = new VitalInfo();
                        name.fkUserId = WebSecurity.GetUserId(model.UserName);
                        name.Title = "Name";
                        name.Value = model.FirstName + " " + model.LastName;

                        VitalInfo address = new VitalInfo();
                        address.fkUserId = WebSecurity.GetUserId(model.UserName);
                        address.Title = "Address";
                        address.Value = ui.Address;

                        VitalInfo birth = new VitalInfo();
                        birth.fkUserId = WebSecurity.GetUserId(model.UserName);
                        birth.Title = "Birthdate";
                        birth.Value = ui.BirthDate.ToShortDateString();

                        VitalInfo weight = new VitalInfo();
                        weight.fkUserId = WebSecurity.GetUserId(model.UserName);
                        weight.Title = "Weight";
                        weight.Value = "0 lbs";

                        VitalInfo height = new VitalInfo();
                        height.fkUserId = WebSecurity.GetUserId(model.UserName);
                        height.Title = "Height";
                        height.Value = "5'5''";
                        

                        db.Charges.Add(charge);
                        db.Contacts.Add(contact);
                        db.Memories.Add(wake);
                        db.Memories.Add(sleep);
                        db.Memories.Add(fitminutes);
                        db.Places.Add(place);
                        db.VitalInfos.Add(name);
                        db.VitalInfos.Add(address);
                        db.VitalInfos.Add(birth);
                        db.VitalInfos.Add(weight);
                        db.VitalInfos.Add(height);
                        db.SaveChanges();
                    }
                    else if (model.UserType == 1)
                    {
                        if (!Roles.GetRolesForUser(model.UserName).Contains("Contact"))
                        {
                            Roles.AddUsersToRoles(new[] { model.UserName }, new[] { "Contact" });
                        }
                        UserInformation ui = new UserInformation();
                        ui.Address = model.Address;
                        ui.BirthDate = model.BirthDate;
                        ui.City = model.City;
                        ui.FkTitle = model.FkTitle;
                        ui.fkUserId = WebSecurity.GetUserId(model.UserName);
                        ui.Gender = model.Gender;
                        //ui.Hand = model.Hand;
                        ui.Phone = model.Phone;
                        ui.Zip = model.Zip;

                        ContactSchedule cs = new ContactSchedule();
                        cs.fkUserId = WebSecurity.GetUserId(model.UserName);

                        db.ContactSchedules.Add(cs);
                        db.UserInfos.Add(ui);
                        db.SaveChanges();
                    }



                    return RedirectToAction("Email", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public Boolean sendEmail(SendGrid.SendGridMessage message)
        {
            var username = "azure_73e080e9b84e851c07cbce4a6d9c166d@azure.com";//"azure_569256974a694fa7ba6f292deb63d995@azure.com";
            var password = "5fZ66AWPEzHA1TI";//"hleUwJY2m46FV3M";
            var credentials = new NetworkCredential(username, password);

            // Create an Web transport for sending email.
            var transportWeb = new SendGrid.Web(credentials);

            // Send the email.
            // You can also use the **DeliverAsync** method, which returns an awaitable task.
            transportWeb.DeliverAsync(message);

            return true;
        }
        //
        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        [Authorize]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.Title = "My Account";
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        [Authorize]
        public ActionResult Profile()
        {
            ViewBag.Title = "My Profile";
            UserProfile up = db.UserProfiles.Where(i => i.UserId == (int)WebSecurity.CurrentUserId).First();
            return View(up);
        }
        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            ViewBag.Title = "My Account";
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            
            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
