using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SmartGuardPortalv1.Filters;
using SmartGuardPortalv1.Models;
using System.Text;
using WebMatrix.WebData;
using System.Data.Entity;

namespace SmartGuardPortalv1.Controllers
{
    public class MobileContactController : ApiController
    {
        

        
        [BasicAuthorizeAttribute(1, Operations.Read, "User")]
        //public IQueryable<item> Get(string user)
        //public string Get(string user)
        public HttpResponseMessage Get()
        {
            if (!WebMatrix.WebData.WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            }
            UsersContext db = new UsersContext();
            string[] roles = System.Web.Security.Roles.Provider.GetRolesForUser(getUserCredential(Request.Headers.Authorization.ToString()));
            
            //string json_data = JsonConvert.SerializeObject(arr);
            List<Response> responses = new List<Response>();
            responses.Add(new Response("Result", "Success"));
            string serial = Request.Headers.GetValues("serial").ToArray()[0]; 
            
            int userId = (int)WebMatrix.WebData.WebSecurity.GetUserId(getUserCredential(Request.Headers.Authorization.ToString()));
            try
            {
                Inventory inv = db.Inventories.Find(serial);
                if (inv == null)
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, new Exception("invalid serial key: " + serial));
                else if(inv.fkUserId != userId && inv.fkUserId != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, new Exception("invalid serial key: " + serial));

                }
                else if(inv.fkUserId == null)
                {
                    Inventory isRegistered = db.Inventories.FirstOrDefault(i => i.fkUserId == userId);
                    if(isRegistered == null)
                    {
                        inv.fkUserId = userId;
                        db.Entry(inv).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.Forbidden, new Exception("user already registered to " + isRegistered.serialKey));

                    }
                    
                }
                
            }
            catch (Exception ex) { }
            
            
            //int title = db.UserInfos.Where(i => i.fkUserId == userId).First().FkTitle;
            string firstname = db.UserProfiles.Where(i => i.UserId == userId).First().FirstName;
            string lastname = db.UserProfiles.Where(i => i.UserId == userId).First().LastName;
            //string salute;
            /*
            if(title == 1)
                salute = "Mr.";
            else if(title == 2)
                salute = "Mrs.";
            else if(title == 3)
                salute = "Ms.";
            else if(title == 4)
                salute = "Dr.";
            else
                salute = " ";
             * */
            responses.Add(new Response("Name", firstname + " " + lastname));
            
            //string[] roles = System.Web.Security.Roles.Provider.GetRolesForUser(user);
            //string json_data = JsonConvert.SerializeObject(arr);
            List<Contact> contacts = new List<Contact>();
            contacts = db.Contacts.Where(i => i.fkUserId == userId).OrderBy(i => i.Rank).ToList();
            
            List<Place> places = new List<Place>();
            places = db.Places.Where(i => i.fkUserId == userId).ToList();

            List<Memory> memories = new List<Memory>();
            memories = db.Memories.Where(i => i.fkUserId == userId).ToList();

            List<Reminder> reminders = new List<Reminder>();
            reminders = db.Reminders.Where(i => i.fkUserId == userId).ToList();

            FallProfile fallProfile = db.FallProfiles.Where(i => i.isActive == true).FirstOrDefault();

            List<VitalInfo> vitals = db.VitalInfos.Where(i => i.fkUserId == userId).ToList();
            String version = db.AppBuilds.Where(i => i.isActive == true).OrderByDescending(i => i.timeStamp).FirstOrDefault().AppVersion;


            SyncData sd = new SyncData(contacts, places, roles.ToList(), memories, reminders, responses, fallProfile, vitals, version);



            return Request.CreateResponse(HttpStatusCode.OK, sd);

        }

        public string getUserCredential(string auth)
        {
            string[] splitAuth = auth.Split(' ');
            byte[] data = Convert.FromBase64String(splitAuth[1]);
            string decodedString = Encoding.UTF8.GetString(data);
            string[] credentials = decodedString.Split(':');
            return credentials[0];
        }

    }
}
