namespace WBD_ASPNET_MVC5.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
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
            
            // Creates users
            SeedUsers(context);

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Generate a list of unique ProjectIds here, need as many or more than the number of Projects you define
            List<string> ProjectIds = new List<string>();
            ProjectIds.Add(Guid.NewGuid().ToString());

            //add projects here
            context.Projects.AddOrUpdate(
                new Project
                {
                    // Use this to generate unique id's 
                    Id = ProjectIds[0],
                    ProjectName = "Mammoth DNA Recovery",
                    BriefDescription = "A study interested in the recovery of mammoth DNA.",
                    LongDescription = "This study aims to find novel methods for the recovery of ancient mammoth DNA from fossilized or nearly fossilized samples. Our samples are several thousands of years old and are from many different regions of North America and Siberia. To date three novel methods have been found for harvesting DNA from our frozen samples.",

                    //                format is yyyy-mm-ddThh-mm-ss
                    CreatedOn = DateTime.Parse("2008-09-15T09:30:41"),
                    // Need to use this method to make sure all foreign keys are properly referenced
                    OwnerID = userManager.FindByName("forrest").Id
                }
                // Just need more projects
            );

            // Generate a list of unique DataIds here, need as many or more than the number of Datafiles you define
            List<string> DataIds = new List<string>();
            DataIds.Add(Guid.NewGuid().ToString());

            //add Datafiles here
            context.DataFiles.AddOrUpdate(
                new DataFile
                {
                    Id = DataIds[0],
                    DataName = "Columbia M23 16s",
                    DataCategory = "DNA sequence",
                    Description = "16s DNA sequence of sample Columbia M23",
                    // Just make up names for the files, and create an actual copy of the file, we need them on the server
                    FileReference = "C:/Users/Columbia_M23_16s.fasta",
                    ImageReference = "/FILES/image.jpg",
                    UploadDate = DateTime.Parse("2009-05-20T09:30:41"),
                    UploaderID = userManager.FindByName("mark").Id
                }
            );

            // Create connection between projects and datafiles
            context.DataProjectAssociations.AddOrUpdate(
                // Just assign pairs of data ids and project ids here
                new DataProjectAssoc
                {
                    DataId = DataIds[0],
                    ProjectId = ProjectIds[0]
                }
            );

            context.UserProjectAssoc.AddOrUpdate(
                // Each one of these you define links a user and a project together, make sure to do each pair no more than one time, otherwise errors will happen
                // Also, when you create a project, you need to make one of these to link the owner of that project to the project
                new UserProjectAssociation
                {
                    // Need to use this to get the user id, just change the string to other defined user names
                    // Example: forrest owns project[0] and needs to be linked to it
                    UserId = userManager.FindByName("forrest").Id,
                    ProjectId = ProjectIds[0],
                    DateAdded = DateTime.Parse("2008-09-15T09:30:41")
                },
                new UserProjectAssociation
                {
                    // Mark doesnt own this project but he is a part of it
                    UserId = userManager.FindByName("mark").Id,
                    ProjectId = ProjectIds[0],
                    DateAdded = DateTime.Parse("2010-01-26T09:30:41")
                }
            );
        }

        // This method seed the users of the project
        private void SeedUsers(WBD_ASPNET_MVC5.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            /*****************************************
             * Add new Roles to the application here
             * DONE
             * *****************************************/

            if (!roleManager.RoleExists("Administrator"))
            {
                roleManager.Create(new IdentityRole("Administrator"));
            }

            if (!roleManager.RoleExists("AppUser"))
            {
                roleManager.Create(new IdentityRole("AppUser"));
            }

            /*****************************************
             * Add new Users to the application here
             * DONE
             * *****************************************/

            //  Creates a new application user. Needs all these variables to be set in this manner
            var user = new ApplicationUser
            {
                UserName = "Admin",
                FirstName = "Admin",
                LastName = "Da Administrator",
                PhoneNumber = "1231234123",
                SignUpDate = DateTime.Today
            };

            // This adds the new user to the database and gives them a role in the application
            // Any new users should be assigned to the AppUser role
            if (userManager.FindByName("Admin") == null)
            {
                var result = userManager.Create(user, "password");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrator");
                }
            }

            // Repeat everything for each new user
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
