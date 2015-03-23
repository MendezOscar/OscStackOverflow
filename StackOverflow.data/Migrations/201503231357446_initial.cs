namespace StackOverflow.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "LastName");
        }
    }
}
