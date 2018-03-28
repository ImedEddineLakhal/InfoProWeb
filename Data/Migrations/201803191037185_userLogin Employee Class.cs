namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userLoginEmployeeClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "userLogin", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "userLogin");
        }
    }
}
