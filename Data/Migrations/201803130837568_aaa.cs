namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaa : DbMigration
    {
        public override void Up()
        {
         
        }
        
        public override void Down()
        {
            AddColumn("dbo.Indicateurs", "tAbsMensuel", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tAbsHebdo", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tventeMensuel", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tventeHebdo", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsPrievewMensuel", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsPrievewHebdo", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsPausePersoMensuel", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsPausePersoHebdo", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsPauseBriefMensuel", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsPauseBriefHebdo", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsAttmensuel", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsAtthebdo", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsAcwmensuel", c => c.Single(nullable: false));
            AddColumn("dbo.Indicateurs", "tempsAcwHebdo", c => c.Single(nullable: false));
           
        }
    }
}
