namespace SmartGuardPortalv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial29 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.MedicalTable");
        }
        
        public override void Down()
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
                    })
                .PrimaryKey(t => t.MedicalId);
            
        }
    }
}
