# AssetsManagementPortal
ASP.NET Core Razor Application

##Initiliaze/set up

1. Update the connection string with your DB information in appsettings.json
2. If you use other DB than SQL server, then update this line in Program.cs
	"builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));"
3. To migrate/create the DB run in a Visual Studio Developer power shell the command: "dotnet ef database update" 
	(if dotnet ef command not found, then run "dotnet tool install --global dotnet-ef"

