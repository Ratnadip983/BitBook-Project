namespace BitBookProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FriendStatus_Replaced_By_IsAccepted_Column_In_Friend_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "IsAccepted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Friends", "FriendShipStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friends", "FriendShipStatus", c => c.String(nullable: false));
            DropColumn("dbo.Friends", "IsAccepted");
        }
    }
}
