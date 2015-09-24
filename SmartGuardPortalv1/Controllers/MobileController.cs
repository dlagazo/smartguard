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
        public IEnumerable<string> Get(string user)
        {
            string[] roles = System.Web.Security.Roles.Provider.GetRolesForUser(user);
            return roles;
            
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
