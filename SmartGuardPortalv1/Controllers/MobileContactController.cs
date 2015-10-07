using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SmartGuardPortalv1.Filters;
using SmartGuardPortalv1.Models;

namespace SmartGuardPortalv1.Controllers
{
    public class MobileContactController : ApiController
    {
        private UsersContext db = new UsersContext();
        [BasicAuthorizeAttribute(1, Operations.Read, "User")]
        //public IQueryable<item> Get(string user)
        //public string Get(string user)
        public HttpResponseMessage Get(string user)
        {
            int userId = (int)WebMatrix.WebData.WebSecurity.GetUserId(user);
            //string[] roles = System.Web.Security.Roles.Provider.GetRolesForUser(user);
            //string json_data = JsonConvert.SerializeObject(arr);
            List<Contact> contacts = new List<Contact>();
            contacts = db.Contacts.Where(i => i.fkUserId == userId).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, contacts);

        }

    }
}
