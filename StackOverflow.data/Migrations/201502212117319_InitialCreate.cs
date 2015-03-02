namespace StackOverflow.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Description = c.String(),
                        Votes = c.Int(nullable: false),
                        Tittle = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Owner_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.Owner_ID)
                .Index(t => t.Owner_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Owner_ID", "dbo.Accounts");
            DropIndex("dbo.Questions", new[] { "Owner_ID" });
            DropTable("dbo.Questions");
            DropTable("dbo.Accounts");
        }
    }
}
