namespace OnLinePrijaveMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdresaPrepiska : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brokers", "UlicaPrepiska", c => c.String(nullable: false));
            AddColumn("dbo.Brokers", "KucniBrojPrepiska", c => c.String(nullable: false));
            AddColumn("dbo.Brokers", "GradPrepiska", c => c.String(nullable: false));
            AddColumn("dbo.MirovinskiFonds", "UlicaPrepiska", c => c.String(nullable: false));
            AddColumn("dbo.MirovinskiFonds", "KucniBrojPrepiska", c => c.String(nullable: false));
            AddColumn("dbo.MirovinskiFonds", "GradPrepiska", c => c.String(nullable: false));
            DropColumn("dbo.Brokers", "AdresaZaPrepisku");
            DropColumn("dbo.MirovinskiFonds", "AdresaZaPrepisku");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MirovinskiFonds", "AdresaZaPrepisku", c => c.String(nullable: false));
            AddColumn("dbo.Brokers", "AdresaZaPrepisku", c => c.String(nullable: false));
            DropColumn("dbo.MirovinskiFonds", "GradPrepiska");
            DropColumn("dbo.MirovinskiFonds", "KucniBrojPrepiska");
            DropColumn("dbo.MirovinskiFonds", "UlicaPrepiska");
            DropColumn("dbo.Brokers", "GradPrepiska");
            DropColumn("dbo.Brokers", "KucniBrojPrepiska");
            DropColumn("dbo.Brokers", "UlicaPrepiska");
        }
    }
}
