namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlertesImed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alertes", "senderId", c => c.Int());
            AddColumn("dbo.Alertes", "reciverId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Alertes", "reciverId");
            DropColumn("dbo.Alertes", "senderId");
        }
    }
}
