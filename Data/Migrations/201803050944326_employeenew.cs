namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeenew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "loginHermes", c => c.String());
            AddColumn("dbo.Employees", "userName", c => c.String());
            AddColumn("dbo.Employees", "loginLdap", c => c.String());
            AddColumn("dbo.Users", "Employee_Id", c => c.Int());
            CreateIndex("dbo.Users", "Employee_Id");
            AddForeignKey("dbo.Users", "Employee_Id", "dbo.Employees", "Id");
            DropColumn("dbo.Employees", "login");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "login", c => c.String());
            DropForeignKey("dbo.Users", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.Users", new[] { "Employee_Id" });
            DropColumn("dbo.Users", "Employee_Id");
            DropColumn("dbo.Employees", "loginLdap");
            DropColumn("dbo.Employees", "userName");
            DropColumn("dbo.Employees", "loginHermes");
        }
    }
}
