namespace WBD_ASPNET_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedKey : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DataProjectAssocs", "ProjectId", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.DataProjectAssocs");
            AddPrimaryKey("dbo.DataProjectAssocs", new[] { "DataId", "ProjectId" });
            DropColumn("dbo.DataProjectAssocs", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataProjectAssocs", "ID", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.DataProjectAssocs");
            AddPrimaryKey("dbo.DataProjectAssocs", "ID");
            AlterColumn("dbo.DataProjectAssocs", "ProjectId", c => c.String(nullable: false));
        }
    }
}
