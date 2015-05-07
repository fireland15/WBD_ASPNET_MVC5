namespace WBD_ASPNET_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userpassoc2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProjectAssociations", "UserId", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProjectAssociations", "UserId", c => c.Int(nullable: false));
        }
    }
}
