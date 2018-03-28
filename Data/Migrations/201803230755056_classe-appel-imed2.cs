namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classeappelimed2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Hermes = c.Int(nullable: false),
                        Customer_Id = c.Int(nullable: false),
                        date = c.DateTime(nullable: false, storeType: "date"),
                        TotalAppelEmis = c.Int(nullable: false),
                        TotalAppelAboutis = c.Int(nullable: false),
                        CA = c.Int(nullable: false),
                        Accords = c.Int(nullable: false),
                        CNA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Appels");
        }
    }
}
