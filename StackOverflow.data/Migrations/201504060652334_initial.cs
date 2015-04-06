namespace StackOverflow.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Account_Id = c.Guid(),
                        Answer_Id = c.Guid(),
                        Question_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Answers", t => t.Answer_Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.Answer_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Answer_Id = c.Guid(),
                        Question_Id = c.Guid(),
                        Voter_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.Answer_Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .ForeignKey("dbo.Accounts", t => t.Voter_Id)
                .Index(t => t.Answer_Id)
                .Index(t => t.Question_Id)
                .Index(t => t.Voter_Id);
            
            AddColumn("dbo.Answers", "CreationDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Answers", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "Title", c => c.String());
            DropForeignKey("dbo.Votes", "Voter_Id", "dbo.Accounts");
            DropForeignKey("dbo.Votes", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Votes", "Answer_Id", "dbo.Answers");
            DropForeignKey("dbo.Comments", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Comments", "Answer_Id", "dbo.Answers");
            DropForeignKey("dbo.Comments", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Votes", new[] { "Voter_Id" });
            DropIndex("dbo.Votes", new[] { "Question_Id" });
            DropIndex("dbo.Votes", new[] { "Answer_Id" });
            DropIndex("dbo.Comments", new[] { "Question_Id" });
            DropIndex("dbo.Comments", new[] { "Answer_Id" });
            DropIndex("dbo.Comments", new[] { "Account_Id" });
            DropColumn("dbo.Answers", "CreationDate");
            DropTable("dbo.Votes");
            DropTable("dbo.Comments");
        }
    }
}
