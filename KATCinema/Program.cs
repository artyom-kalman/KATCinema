using KATCinema.Models;
using KATCinema.Utils.DBConnection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.Internal;
using KATCinema.Data;

var builder = WebApplication.CreateBuilder(args);

var dbConnection = new DBConnection();

// Connect to DB.
var conn = dbConnection.Connect(builder.Configuration.GetConnectionString("DefaultConnection"));

// Add services to the container.
builder.Services.AddControllersWithViews();
// Configure DbContext.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(conn);
});
// Configure Identity Servieces
builder.Services.AddIdentity<User, IdentityRole>(
    options =>
    {
        // Configure password policy.
        options.Password.RequiredLength = 4;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Seed
//await Seed.SeedUsersAndRolesAsync(app);
//await Seed.SeedHalls(app);



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
