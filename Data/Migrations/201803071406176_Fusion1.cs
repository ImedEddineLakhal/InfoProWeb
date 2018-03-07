namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fusion1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groupes", "User_Id", c => c.Int());
            AddColumn("dbo.Users", "nomPrenom", c => c.String());
            AddColumn("dbo.Users", "logEntree", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Users", "logSortie", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Titres", "activite", c => c.String());
            AddColumn("dbo.Titres", "type", c => c.String());
            AddColumn("dbo.Titres", "codeOperation", c => c.Int(nullable: false));
            AddColumn("dbo.Titres", "codeProvRelance", c => c.Int(nullable: false));
            AddColumn("dbo.Titres", "dateInjection", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Titres", "nombreFichesInjectees", c => c.Int(nullable: false));
            CreateIndex("dbo.Groupes", "User_Id");
            AddForeignKey("dbo.Groupes", "User_Id", "dbo.Users", "Id");
            DropColumn("dbo.Users", "password");
            DropColumn("dbo.Users", "nom");
            DropColumn("dbo.Users", "prenom");
            DropColumn("dbo.Users", "responsable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "responsable", c => c.String());
            AddColumn("dbo.Users", "prenom", c => c.String());
            AddColumn("dbo.Users", "nom", c => c.String());
            AddColumn("dbo.Users", "password", c => c.String());
            DropForeignKey("dbo.Groupes", "User_Id", "dbo.Users");
            DropIndex("dbo.Groupes", new[] { "User_Id" });
            DropColumn("dbo.Titres", "nombreFichesInjectees");
            DropColumn("dbo.Titres", "dateInjection");
            DropColumn("dbo.Titres", "codeProvRelance");
            DropColumn("dbo.Titres", "codeOperation");
            DropColumn("dbo.Titres", "type");
            DropColumn("dbo.Titres", "activite");
            DropColumn("dbo.Users", "logSortie");
            DropColumn("dbo.Users", "logEntree");
            DropColumn("dbo.Users", "nomPrenom");
            DropColumn("dbo.Groupes", "User_Id");
        }
    }
}
