namespace WBD_ASPNET_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDatFiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataFiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DataName = c.String(nullable: false, maxLength: 100),
                        FileReference = c.String(nullable: false, maxLength: 100),
                        DataCategory = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 140),
                        UploadDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DataFiles");
        }
    }
}
