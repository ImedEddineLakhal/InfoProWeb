namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventFusion2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        titre = c.String(),
                        description = c.String(),
                        dateDebut = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        dateFin = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        start = c.String(),
                        end = c.String(),
                        themeColor = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Employees", "Event_Id", c => c.Int());
            CreateIndex("dbo.Employees", "Event_Id");
            AddForeignKey("dbo.Employees", "Event_Id", "dbo.Events", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Event_Id", "dbo.Events");
            DropIndex("dbo.Employees", new[] { "Event_Id" });
            DropColumn("dbo.Employees", "Event_Id");
            DropTable("dbo.Events");
        }
    }
}
