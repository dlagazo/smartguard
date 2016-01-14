using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGuardPortalv1.Models
{
    public class SyncData
    {
        public List<MyContact> contacts { get; set; }
        public List<Place> places { get; set; }
        public List<string> roles { get; set; }
        public List<Memory> memories {get; set;}
        public List<Subscription> subscriptions { get; set; }
        public List<Response> responses { get; set; }
        public List<Reminder> reminders { get; set; }
        public FallProfile fallProfile { get; set; }
        public List<VitalInfo> vitals { get; set; }
        public String version { get; set; }

        private UsersContext db = new UsersContext();

        public SyncData(List<Contact> _contacts, List<Place> _places, List<string> _roles, 
            List<Memory> _memories, List<Reminder> _reminders, List<Response> _responses, FallProfile _fp, 
            List<VitalInfo> _vitals, String _version)
        {
            contacts = new List<MyContact>();
            int userId;
            String sched = "";
            bool canContact = false;
            foreach (Contact cont in _contacts)
            {
                try
                {
                    userId = db.UserProfiles.Where(i => i.Email == cont.Email).FirstOrDefault().UserId;
                    sched = db.ContactSchedules.Where(i => i.fkUserId == userId).FirstOrDefault().ContactSchedules;
                    canContact = db.ContactSchedules.Where(i => i.fkUserId == userId).FirstOrDefault().canContactOutsideSched;
                }
                catch (Exception ex)
                {
                }
                MyContact myCont = new MyContact(cont, sched, canContact);
                contacts.Add(myCont);
                sched = "";
            }
            places = _places;
            roles = _roles;
            memories = _memories;
            reminders = _reminders;
            responses = _responses;
            fallProfile = _fp;
            vitals = _vitals;
            version = _version;

        }

    }

    public class MyContact
    {
        public int ContactId;
        public string FirstName, LastName, Email, Mobile, Relationship;
        public int Rank;
        public String schedule;
        public int fkUserId;
        public int type;
        public int canContactOutside;
        

        public MyContact(Contact _contact, String _sched, bool _canContact)
        {
            ContactId = _contact.ContactId;
            FirstName = _contact.FirstName;
            LastName = _contact.LastName;
            Mobile = _contact.Mobile;
            Email = _contact.Email;
            Relationship = _contact.Relationship;
            if (_contact.type)
                type = 1;
            else
                type = 0;
            if (_canContact)
                canContactOutside = 1;
            else
                canContactOutside = 0;
            Rank = _contact.Rank;
            schedule = _sched;
            fkUserId = _contact.fkUserId;
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