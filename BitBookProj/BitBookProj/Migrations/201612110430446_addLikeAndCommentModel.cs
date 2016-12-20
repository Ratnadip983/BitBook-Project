namespace BitBookProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLikeAndCommentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.UserId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Likes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Likes", "StatusId", "dbo.Status");
            DropIndex("dbo.Likes", new[] { "StatusId" });
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "StatusId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropTable("dbo.Likes");
            DropTable("dbo.Comments");
        }
    }
}
