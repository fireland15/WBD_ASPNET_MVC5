namespace WBD_ASPNET_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedKeyAgain : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DataUserAssocs", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.DataUserAssocs");
            AddPrimaryKey("dbo.DataUserAssocs", new[] { "DataId", "UserId" });
            DropColumn("dbo.DataUserAssocs", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataUserAssocs", "ID", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.DataUserAssocs");
            AddPrimaryKey("dbo.DataUserAssocs", "ID");
            AlterColumn("dbo.DataUserAssocs", "UserId", c => c.String(nullable: false));
        }
    }
}
