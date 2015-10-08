using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SmartGuardPortalv1.Filters;
using SmartGuardPortalv1.Models;
using System.Text;

namespace SmartGuardPortalv1.Controllers
{
    public class MobileContactController : ApiController
    {
        private UsersContext db = new UsersContext();
        [BasicAuthorizeAttribute(1, Operations.Read, "User")]
        //public IQueryable<item> Get(string user)
        //public string Get(string user)
        public HttpResponseMessage Get()
        {
            string[] roles = System.Web.Security.Roles.Provider.GetRolesForUser(getUserCredential(Request.Headers.Authorization.ToString()));
            //string json_data = JsonConvert.SerializeObject(arr);
            List<Response> responses = new List<Response>();
            responses.Add(new Response("Result", "Success"));
            
            
            int userId = (int)WebMatrix.WebData.WebSecurity.GetUserId(getUserCredential(Request.Headers.Authorization.ToString()));
            //string[] roles = System.Web.Security.Roles.Provider.GetRolesForUser(user);
            //string json_data = JsonConvert.SerializeObject(arr);
            List<Contact> contacts = new List<Contact>();
            contacts = db.Contacts.Where(i => i.fkUserId == userId).ToList();
            List<Place> places = new List<Place>();

            SyncData sd = new SyncData(contacts, places, roles.ToList(), responses);



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
