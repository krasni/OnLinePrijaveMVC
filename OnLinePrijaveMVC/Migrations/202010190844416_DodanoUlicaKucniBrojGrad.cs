namespace OnLinePrijaveMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanoUlicaKucniBrojGrad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brokers", "BusinessEntityId", c => c.Int());
            AddColumn("dbo.Brokers", "Processed", c => c.Boolean());
            AddColumn("dbo.Brokers", "Ulica", c => c.String(nullable: false));
            AddColumn("dbo.Brokers", "KucniBroj", c => c.String(nullable: false));
            AddColumn("dbo.Brokers", "Grad", c => c.String(nullable: false));
            AddColumn("dbo.Distributers", "BusinessEntityId", c => c.Int());
            AddColumn("dbo.Distributers", "Processed", c => c.Boolean());
            AddColumn("dbo.Distributers", "Ulica", c => c.String(nullable: false));
            AddColumn("dbo.Distributers", "KucniBroj", c => c.String(nullable: false));
            AddColumn("dbo.Distributers", "Grad", c => c.String(nullable: false));
            AddColumn("dbo.MirovinskiFonds", "BusinessEntityId", c => c.Int());
            AddColumn("dbo.MirovinskiFonds", "Processed", c => c.Boolean());
            AddColumn("dbo.MirovinskiFonds", "Ulica", c => c.String(nullable: false));
            AddColumn("dbo.MirovinskiFonds", "KucniBroj", c => c.String(nullable: false));
            AddColumn("dbo.MirovinskiFonds", "Grad", c => c.String(nullable: false));
            DropColumn("dbo.Brokers", "AdresaPrebivalista");
            DropColumn("dbo.Distributers", "Adresa");
            DropColumn("dbo.MirovinskiFonds", "AdresaPrebivalista");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MirovinskiFonds", "AdresaPrebivalista", c => c.String(nullable: false));
            AddColumn("dbo.Distributers", "Adresa", c => c.String(nullable: false));
            AddColumn("dbo.Brokers", "AdresaPrebivalista", c => c.String(nullable: false));
            DropColumn("dbo.MirovinskiFonds", "Grad");
            DropColumn("dbo.MirovinskiFonds", "KucniBroj");
            DropColumn("dbo.MirovinskiFonds", "Ulica");
            DropColumn("dbo.MirovinskiFonds", "Processed");
            DropColumn("dbo.MirovinskiFonds", "BusinessEntityId");
            DropColumn("dbo.Distributers", "Grad");
            DropColumn("dbo.Distributers", "KucniBroj");
            DropColumn("dbo.Distributers", "Ulica");
            DropColumn("dbo.Distributers", "Processed");
            DropColumn("dbo.Distributers", "BusinessEntityId");
            DropColumn("dbo.Brokers", "Grad");
            DropColumn("dbo.Brokers", "KucniBroj");
            DropColumn("dbo.Brokers", "Ulica");
            DropColumn("dbo.Brokers", "Processed");
            DropColumn("dbo.Brokers", "BusinessEntityId");
        }
    }
}
