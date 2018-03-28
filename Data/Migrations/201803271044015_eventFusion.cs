namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventFusion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlaningTitres", "Planing_Id", "dbo.Planings");
            DropForeignKey("dbo.PlaningTitres", "Titre_Id", "dbo.Titres");
            DropIndex("dbo.PlaningTitres", new[] { "Planing_Id" });
            DropIndex("dbo.PlaningTitres", new[] { "Titre_Id" });
            AddColumn("dbo.Groupes", "Planing_Id", c => c.Int());
            AddColumn("dbo.Planings", "dateDebut", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Planings", "dateFin", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Planings", "heureDebut", c => c.String());
            AddColumn("dbo.Planings", "heureFin", c => c.String());
            AddColumn("dbo.Planings", "Titre_Id", c => c.Int());
            CreateIndex("dbo.Groupes", "Planing_Id");
            CreateIndex("dbo.Planings", "Titre_Id");
            AddForeignKey("dbo.Groupes", "Planing_Id", "dbo.Planings", "Id");
            AddForeignKey("dbo.Planings", "Titre_Id", "dbo.Titres", "Id");
            DropColumn("dbo.Planings", "date");
            DropColumn("dbo.Planings", "heureArrivee");
            DropColumn("dbo.Planings", "dateDepart");
            DropColumn("dbo.Planings", "duree");
            DropTable("dbo.PlaningTitres");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PlaningTitres",
                c => new
                    {
                        Planing_Id = c.Int(nullable: false),
                        Titre_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Planing_Id, t.Titre_Id });
            
            AddColumn("dbo.Planings", "duree", c => c.Int(nullable: false));
            AddColumn("dbo.Planings", "dateDepart", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Planings", "heureArrivee", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Planings", "date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropForeignKey("dbo.Planings", "Titre_Id", "dbo.Titres");
            DropForeignKey("dbo.Groupes", "Planing_Id", "dbo.Planings");
            DropIndex("dbo.Planings", new[] { "Titre_Id" });
            DropIndex("dbo.Groupes", new[] { "Planing_Id" });
            DropColumn("dbo.Planings", "Titre_Id");
            DropColumn("dbo.Planings", "heureFin");
            DropColumn("dbo.Planings", "heureDebut");
            DropColumn("dbo.Planings", "dateFin");
            DropColumn("dbo.Planings", "dateDebut");
            DropColumn("dbo.Groupes", "Planing_Id");
            CreateIndex("dbo.PlaningTitres", "Titre_Id");
            CreateIndex("dbo.PlaningTitres", "Planing_Id");
            AddForeignKey("dbo.PlaningTitres", "Titre_Id", "dbo.Titres", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlaningTitres", "Planing_Id", "dbo.Planings", "Id", cascadeDelete: true);
        }
    }
}
