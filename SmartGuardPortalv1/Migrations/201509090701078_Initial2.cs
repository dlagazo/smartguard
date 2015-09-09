namespace SmartGuardPortalv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfile", "UserName", c => c.String(nullable: false, maxLength: 18));
            AlterColumn("dbo.UserProfile", "LastName", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfile", "LastName", c => c.String());
            AlterColumn("dbo.UserProfile", "UserName", c => c.String());
        }
    }
}
