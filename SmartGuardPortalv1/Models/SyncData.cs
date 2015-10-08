using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGuardPortalv1.Models
{
    public class SyncData
    {
        public List<Contact> contacts { get; set; }
        public List<Place> places { get; set; }
        public List<string> roles { get; set; }
        public List<Memory> memories {get; set;}
        public List<Subscription> subscriptions { get; set; }
        public List<Response> responses { get; set; }

        public SyncData(List<Contact> _contacts, List<Place> _places, List<string> _roles, List<Response> _responses)
        {
            contacts = _contacts;
            places = _places;
            roles = _roles;
            responses = _responses;
        }

    }

    public class Response
    {
        public string response = "id";
        public string value = "value";
        public Response(string resp, string val)
        {
            response = resp;
            value = val;
        }
    }
}