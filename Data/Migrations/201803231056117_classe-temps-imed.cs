namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classetempsimed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appels", "semaine", c => c.Int(nullable: false));
            AddColumn("dbo.Appels", "mois", c => c.String());
            AddColumn("dbo.Appels", "Annee", c => c.Int(nullable: false));
            AddColumn("dbo.AttendanceHermes", "semaine", c => c.Int(nullable: false));
            AddColumn("dbo.AttendanceHermes", "mois", c => c.String());
            AddColumn("dbo.AttendanceHermes", "Annee", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AttendanceHermes", "Annee");
            DropColumn("dbo.AttendanceHermes", "mois");
            DropColumn("dbo.AttendanceHermes", "semaine");
            DropColumn("dbo.Appels", "Annee");
            DropColumn("dbo.Appels", "mois");
            DropColumn("dbo.Appels", "semaine");
        }
    }
}
