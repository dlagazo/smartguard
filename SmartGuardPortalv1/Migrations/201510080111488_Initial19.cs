namespace SmartGuardPortalv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial19 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemoryTable",
                c => new
                    {
                        MemoryId = c.Int(nullable: false, identity: true),
                        MemoryName = c.String(),
                        fkUserId = c.Int(nullable: false),
                        MemoryDate = c.DateTime(nullable: false),
                        MemoryFreq = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MemoryId);
            
            CreateTable(
                "dbo.PlacesTable",
                c => new
                    {
                        PlaceId = c.Int(nullable: false, identity: true),
                        PlaceName = c.String(),
                        fkUserId = c.Int(nullable: false),
                        PlaceLat = c.String(),
                        PlaceLong = c.String(),
                    })
                .PrimaryKey(t => t.PlaceId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PlacesTable");
            DropTable("dbo.MemoryTable");
        }
    }
}
