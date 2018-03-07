namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useremp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "userId", c => c.Int());
            CreateIndex("dbo.Employees", "userId");
            AddForeignKey("dbo.Employees", "userId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "userId", "dbo.Users");
            DropIndex("dbo.Employees", new[] { "userId" });
            DropColumn("dbo.Employees", "userId");
        }
    }
}
