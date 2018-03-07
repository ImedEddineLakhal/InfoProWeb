namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06032018 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "loginHermes", c => c.String());
            AddColumn("dbo.Employees", "userName", c => c.String());
            AddColumn("dbo.Employees", "loginLdap", c => c.String());
            AddColumn("dbo.Employees", "userId", c => c.Int());
            AddColumn("dbo.Users", "Employee_Id", c => c.Int());
            CreateIndex("dbo.Employees", "userId");
            CreateIndex("dbo.Users", "Employee_Id");
            AddForeignKey("dbo.Employees", "userId", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "Employee_Id", "dbo.Employees", "Id");
            DropColumn("dbo.Employees", "login");
            DropColumn("dbo.Titres", "activite");
            DropColumn("dbo.Titres", "type");
            DropColumn("dbo.Titres", "codeOperation");
            DropColumn("dbo.Titres", "codeProvRelance");
            DropColumn("dbo.Titres", "dateInjection");
            DropColumn("dbo.Titres", "nombreFichesInjectees");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Titres", "nombreFichesInjectees", c => c.Int(nullable: false));
            AddColumn("dbo.Titres", "dateInjection", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Titres", "codeProvRelance", c => c.Int(nullable: false));
            AddColumn("dbo.Titres", "codeOperation", c => c.Int(nullable: false));
            AddColumn("dbo.Titres", "type", c => c.String());
            AddColumn("dbo.Titres", "activite", c => c.String());
            AddColumn("dbo.Employees", "login", c => c.String());
            DropForeignKey("dbo.Users", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "userId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Employee_Id" });
            DropIndex("dbo.Employees", new[] { "userId" });
            DropColumn("dbo.Users", "Employee_Id");
            DropColumn("dbo.Employees", "userId");
            DropColumn("dbo.Employees", "loginLdap");
            DropColumn("dbo.Employees", "userName");
            DropColumn("dbo.Employees", "loginHermes");
        }
    }
}
