namespace OnLinePrijaveMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanIspitPolazemMirovinskiFond : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MirovinskiFonds", "IspitPolazem", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MirovinskiFonds", "IspitPolazem");
        }
    }
}
