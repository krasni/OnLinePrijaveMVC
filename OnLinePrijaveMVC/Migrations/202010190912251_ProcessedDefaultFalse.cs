namespace OnLinePrijaveMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProcessedDefaultFalse : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Brokers", "Processed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Distributers", "Processed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.MirovinskiFonds", "Processed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MirovinskiFonds", "Processed", c => c.Boolean());
            AlterColumn("dbo.Distributers", "Processed", c => c.Boolean());
            AlterColumn("dbo.Brokers", "Processed", c => c.Boolean());
        }
    }
}
