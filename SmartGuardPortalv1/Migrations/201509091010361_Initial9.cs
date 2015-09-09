namespace SmartGuardPortalv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "UserType", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "UserType");
        }
    }
}
