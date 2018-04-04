namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groupes", "Groupe_Id", "dbo.Groupes");
            DropForeignKey("dbo.Events", "Event_Id", "dbo.Events");
            DropIndex("dbo.Groupes", new[] { "Groupe_Id" });
            DropIndex("dbo.Events", new[] { "Event_Id" });
            DropColumn("dbo.Groupes", "Groupe_Id");
            DropColumn("dbo.Events", "Event_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Event_Id", c => c.Int());
            AddColumn("dbo.Groupes", "Groupe_Id", c => c.Int());
            CreateIndex("dbo.Events", "Event_Id");
            CreateIndex("dbo.Groupes", "Groupe_Id");
            AddForeignKey("dbo.Events", "Event_Id", "dbo.Events", "Id");
            AddForeignKey("dbo.Groupes", "Groupe_Id", "dbo.Groupes", "Id");
        }
    }
}
