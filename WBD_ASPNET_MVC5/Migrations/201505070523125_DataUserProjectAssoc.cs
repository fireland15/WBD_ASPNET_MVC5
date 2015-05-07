namespace WBD_ASPNET_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataUserProjectAssoc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataProjectAssocs",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        DataId = c.Int(nullable: false),
                        ProjectId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DataUserAssocs",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        DataId = c.Int(nullable: false),
                        UserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DataUserAssocs");
            DropTable("dbo.DataProjectAssocs");
        }
    }
}
