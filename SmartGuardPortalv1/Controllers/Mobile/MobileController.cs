using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SmartGuardPortalv1.Filters;
using System.Web.Security;
using System.Text;
using SmartGuardPortalv1.Models;

namespace SmartGuardPortalv1.Controllers
{
    //[MembershipHttpAuthorizeAttribute(Roles="User")]
    public class MobileController : ApiController
    {
        // GET api/mobile
       
        [BasicAuthorizeAttribute(1,Operations.Read, "User")]
        
        public HttpResponseMessage Get()
        {
            string[] roles = System.Web.Security.Roles.Provider.GetRolesForUser(getUserCredential(Request.Headers.Authorization.ToString()));
            //string json_data = JsonConvert.SerializeObject(arr);
            List<Response> responses = new List<Response>();
            responses.Add(new Response("Result", "Success"));
            responses.Add(new Response("Roles", roles[0]));
            


            return Request.CreateResponse(HttpStatusCode.OK, responses);
            
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
