namespace WBD_ASPNET_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangerdUserInfoLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(maxLength: 10));
            AddColumn("dbo.AspNetUsers", "SignUpDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SignUpDate");
            DropColumn("dbo.AspNetUsers", "PhoneNumber");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
