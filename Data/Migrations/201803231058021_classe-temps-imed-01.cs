namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classetempsimed01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Temps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Hermes = c.Int(nullable: false),
                        Customer_Id = c.Int(nullable: false),
                        date = c.DateTime(nullable: false, storeType: "date"),
                        tempsLog = c.Int(nullable: false),
                        tempscom = c.Int(nullable: false),
                        tempsAtt = c.Int(nullable: false),
                        tempsACW = c.Int(nullable: false),
                        tempsPausePerso = c.Int(nullable: false),
                        tempsPreview = c.Int(nullable: false),
                        semaine = c.Int(nullable: false),
                        mois = c.String(),
                        Annee = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Temps");
        }
    }
}
