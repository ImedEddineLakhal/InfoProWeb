namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajouttablecalendrier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppointmentDiaries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SomeImportantKey = c.Int(nullable: false),
                        DateTimeScheduled = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        AppointmentLength = c.Int(nullable: false),
                        StatusENUM = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AppointmentDiaries");
        }
    }
}
