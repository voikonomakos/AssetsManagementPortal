using AssetsManagement.Configuration;
using AssetsManagement.Infrastructure.Repositories;

namespace AssetsManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection("ConnectionStrings"));

            builder.Services.AddSingleton<UsersRepository>();
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSingleton<Db>();

            builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection("ConnectionStrings"));
            // Register DbContext as singleton
            builder.Services.AddSingleton<DbContext>();

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

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
