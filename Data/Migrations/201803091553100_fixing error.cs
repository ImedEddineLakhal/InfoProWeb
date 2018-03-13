namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingerror : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "logEntree", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "logSortie", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "logSortie", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "logEntree", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
