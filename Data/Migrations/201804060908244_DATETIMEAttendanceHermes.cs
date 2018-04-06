namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DATETIMEAttendanceHermes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AttendanceHermes", "Arrive", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AttendanceHermes", "Depart", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AttendanceHermes", "Depart", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AttendanceHermes", "Arrive", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
