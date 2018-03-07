namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newCorrespondanceImed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "IdHermes", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "pseudoName", c => c.String());
            AddColumn("dbo.Employees", "IdAD", c => c.String());
            AddColumn("dbo.Employees", "Activite", c => c.String());
            DropColumn("dbo.Employees", "loginHermes");
            DropColumn("dbo.Employees", "password");
            DropColumn("dbo.Employees", "loginLdap");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "loginLdap", c => c.String());
            AddColumn("dbo.Employees", "password", c => c.String());
            AddColumn("dbo.Employees", "loginHermes", c => c.String());
            DropColumn("dbo.Employees", "Activite");
            DropColumn("dbo.Employees", "IdAD");
            DropColumn("dbo.Employees", "pseudoName");
            DropColumn("dbo.Employees", "IdHermes");
        }
    }
}
