namespace WBD_ASPNET_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataFiles_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataFiles", "ImageReference", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DataFiles", "ImageReference");
        }
    }
}
