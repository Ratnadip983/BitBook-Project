namespace BitBookProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 20, unicode: false),
                        Email = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        DateOfBirth = c.String(),
                        Password = c.String(nullable: false),
                        City = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
