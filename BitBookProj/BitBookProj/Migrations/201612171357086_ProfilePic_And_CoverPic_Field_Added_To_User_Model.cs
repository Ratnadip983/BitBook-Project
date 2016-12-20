namespace BitBookProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfilePic_And_CoverPic_Field_Added_To_User_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ProfilePic", c => c.Binary());
            AddColumn("dbo.Users", "CoverPic", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CoverPic");
            DropColumn("dbo.Users", "ProfilePic");
        }
    }
}
