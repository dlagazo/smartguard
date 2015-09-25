using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SmartGuardPortalv1.Filters;
using System.Web.Security;

namespace SmartGuardPortalv1.Controllers
{
    //[MembershipHttpAuthorizeAttribute(Roles="User")]
    public class MobileController : ApiController
    {
        // GET api/mobile
        //[MembershipHttpAuthorizeAttribute(Roles="User")]
        [BasicAuthorizeAttribute(1,Operations.Read, "User")]
        //public IQueryable<item> Get(string user)
        //public string Get(string user)
        public HttpResponseMessage Get(string user)
        {
            //string[] roles = System.Web.Security.Roles.Provider.GetRolesForUser(user);
            //string json_data = JsonConvert.SerializeObject(arr);
            List<item> items = new List<item>();
            items.Add(new item());
            items.Add(new item());
            return Request.CreateResponse(HttpStatusCode.OK, items);
            
        }

        class item
        {
            public int id = 0;
            public string value = "value";
        }

        // GET api/mobile/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/mobile
        public void Post([FromBody]string value)
        {
        }

        // PUT api/mobile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/mobile/5
        public void Delete(int id)
        {
        }
    }
}
