# AssetsManagementPortal
## ASP.NET Core Razor Application

## Initiliaze/set up

 - Update the connection string with your DB information in appsettings.json
 - If you use other DB than SQL server, then update this line in Program.cs `"builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));"`
 - To migrate/create the DB run in a Visual Studio Developer power shell the command: `"dotnet ef database update"`  (if dotnet ef command not found, then run `"dotnet tool install --global dotnet-ef"`

