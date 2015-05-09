namespace WBD_ASPNET_MVC5.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WBD_ASPNET_MVC5.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WBD_ASPNET_MVC5.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WBD_ASPNET_MVC5.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Administrator"))
            {
                roleManager.Create(new IdentityRole("Administrator"));
            }

            if (!roleManager.RoleExists("AppUser"))
            {
                roleManager.Create(new IdentityRole("AppUser"));
            }

            var user = new ApplicationUser
            {
                UserName = "Admin",
                FirstName = "Admin",
                LastName = "Da Administrator",
                PhoneNumber = "1231234123",
                SignUpDate = DateTime.Today
            };

            if (userManager.FindByName("Admin") == null)
            {
                var result = userManager.Create(user, "password");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrator");
                }
            }

            user = new ApplicationUser
            {
                UserName = "forrest",
                FirstName = "Forrest",
                LastName = "Ireland",
                PhoneNumber = "5094672438",
                SignUpDate = DateTime.Today
            };

            if (userManager.FindByName("forrest") == null)
            {
                var result = userManager.Create(user, "password");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "AppUser");
                }
            }

            user = new ApplicationUser
            {
                UserName = "mark",
                FirstName = "Mark",
                LastName = "Klick",
                PhoneNumber = "3062314352",
                SignUpDate = DateTime.Today
            };

            if (userManager.FindByName("mark") == null)
            {
                var result = userManager.Create(user, "password");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "AppUser");
                }
            }

            user = new ApplicationUser
            {
                UserName = "jared",
                FirstName = "Jared",
                LastName = "Miles",
                PhoneNumber = "1234526449",
                SignUpDate = DateTime.Today
            };

            if (userManager.FindByName("jared") == null)
            {
                var result = userManager.Create(user, "password");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "AppUser");
                }
            }

            user = new ApplicationUser
            {
                UserName = "garrett",
                FirstName = "Garrett",
                LastName = "Capachelli",
                PhoneNumber = "9865854561",
                SignUpDate = DateTime.Today
            };

            if (userManager.FindByName("garrett") == null)
            {
                var result = userManager.Create(user, "password");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "AppUser");
                }
            }
        }
    }
}
