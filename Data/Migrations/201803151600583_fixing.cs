namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixing : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Titres", "dateInjection", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropTable("dbo.Events");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        subject = c.String(),
                        description = c.String(),
                        start = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        end = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        themeColor = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.Titres", "dateInjection", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
