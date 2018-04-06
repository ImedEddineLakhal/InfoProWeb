namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imedEventaaa : DbMigration
    {
        public override void Up()
        {
            
            AddForeignKey("dbo.Events", "employeeId", "dbo.Employees", "Id");
        }
        
        public override void Down()
        {
        }
    }
}
