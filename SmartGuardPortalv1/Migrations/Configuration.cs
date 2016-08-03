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
            AutomaticMigrationDataLossAllowed = true;
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
            if (!Roles.RoleExists("LocalAdministrator"))
                Roles.CreateRole("LocalAdministrator");
            if (!Roles.RoleExists("ContentAdministrator"))
                Roles.CreateRole("ContentAdministrator");
            if (!Roles.RoleExists("Distributor"))
                Roles.CreateRole("Distributor");

            if (!WebSecurity.UserExists("administrator"))
            {

                WebSecurity.CreateUserAndAccount("administrator", "password");
            }

            if (!Roles.GetRolesForUser("administrator").Contains("Administrator"))
            {
                Roles.AddUsersToRoles(new[] { "administrator" }, new[] { "Administrator" });
            }

            if (!WebSecurity.UserExists("content-admin"))
            {

                WebSecurity.CreateUserAndAccount("content-admin", "password");
            }

            if (!Roles.GetRolesForUser("content-admin").Contains("ContentAdministrator"))
            {
                Roles.AddUsersToRoles(new[] { "content-admin" }, new[] { "ContentAdministrator" });
            }

           
        }
    }
}
