namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstManyToManyImed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupeEmployees", "Groupe_Id", "dbo.Groupes");
            DropForeignKey("dbo.GroupeEmployees", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.GroupeEmployees", new[] { "Groupe_Id" });
            DropIndex("dbo.GroupeEmployees", new[] { "Employee_Id" });
            CreateTable(
                "dbo.GroupesEmployees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        employeeId = c.Int(),
                        groupeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.employeeId)
                .ForeignKey("dbo.Groupes", t => t.groupeId)
                .Index(t => t.employeeId)
                .Index(t => t.groupeId);
            
            DropTable("dbo.GroupeEmployees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GroupeEmployees",
                c => new
                    {
                        Groupe_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Groupe_Id, t.Employee_Id });
            
            DropForeignKey("dbo.GroupesEmployees", "groupeId", "dbo.Groupes");
            DropForeignKey("dbo.GroupesEmployees", "employeeId", "dbo.Employees");
            DropIndex("dbo.GroupesEmployees", new[] { "groupeId" });
            DropIndex("dbo.GroupesEmployees", new[] { "employeeId" });
            DropTable("dbo.GroupesEmployees");
            CreateIndex("dbo.GroupeEmployees", "Employee_Id");
            CreateIndex("dbo.GroupeEmployees", "Groupe_Id");
            AddForeignKey("dbo.GroupeEmployees", "Employee_Id", "dbo.Employees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GroupeEmployees", "Groupe_Id", "dbo.Groupes", "Id", cascadeDelete: true);
        }
    }
}
