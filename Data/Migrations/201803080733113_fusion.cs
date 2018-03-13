namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fusion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "IdHermes", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "userName", c => c.String());
            AddColumn("dbo.Employees", "pseudoName", c => c.String());
            AddColumn("dbo.Employees", "IdAD", c => c.String());
            AddColumn("dbo.Employees", "Activite", c => c.String());
            AddColumn("dbo.Employees", "userId", c => c.Int());
            AddColumn("dbo.Users", "Employee_Id", c => c.Int());
            CreateIndex("dbo.Employees", "userId");
            CreateIndex("dbo.Users", "Employee_Id");
            AddForeignKey("dbo.Employees", "userId", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "Employee_Id", "dbo.Employees", "Id");
            DropColumn("dbo.Employees", "login");
            DropColumn("dbo.Employees", "password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "password", c => c.String());
            AddColumn("dbo.Employees", "login", c => c.String());
            DropForeignKey("dbo.Users", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "userId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Employee_Id" });
            DropIndex("dbo.Employees", new[] { "userId" });
            DropColumn("dbo.Users", "Employee_Id");
            DropColumn("dbo.Employees", "userId");
            DropColumn("dbo.Employees", "Activite");
            DropColumn("dbo.Employees", "IdAD");
            DropColumn("dbo.Employees", "pseudoName");
            DropColumn("dbo.Employees", "userName");
            DropColumn("dbo.Employees", "IdHermes");
        }
    }
}
