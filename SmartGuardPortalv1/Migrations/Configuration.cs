namespace SmartGuardPortalv1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebMatrix.WebData;
    using WebMatrix.Data;
    using System.Web;
    using System.Web.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<SmartGuardPortalv1.Models.UsersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SmartGuardPortalv1.Models.UsersContext context)
        {

            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
            "UserProfile", "UserId", "UserName", autoCreateTables: true);
            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");
            if (!Roles.RoleExists("User"))
                Roles.CreateRole("User");
            if (!Roles.RoleExists("Contact"))
                Roles.CreateRole("Contact");
            

            if (!WebSecurity.UserExists("Smartguardadmin"))
            {

                WebSecurity.CreateUserAndAccount("Smartguardadmin", "password");
            }

            if (!Roles.GetRolesForUser("Smartguardadmin").Contains("Administrator"))
            {
                Roles.AddUsersToRoles(new[] { "Smartguardadmin" }, new[] { "Administrator" });
            }
           
        }
    }
}
