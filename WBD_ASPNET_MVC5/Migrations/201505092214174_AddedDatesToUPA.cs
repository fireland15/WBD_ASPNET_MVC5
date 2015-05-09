namespace WBD_ASPNET_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDatesToUPA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProjectAssociations", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProjectAssociations", "DateAdded");
        }
    }
}
