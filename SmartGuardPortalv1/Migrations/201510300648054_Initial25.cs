namespace SmartGuardPortalv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial25 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeoLocationTable",
                c => new
                    {
                        GeoLocationId = c.Int(nullable: false, identity: true),
                        GeoLocationLat = c.String(),
                        GeoLocationLong = c.String(),
                        GeoLocationTimeStamp = c.DateTime(nullable: false),
                        fkUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GeoLocationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GeoLocationTable");
        }
    }
}
