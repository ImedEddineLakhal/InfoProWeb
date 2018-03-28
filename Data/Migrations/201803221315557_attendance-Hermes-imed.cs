namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attendanceHermesimed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttendanceHermes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false, storeType: "date"),
                        Arrive = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Depart = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AttendanceHermes");
        }
    }
}
