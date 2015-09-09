namespace SmartGuardPortalv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProfile", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfile", "UserName", c => c.String(nullable: false, maxLength: 18));
        }
    }
}
