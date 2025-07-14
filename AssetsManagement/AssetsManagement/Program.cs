using AssetsManagement.Configuration;
using AssetsManagement.Infrastructure.Data;
using AssetsManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetsManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection("ConnectionStrings"));

            builder.Services.AddScoped<UsersRepository>();
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSingleton<Db>();

            builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection("ConnectionStrings"));

            // Add EF Core with SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register AppDbContext as singleton
            //builder.Services.AddSingleton<AppDbContext>();

            // Add Identity services
            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 5; //change this in production
            })
            .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/"); // Require authentication for all pages by default 
                options.Conventions.AllowAnonymousToPage("/Index"); //Make Home page public
                options.Conventions.AllowAnonymousToPage("/Privacy");
                options.Conventions.AllowAnonymousToFolder("/Public"); //Make all pages in /Public folder accessible without login
                //you can also add the attribute [AllowAnonymous] to specific pages to make them public

            }).AddMvcOptions(options => { });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
