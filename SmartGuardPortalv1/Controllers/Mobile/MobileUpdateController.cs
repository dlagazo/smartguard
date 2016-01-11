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

namespace SmartGuardPortalv1.Controllers
{
    public class MobileUpdateController : ApiController
    {
        

        
        [BasicAuthorizeAttribute(1, Operations.Read, "User")]
        //public IQueryable<item> Get(string user)
        //public string Get(string user)
        public HttpResponseMessage Get()
        {
            /*
            if (!WebMatrix.WebData.WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            }
            UsersContext db = new UsersContext();
            byte[] apk = db.AppBuilds.Where(i => i.isActive == true).OrderByDescending(i => i.timeStamp).FirstOrDefault().Apk;
            MemoryStream ms = new MemoryStream(apk);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            //response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            return response;*/
            return null;
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
