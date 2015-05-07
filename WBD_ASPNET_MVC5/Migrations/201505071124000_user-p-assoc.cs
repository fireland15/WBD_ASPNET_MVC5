namespace WBD_ASPNET_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userpassoc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProjectAssociations",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ProjectId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProjectId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProjectAssociations");
        }
    }
}
