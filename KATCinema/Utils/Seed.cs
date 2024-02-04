//using KATCinema.Utils;
//using KATCinema.Utils.DBConnection;
//using Microsoft.AspNetCore.Identity;
//using KATCinema.Models;

//namespace RunGroopWebApp.Data
//{
//    public class Seed
//    {
//        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
//        {
//            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
//            {
//                //Roles
//                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
//                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
//                if (!await roleManager.RoleExistsAsync(UserRoles.User))
//                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

//                //Users
//                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
//                string adminUserEmail = "teddysmithdeveloper@gmail.com";

//                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
//                if (adminUser == null)
//                {
//                    var newAdminUser = new User()
//                    {
//                        UserName = "teddysmithdev",
//                        Email = adminUserEmail,
//                        EmailConfirmed = true,
//                        Address = new Address()
//                        {
//                            Street = "123 Main St",
//                            City = "Charlotte",
//                            State = "NC"
//                        }
//                    };
//                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
//                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
//                }

//                string UserEmail = "user@etickets.com";

//                var User = await userManager.FindByEmailAsync(UserEmail);
//                if (User == null)
//                {
//                    var newUser = new User()
//                    {
//                        UserName = "app-user",
//                        Email = UserEmail,
//                        EmailConfirmed = true,
//                        Address = new Address()
//                        {
//                            Street = "123 Main St",
//                            City = "Charlotte",
//                            State = "NC"
//                        }
//                    };
//                    await userManager.CreateAsync(newUser, "Coding@1234?");
//                    await userManager.AddToRoleAsync(newUser, UserRoles.User);
//                }
//            }
//        }
//    }
//}