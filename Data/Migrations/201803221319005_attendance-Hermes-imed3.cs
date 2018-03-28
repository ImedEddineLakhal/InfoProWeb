namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attendanceHermesimed3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AttendanceHermes", "Id_Hermes", c => c.Int(nullable: false));
            AddColumn("dbo.AttendanceHermes", "Customer_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AttendanceHermes", "Customer_Id");
            DropColumn("dbo.AttendanceHermes", "Id_Hermes");
        }
    }
}
