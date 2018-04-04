namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventGroupes", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.EventGroupes", "Groupe_Id", "dbo.Groupes");
            DropIndex("dbo.EventGroupes", new[] { "Event_Id" });
            DropIndex("dbo.EventGroupes", new[] { "Groupe_Id" });
            DropTable("dbo.EventGroupes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EventGroupes",
                c => new
                    {
                        Event_Id = c.Int(nullable: false),
                        Groupe_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_Id, t.Groupe_Id });
            
            CreateIndex("dbo.EventGroupes", "Groupe_Id");
            CreateIndex("dbo.EventGroupes", "Event_Id");
            AddForeignKey("dbo.EventGroupes", "Groupe_Id", "dbo.Groupes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventGroupes", "Event_Id", "dbo.Events", "Id", cascadeDelete: true);
        }
    }
}
