namespace OnLinePrijaveMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanaKolonaError : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brokers", "Error", c => c.Boolean(nullable: false));
            AddColumn("dbo.Distributers", "Error", c => c.Boolean(nullable: false));
            AddColumn("dbo.MirovinskiFonds", "Error", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MirovinskiFonds", "Error");
            DropColumn("dbo.Distributers", "Error");
            DropColumn("dbo.Brokers", "Error");
        }
    }
}
