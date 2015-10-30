namespace SmartGuardPortalv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial22 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProfile", "FkTitle");
            DropColumn("dbo.UserProfile", "BirthDate");
            DropColumn("dbo.UserProfile", "Phone");
            DropColumn("dbo.UserProfile", "Address");
            DropColumn("dbo.UserProfile", "City");
            DropColumn("dbo.UserProfile", "Zip");
            DropColumn("dbo.UserProfile", "Gender");
            DropColumn("dbo.UserProfile", "Hand");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfile", "Hand", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserProfile", "Gender", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserProfile", "Zip", c => c.String());
            AddColumn("dbo.UserProfile", "City", c => c.String());
            AddColumn("dbo.UserProfile", "Address", c => c.String());
            AddColumn("dbo.UserProfile", "Phone", c => c.String());
            AddColumn("dbo.UserProfile", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserProfile", "FkTitle", c => c.Short(nullable: false));
        }
    }
}
