namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alertes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        indicateurId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Indicateurs", t => t.indicateurId)
                .Index(t => t.indicateurId);
            
            CreateTable(
                "dbo.Indicateurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        employeeId = c.Int(),
                        titreId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.employeeId)
                .ForeignKey("dbo.Titres", t => t.titreId)
                .Index(t => t.employeeId)
                .Index(t => t.titreId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        login = c.String(),
                        password = c.String(),
                        role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groupes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Titres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        libelle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Planings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupeEmployees",
                c => new
                    {
                        Groupe_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Groupe_Id, t.Employee_Id })
                .ForeignKey("dbo.Groupes", t => t.Groupe_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.Groupe_Id)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.PlaningTitres",
                c => new
                    {
                        Planing_Id = c.Int(nullable: false),
                        Titre_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Planing_Id, t.Titre_Id })
                .ForeignKey("dbo.Planings", t => t.Planing_Id, cascadeDelete: true)
                .ForeignKey("dbo.Titres", t => t.Titre_Id, cascadeDelete: true)
                .Index(t => t.Planing_Id)
                .Index(t => t.Titre_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Alertes", "indicateurId", "dbo.Indicateurs");
            DropForeignKey("dbo.PlaningTitres", "Titre_Id", "dbo.Titres");
            DropForeignKey("dbo.PlaningTitres", "Planing_Id", "dbo.Planings");
            DropForeignKey("dbo.Indicateurs", "titreId", "dbo.Titres");
            DropForeignKey("dbo.Indicateurs", "employeeId", "dbo.Employees");
            DropForeignKey("dbo.GroupeEmployees", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.GroupeEmployees", "Groupe_Id", "dbo.Groupes");
            DropIndex("dbo.PlaningTitres", new[] { "Titre_Id" });
            DropIndex("dbo.PlaningTitres", new[] { "Planing_Id" });
            DropIndex("dbo.GroupeEmployees", new[] { "Employee_Id" });
            DropIndex("dbo.GroupeEmployees", new[] { "Groupe_Id" });
            DropIndex("dbo.Indicateurs", new[] { "titreId" });
            DropIndex("dbo.Indicateurs", new[] { "employeeId" });
            DropIndex("dbo.Alertes", new[] { "indicateurId" });
            DropTable("dbo.PlaningTitres");
            DropTable("dbo.GroupeEmployees");
            DropTable("dbo.Users");
            DropTable("dbo.Planings");
            DropTable("dbo.Titres");
            DropTable("dbo.Groupes");
            DropTable("dbo.Employees");
            DropTable("dbo.Indicateurs");
            DropTable("dbo.Alertes");
        }
    }
}
