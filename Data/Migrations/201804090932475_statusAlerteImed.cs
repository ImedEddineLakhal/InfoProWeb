namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statusAlerteImed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alertes", "status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Alertes", "status");
        }
    }
}
