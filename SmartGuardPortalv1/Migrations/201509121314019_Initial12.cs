namespace SmartGuardPortalv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial12 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProfile", "UserType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfile", "UserType", c => c.Short(nullable: false));
        }
    }
}
