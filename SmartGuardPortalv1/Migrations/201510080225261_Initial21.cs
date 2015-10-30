namespace SmartGuardPortalv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial21 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CountryTable",
                c => new
                    {
                        CountryId = c.String(nullable: false, maxLength: 128),
                        CountryName = c.String(),
                        AdminRole = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CountryTable");
        }
    }
}
