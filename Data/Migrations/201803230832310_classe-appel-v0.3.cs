namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classeappelv03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appels", "nomCompagne", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appels", "nomCompagne");
        }
    }
}
