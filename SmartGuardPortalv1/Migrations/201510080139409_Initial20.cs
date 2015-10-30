namespace SmartGuardPortalv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemoryTable", "MemoryInstructions", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemoryTable", "MemoryInstructions");
        }
    }
}
