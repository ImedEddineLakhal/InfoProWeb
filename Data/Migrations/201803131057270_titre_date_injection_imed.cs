namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class titre_date_injection_imed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Titres", "codeOperation", c => c.String());
            AlterColumn("dbo.Titres", "codeProvRelance", c => c.String());
            AlterColumn("dbo.Titres", "dateInjection", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Titres", "dateInjection", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Titres", "codeProvRelance", c => c.Int(nullable: false));
            AlterColumn("dbo.Titres", "codeOperation", c => c.Int(nullable: false));
        }
    }
}
