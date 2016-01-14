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

namespace SmartGuardPortalv1.Controllers
{
    public class MobileTrackController : ApiController
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

        [HttpPost]
        //public HttpResponseMessage Set(GeoLocation geo)
        public HttpResponseMessage Set(Track track)
        {
            
            int userId = (int)WebMatrix.WebData.WebSecurity.GetUserId(getUserCredential(Request.Headers.Authorization.ToString()));
            

            if (ModelState.IsValid)
            {

                Track temp = db.Tracks.Where(i => i.fkUserId == userId).FirstOrDefault();
                if (temp == null)
                {
                    track.fkUserId = userId;
                    db.Tracks.Add(track);
                    db.SaveChanges();
                }
                else
                {
                    temp.timestamp = track.timestamp;
                    temp.TrackLat = track.TrackLat;
                    temp.TrackLong = track.TrackLong;
                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();

                }

                LocateStatus status = db.LocateDevicesStatus.Where(i => i.fkUserId == userId).FirstOrDefault();

                if(status == null)
                {
                    Response resp = new Response("status", "0");
                    
                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
                else
                {
                    if (status.isMissing)
                    {
                        UserProfile profile = db.UserProfiles.Where(i => i.UserId == userId).FirstOrDefault();
                        String name = profile.FirstName + " " + profile.LastName;

                        String response = "This is the property of " + name + ". Please call the owner's primary contact person if you found this watch.";

                        Response resp = new Response(response, "1");
                        return Request.CreateResponse(HttpStatusCode.OK, resp);
                    }
                    else
                    {

                        Response resp = new Response("status", "0");
                        return Request.CreateResponse(HttpStatusCode.OK, resp);
                    }
                }
                
                
                
                
                    
                
            }
            
            
            return Request.CreateResponse(HttpStatusCode.OK, 0);
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
