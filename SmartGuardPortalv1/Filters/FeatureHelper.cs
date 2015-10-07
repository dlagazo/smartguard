using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartGuardPortalv1.Models;

namespace SmartGuardPortalv1.Filters
{
    
    public class FeatureHelper
    {
        private UsersContext db = new UsersContext();
        public FeatureHelper()
        {
            //context = new UsersContext();
        }

        public Boolean FeatureExists(string feature)
        {
            if (db.Features.First(i => i.FeatureName == "feature") != null)
            {
                return true;
            }
            else 
                return false;
        }


    }
}