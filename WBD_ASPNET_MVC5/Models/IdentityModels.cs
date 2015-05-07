using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace WBD_ASPNET_MVC5.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<WBD_ASPNET_MVC5.Models.Project> Projects { get; set; }
        public System.Data.Entity.DbSet<WBD_ASPNET_MVC5.Models.UserInfo> UserInfos { get; set; }
        public System.Data.Entity.DbSet<WBD_ASPNET_MVC5.Models.DataFile> DataFiles { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    public System.Data.Entity.DbSet<WBD_ASPNET_MVC5.Models.Project> Projects { get; set;}
        //}
    }

    public class MyDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string name = "Admin";
            string password = "password";

            // Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(name))
            {
                var roleResult = RoleManager.Create(new IdentityRole(name));
            }

            // Create User = Admin with password
            var user = new ApplicationUser();
            user.UserName = name;
            var adminresult = UserManager.Create(user, password);

            // Add user admin to role admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, name);
            }

            base.Seed(context);
        }
    }
}