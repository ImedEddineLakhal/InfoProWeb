namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correspondancePhotoImed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Content", c => c.Binary());
            AddColumn("dbo.Employees", "ContentType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "ContentType");
            DropColumn("dbo.Employees", "Content");
        }
    }
}
