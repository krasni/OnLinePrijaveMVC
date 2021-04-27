namespace OnLinePrijaveMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BrokerFiles",
                c => new
                    {
                        BrokerFileId = c.Int(nullable: false, identity: true),
                        FilePath = c.String(nullable: false),
                        Broker_BrokerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BrokerFileId)
                .ForeignKey("dbo.Brokers", t => t.Broker_BrokerId, cascadeDelete: true)
                .Index(t => t.Broker_BrokerId);
            
            CreateTable(
                "dbo.Brokers",
                c => new
                    {
                        BrokerId = c.Int(nullable: false, identity: true),
                        VrstaPrijaveBroker = c.Int(nullable: false),
                        Ime = c.String(nullable: false),
                        Prezime = c.String(nullable: false),
                        DatumRodjenja = c.DateTime(nullable: false),
                        MjestoRodjenja = c.String(nullable: false),
                        DrzavaRodjenja = c.String(nullable: false),
                        StupanjObrazovanja = c.String(nullable: false),
                        SteceniNaziv = c.String(nullable: false),
                        Zanimanje = c.String(nullable: false),
                        AdresaPrebivalista = c.String(nullable: false),
                        AdresaZaPrepisku = c.String(nullable: false),
                        Telefon = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        OIB = c.String(nullable: false),
                        SifraKandidata = c.String(nullable: false),
                        IspitiPolozeniUHanfi = c.String(maxLength: 250),
                        IspitiPolozeniUOrganizacijiCFA = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BrokerId);
            
            CreateTable(
                "dbo.Distributers",
                c => new
                    {
                        DistributerId = c.Int(nullable: false, identity: true),
                        Ime = c.String(nullable: false),
                        Prezime = c.String(nullable: false),
                        OIB = c.String(nullable: false),
                        DatumRodjenja = c.DateTime(nullable: false),
                        MjestoRodjenja = c.String(nullable: false),
                        DrzavaRodjenja = c.String(nullable: false),
                        Adresa = c.String(nullable: false),
                        BrojTelefona = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        StrucnaSprema = c.String(nullable: false),
                        Zanimanje = c.String(),
                        ZaposlenKod = c.String(),
                        IspitPolazem = c.Int(nullable: false),
                        SifraKandidata = c.String(nullable: false),
                        ZivotnoOsiguranje = c.Boolean(nullable: false),
                        NezivotnoOsiguranje = c.Boolean(nullable: false),
                        DistribucijaOsiguranja = c.Boolean(nullable: false),
                        DistribucijaReosiguranja = c.Boolean(nullable: false),
                        ZastupnikUOsiguranju = c.Boolean(nullable: false),
                        BrokerUOsiguranju = c.Boolean(nullable: false),
                        BrokerUReosiguranju = c.Boolean(nullable: false),
                        PosrednikUOsiguranju = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DistributerId);
            
            CreateTable(
                "dbo.DistributerFiles",
                c => new
                    {
                        DistributerFileId = c.Int(nullable: false, identity: true),
                        FilePath = c.String(nullable: false),
                        Distributer_DistributerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DistributerFileId)
                .ForeignKey("dbo.Distributers", t => t.Distributer_DistributerId, cascadeDelete: true)
                .Index(t => t.Distributer_DistributerId);
            
            CreateTable(
                "dbo.MirovinskiFonds",
                c => new
                    {
                        MirovinskiFondId = c.Int(nullable: false, identity: true),
                        VrstaPrijaveMirovinskiFond = c.Int(nullable: false),
                        Ime = c.String(nullable: false),
                        Prezime = c.String(nullable: false),
                        DatumRodjenja = c.DateTime(nullable: false),
                        MjestoRodjenja = c.String(nullable: false),
                        DrzavaRodjenja = c.String(nullable: false),
                        StupanjObrazovanja = c.String(nullable: false),
                        SteceniNaziv = c.String(nullable: false),
                        Zanimanje = c.String(nullable: false),
                        AdresaPrebivalista = c.String(nullable: false),
                        AdresaZaPrepisku = c.String(nullable: false),
                        Telefon = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        OIB = c.String(nullable: false),
                        SifraKandidata = c.String(nullable: false),
                        IspitiPolozeniUHanfi = c.String(maxLength: 250),
                        IspitiPolozeniUOrganizacijiCFA = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MirovinskiFondId);
            
            CreateTable(
                "dbo.MirovinskiFondFiles",
                c => new
                    {
                        MirovinskiFondFileId = c.Int(nullable: false, identity: true),
                        FilePath = c.String(nullable: false),
                        MirovinskiFond_MirovinskiFondId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MirovinskiFondFileId)
                .ForeignKey("dbo.MirovinskiFonds", t => t.MirovinskiFond_MirovinskiFondId, cascadeDelete: true)
                .Index(t => t.MirovinskiFond_MirovinskiFondId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MirovinskiFondFiles", "MirovinskiFond_MirovinskiFondId", "dbo.MirovinskiFonds");
            DropForeignKey("dbo.DistributerFiles", "Distributer_DistributerId", "dbo.Distributers");
            DropForeignKey("dbo.BrokerFiles", "Broker_BrokerId", "dbo.Brokers");
            DropIndex("dbo.MirovinskiFondFiles", new[] { "MirovinskiFond_MirovinskiFondId" });
            DropIndex("dbo.DistributerFiles", new[] { "Distributer_DistributerId" });
            DropIndex("dbo.BrokerFiles", new[] { "Broker_BrokerId" });
            DropTable("dbo.MirovinskiFondFiles");
            DropTable("dbo.MirovinskiFonds");
            DropTable("dbo.DistributerFiles");
            DropTable("dbo.Distributers");
            DropTable("dbo.Brokers");
            DropTable("dbo.BrokerFiles");
        }
    }
}
