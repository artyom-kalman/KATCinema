using KATCinema.Utils;
using KATCinema.Utils.DBConnection;
using Microsoft.AspNetCore.Identity;
using KATCinema.Models;

namespace KATCinema.Data
{
    public class Seed
    {
        public static async Task SeesSessions(IApplicationBuilder applicationBuilder)
        {
            var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            var rnd = new Random();
            var prices = new decimal[] { 250.00M, 300.00M, 500.00M, 200.00M };

            for (int i = 0; i < 20; i++)
            {
                var startTime = DateTime.Now.Date.AddDays(rnd.Next(8));
                startTime += new TimeSpan(rnd.Next(8, 25), 0, 0);
                Console.WriteLine(startTime);
                var newSession = new Session()
                {
                    HallId = rnd.Next(1, 4),
                    MovieId = rnd.Next(1, 7),
                    StartTime = startTime.ToUniversalTime(),
                    TicketPrice = prices[rnd.Next(4)]
                };
                context.Sessions.Add(newSession);
            }
            context.SaveChanges();
        }

        public static async Task SeedHalls(IApplicationBuilder applicationBuilder)
        {
            var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            
            context.Database.EnsureCreated();

            var rowCount = 0;
            // Hall 1
            for (int i = 1; i <= 7; i++)
            {
                var newRow = new Row()
                {
                    Id = ++rowCount,
                    HallId = 1,
                    RowNumber = i
                };
                context.Rows.Add(newRow);

                var numberOfSeats = 10;
                if (i <= 2)
                {
                    numberOfSeats = 8;
                }
                else if (i >= 5)
                {
                    numberOfSeats = 12;
                }

                for (int j = 1; j <= numberOfSeats; j++)
                {
                    var newSeat = new Seat()
                    {
                        SeatNumber = j,
                        RowId = newRow.Id
                    };
                    context.Seats.Add(newSeat);
                }
            }

            // Hall 2
            for (int i = 1; i <= 12; i++)
            {
                var newRow = new Row()
                {
                    Id = ++rowCount,
                    HallId = 2,
                    RowNumber = i
                };
                context.Rows.Add(newRow);

                var numberOfSeats = 15;
                if (i <= 3)
                {
                    numberOfSeats = 12;
                }
                else if (i >= 11)
                {
                    numberOfSeats = 12;
                }

                for (int j = 1; j <= numberOfSeats; j++)
                {
                    var newSeat = new Seat()
                    {
                        SeatNumber = j,
                        RowId = newRow.Id
                    };
                    context.Seats.Add(newSeat);
                }
            }

            // Hall 3
            for (int i = 1; i <= 20; i++)
            {
                var newRow = new Row()
                {
                    Id = ++rowCount,
                    HallId = 3,
                    RowNumber = i
                };
                context.Rows.Add(newRow);

                var numberOfSeats = 20;
                if (i <= 4)
                {
                    numberOfSeats = 16;
                }
                else if (i >= 17)
                {
                    numberOfSeats = 24;
                }

                for (int j = 1; j <= numberOfSeats; j++)
                {
                    var newSeat = new Seat()
                    {
                        SeatNumber = j,
                        RowId = newRow.Id
                    };
                    context.Seats.Add(newSeat);
                }
            }

            context.SaveChanges();
        }

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