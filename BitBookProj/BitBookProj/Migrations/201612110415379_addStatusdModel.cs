namespace BitBookProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStatusdModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Status", "UserId", "dbo.Users");
            DropIndex("dbo.Status", new[] { "UserId" });
            DropTable("dbo.Status");
        }
    }
}
