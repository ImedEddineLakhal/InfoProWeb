namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alertes", "titreAlerte", c => c.String());
            AddColumn("dbo.Alertes", "description", c => c.String());
            AddColumn("dbo.Alertes", "dateCreation", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Alertes", "etatAlerte", c => c.String());
            AddColumn("dbo.Alertes", "reponseAlerte", c => c.String());
            AddColumn("dbo.Alertes", "dateReponse", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Indicateurs", "agent", c => c.Int(nullable: false));
            AddColumn("dbo.Indicateurs", "CA", c => c.Int(nullable: false));
            AddColumn("dbo.Indicateurs", "accord", c => c.Int(nullable: false));
            AddColumn("dbo.Indicateurs", "CNA", c => c.Int(nullable: false));
            AddColumn("dbo.Indicateurs", "totAboutis", c => c.Int(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsLog", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsComm", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsAtt", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsACW", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsPausePerso", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsPreview", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsPauseBrief", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "responsable", c => c.Int(nullable: false));
            AddColumn("dbo.Indicateurs", "date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Indicateurs", "semaine", c => c.Int(nullable: false));
            AddColumn("dbo.Indicateurs", "mois", c => c.String());
            AddColumn("dbo.Indicateurs", "tempsAcwHebdo", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsAcwmensuel", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsAtthebdo", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsAttmensuel", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsPauseBriefHebdo", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsPauseBriefMensuel", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsPausePersoHebdo", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsPausePersoMensuel", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsPrievewHebdo", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsPrievewMensuel", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "appelEmis", c => c.Int(nullable: false));
            AddColumn("dbo.Indicateurs", "tvente", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tventeHebdo", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tventeMensuel", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tAbs", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tAbsHebdo", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tAbsMensuel", c => c.Single(nullable: false));
            AddColumn("dbo.Groupes", "nom", c => c.String());
            AddColumn("dbo.Groupes", "responsable", c => c.String());
            AddColumn("dbo.Planings", "date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Planings", "heureArrivee", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Planings", "dateDepart", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Planings", "duree", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "login", c => c.String());
            AddColumn("dbo.Users", "password", c => c.String());
            AddColumn("dbo.Users", "nom", c => c.String());
            AddColumn("dbo.Users", "prenom", c => c.String());
            AddColumn("dbo.Users", "role", c => c.String());
            AddColumn("dbo.Users", "responsable", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "responsable");
            DropColumn("dbo.Users", "role");
            DropColumn("dbo.Users", "prenom");
            DropColumn("dbo.Users", "nom");
            DropColumn("dbo.Users", "password");
            DropColumn("dbo.Users", "login");
            DropColumn("dbo.Planings", "duree");
            DropColumn("dbo.Planings", "dateDepart");
            DropColumn("dbo.Planings", "heureArrivee");
            DropColumn("dbo.Planings", "date");
            DropColumn("dbo.Groupes", "responsable");
            DropColumn("dbo.Groupes", "nom");
            DropColumn("dbo.Indicateurs", "tAbsMensuel");
            DropColumn("dbo.Indicateurs", "tAbsHebdo");
            DropColumn("dbo.Indicateurs", "tAbs");
            DropColumn("dbo.Indicateurs", "tventeMensuel");
            DropColumn("dbo.Indicateurs", "tventeHebdo");
            DropColumn("dbo.Indicateurs", "tvente");
            DropColumn("dbo.Indicateurs", "appelEmis");
            DropColumn("dbo.Indicateurs", "tempsPrievewMensuel");
            DropColumn("dbo.Indicateurs", "tempsPrievewHebdo");
            DropColumn("dbo.Indicateurs", "tempsPausePersoMensuel");
            DropColumn("dbo.Indicateurs", "tempsPausePersoHebdo");
            DropColumn("dbo.Indicateurs", "tempsPauseBriefMensuel");
            DropColumn("dbo.Indicateurs", "tempsPauseBriefHebdo");
            DropColumn("dbo.Indicateurs", "tempsAttmensuel");
            DropColumn("dbo.Indicateurs", "tempsAtthebdo");
            DropColumn("dbo.Indicateurs", "tempsAcwmensuel");
            DropColumn("dbo.Indicateurs", "tempsAcwHebdo");
            DropColumn("dbo.Indicateurs", "mois");
            DropColumn("dbo.Indicateurs", "semaine");
            DropColumn("dbo.Indicateurs", "date");
            DropColumn("dbo.Indicateurs", "responsable");
            DropColumn("dbo.Indicateurs", "tempsPauseBrief");
            DropColumn("dbo.Indicateurs", "tempsPreview");
            DropColumn("dbo.Indicateurs", "tempsPausePerso");
            DropColumn("dbo.Indicateurs", "tempsACW");
            DropColumn("dbo.Indicateurs", "tempsAtt");
            DropColumn("dbo.Indicateurs", "tempsComm");
            DropColumn("dbo.Indicateurs", "tempsLog");
            DropColumn("dbo.Indicateurs", "totAboutis");
            DropColumn("dbo.Indicateurs", "CNA");
            DropColumn("dbo.Indicateurs", "accord");
            DropColumn("dbo.Indicateurs", "CA");
            DropColumn("dbo.Indicateurs", "agent");
            DropColumn("dbo.Alertes", "dateReponse");
            DropColumn("dbo.Alertes", "reponseAlerte");
            DropColumn("dbo.Alertes", "etatAlerte");
            DropColumn("dbo.Alertes", "dateCreation");
            DropColumn("dbo.Alertes", "description");
            DropColumn("dbo.Alertes", "titreAlerte");
        }
    }
}
