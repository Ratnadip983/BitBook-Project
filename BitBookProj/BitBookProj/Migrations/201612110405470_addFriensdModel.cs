namespace BitBookProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFriensdModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friend",
                c => new
                    {
                        User1Id = c.Int(nullable: false),
                        User2Id = c.Int(nullable: false),
                        FriendShipStatus = c.String(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.User1Id, t.User2Id })
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User1Id)
                .ForeignKey("dbo.Users", t => t.User2Id)
                .Index(t => t.User1Id)
                .Index(t => t.User2Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friend", "User2Id", "dbo.Users");
            DropForeignKey("dbo.Friend", "User1Id", "dbo.Users");
            DropForeignKey("dbo.Friend", "User_Id", "dbo.Users");
            DropIndex("dbo.Friend", new[] { "User_Id" });
            DropIndex("dbo.Friend", new[] { "User2Id" });
            DropIndex("dbo.Friend", new[] { "User1Id" });
            DropTable("dbo.Friend");
        }
    }
}
