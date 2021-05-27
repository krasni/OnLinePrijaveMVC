namespace OnLinePrijaveMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanIspitPolazem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brokers", "IspitPolazem", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Brokers", "IspitPolazem");
        }
    }
}
