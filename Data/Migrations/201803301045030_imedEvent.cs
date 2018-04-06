namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imedEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "employeeId", c => c.Int());
            CreateIndex("dbo.Events", "employeeId");
            AddForeignKey("dbo.Events", "employeeId", "dbo.Employees", "Id");
        }

        public override void Down()
        {
           

        }
    }
}
