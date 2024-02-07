using KATCinema.Utils;
using KATCinema.Utils.DBConnection;
using Microsoft.AspNetCore.Identity;
using KATCinema.Models;

namespace KATCinema.Data
{
    public class Seed
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            // Roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin.ToString()))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin.ToString()));
            if (!await roleManager.RoleExistsAsync(UserRoles.Basic.ToString()))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Basic.ToString()));

            // Admin user
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
            string adminUserEmail = "admin@mail.ru";

            var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
            if (adminUser == null)
            {
                var newAdminUser = new User()
                {
                    UserName = "kalman",
                    Email = adminUserEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newAdminUser, "1234");
                await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin.ToString());
            }

            // Basic user
            string UserEmail = "basic-user@mail.ru";

            var User = await userManager.FindByEmailAsync(UserEmail);
            if (User == null)
            {
                var newUser = new User()
                {
                    UserName = "basic-user",
                    Email = UserEmail,
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(newUser, "1234");
                await userManager.AddToRoleAsync(newUser, UserRoles.Basic.ToString());
            }
        }
    }
}