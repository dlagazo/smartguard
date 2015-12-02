using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Web.Http;
using SmartGuardPortalv1.Filters;
using SmartGuardPortalv1.Models;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace SmartGuardPortalv1.Controllers
{
    public class MobileImageUploadController : ApiController
    {
        private UsersContext db = new UsersContext();
        [BasicAuthorizeAttribute(1, Operations.Read, "User")]
        //public IQueryable<item> Get(string user)
        //public string Get(string user)
        public HttpResponseMessage Get()
        {
            
            //string[] roles = System.Web.Security.Roles.Provider.GetRolesForUser(getUserCredential(Request.Headers.Authorization.ToString()));
            //string json_data = JsonConvert.SerializeObject(arr);
            //List<Response> responses = new List<Response>();
            //responses.Add(new Response("Result", "Success"));
             
            
            int userId = (int)WebMatrix.WebData.WebSecurity.GetUserId(getUserCredential(Request.Headers.Authorization.ToString()));
            
            
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
            //responses.Add(new Response("Name", firstname + " " + lastname));
            
            //string[] roles = System.Web.Security.Roles.Provider.GetRolesForUser(user);
            //string json_data = JsonConvert.SerializeObject(arr);
            GeoLocation geo = db.GeoLocations.First(i=> i.fkUserId == userId);
            //contacts = db.Contacts.Where(i => i.fkUserId == userId).ToList();
            
           



            return Request.CreateResponse(HttpStatusCode.OK, geo);

        }

        [HttpPost, Route("api/upload")]
        public async Task<IHttpActionResult> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                var buffer = await file.ReadAsByteArrayAsync();
                //Do whatever you want with filename and its binaray data.
                string path = Path.Combine("~/Content/camera", Path.GetFileName(filename));
                
                if (System.IO.File.Exists(path))
                {
                    //ViewBag.Message = "The File Already Exists in System";
                }
                else
                {
                    File.WriteAllBytes(path, buffer);
                    //file.SaveAs(path);
                }
            }

            return Ok();
        }

        /*

        [HttpPost]
        //public HttpResponseMessage Set(GeoLocation geo)
        public HttpResponseMessage Set(ChargeData charge)
        {
            
            int userId = (int)WebMatrix.WebData.WebSecurity.GetUserId(getUserCredential(Request.Headers.Authorization.ToString()));
            

            if (ModelState.IsValid)
            {
                
                ChargeData temp = db.Charges.Where(i => i.fkUserId == userId).First();
                temp.ChargePct = charge.ChargePct;
                temp.ChargeTimeStamp = charge.ChargeTimeStamp;
                //charge.fkUserId = userId;
                //db..Add(temp);


                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                
                
                return Request.CreateResponse(HttpStatusCode.OK, charge);
                    
                
            }
            
            
            return Request.CreateResponse(HttpStatusCode.OK, charge);
        }
         */

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
