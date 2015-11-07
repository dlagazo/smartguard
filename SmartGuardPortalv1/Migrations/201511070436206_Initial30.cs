namespace SmartGuardPortalv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial30 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedicalTable",
                c => new
                    {
                        MedicalId = c.Int(nullable: false, identity: true),
                        fkUserId = c.Int(nullable: false),
                        description = c.String(),
                        accessLevel = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        MedicalFile = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.MedicalId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MedicalTable");
        }
    }
}
