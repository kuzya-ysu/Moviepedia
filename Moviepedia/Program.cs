using Moviepedia.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MovieContext>(options=>options.UseSqlServer(connection));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 6;
    opts.Password.RequireDigit = true;
    opts.Password.RequireLowercase = true;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireUppercase = true;
}
).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

app.Run();

