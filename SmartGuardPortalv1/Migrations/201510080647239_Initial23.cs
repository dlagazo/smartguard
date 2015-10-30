namespace SmartGuardPortalv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial23 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInformation",
                c => new
                    {
                        InfoId = c.Int(nullable: false, identity: true),
                        fkUserId = c.Int(nullable: false),
                        FkTitle = c.Short(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Phone = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Zip = c.String(),
                        Gender = c.Boolean(nullable: false),
                        Hand = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InfoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInformation");
        }
    }
}
