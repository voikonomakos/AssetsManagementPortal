using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AssetsManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// CONFIGURATION SETUP
// -----------------------------

var dbConnection = builder.Configuration.GetConnectionString("Default")
                   ?? throw new Exception("Default connection string not found.");

builder.Services.Configure<DatabaseConfiguration>(options =>
{
    options.DefaultConnection = dbConnection;
});

// -----------------------------
// API CONFIG SETUP
// -----------------------------

var apiConfig = builder.Configuration.GetSection("OpenExchangeRates");
var appId = apiConfig["AppId"] ?? throw new Exception("AppId not found.");
var baseUrl = apiConfig["BaseUrl"] ?? throw new Exception("BaseUrl not found.");

// -----------------------------
// DEPENDENCY INJECTION
// -----------------------------

builder.Services.AddSingleton(new ExchangeRatesService(appId, baseUrl));

builder.Services.AddTransient<ExchangeRateRepository>();
builder.Services.AddTransient<IJobRunner, JobRunner>();

builder.Services.AddTransient<ClientsRepository>();
builder.Services.AddTransient<AssetsRepository>();
builder.Services.AddTransient<ClientAssetsRepository>();
builder.Services.AddTransient<AccountManagersRepository>();
builder.Services.AddTransient<AccountManagerClientsRepository>();
builder.Services.AddTransient<AssetCategoriesRepository>();
builder.Services.AddTransient<AuditActionsRepository>();

// -----------------------------
// HANGFIRE SETUP
// -----------------------------

builder.Services.AddHangfire(config =>
{
    config.UsePostgreSqlStorage(dbConnection);
});

builder.Services.AddHangfireServer();

var app = builder.Build();

// Enable Hangfire dashboard at /hangfire
app.UseHangfireDashboard("/hangfire");

// Add a simple GET route
app.MapGet("/", () => "Hangfire is running. Visit /hangfire to view dashboard.");

// -----------------------------
// CONSOLE MENU INTERACTION
// -----------------------------

using var scope = app.Services.CreateScope();

var jobs = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
var jobRunner = scope.ServiceProvider.GetRequiredService<IJobRunner>();

var clientsRepo = scope.ServiceProvider.GetRequiredService<ClientsRepository>();
var assetsRepo = scope.ServiceProvider.GetRequiredService<AssetsRepository>();
var clientAssetsRepo = scope.ServiceProvider.GetRequiredService<ClientAssetsRepository>();
var accountManagersRepo = scope.ServiceProvider.GetRequiredService<AccountManagersRepository>();
var accountManagerClientsRepo = scope.ServiceProvider.GetRequiredService<AccountManagerClientsRepository>();
var assetCategoriesRepo = scope.ServiceProvider.GetRequiredService<AssetCategoriesRepository>();
var auditActionsRepo = scope.ServiceProvider.GetRequiredService<AuditActionsRepository>();

while (true)
{
    Console.Clear();
    Console.WriteLine("""
    ===== Exchange Rate App Menu =====
    1. Register recurring Hangfire job
    2. Ignore for now please :\
    3. Show Hangfire Dashboard URL
    4. Insert Client
    5. View All Clients
    6. Insert Asset
    7. View All Assets
    8. Assign Asset to Client
    9. View Assets by Client
    10. Insert Account Manager
    11. View All Account Managers
    12. Assign Client to Account Manager
    13. View Clients by Account Manager
    14. Insert Asset Category
    15. View All Asset Categories
    16. Insert Audit Action
    17. View All Audit Actions
    18. Start Server (Dashboard + Background Jobs)
    0. Exit
    """);

    Console.Write("Select an option: ");
    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            jobs.AddOrUpdate("fetch-and-store-exchange-rates", () => jobRunner.Run(), Cron.Minutely);
            Console.WriteLine("Recurring job registered.");
            break;

        case "2":
            // BackgroundJob.Enqueue(() => jobRunner.Run());
            // Console.WriteLine("Job triggered immediately.");
            break;

        case "3":
            Console.WriteLine("Visit: http://localhost:5000/hangfire");
            break;

        case "4":
            Console.Write("Client name: ");
            var cname = Console.ReadLine();
            await clientsRepo.InsertClient(new Clients { Name = cname! });
            Console.WriteLine("Client inserted.");
            break;

        case "5":
            var clients = await clientsRepo.GetAllClients();
            foreach (var c in clients)
                Console.WriteLine($"[{c.Id}] {c.Name}");
            break;

        case "6":
            Console.Write("Asset name: ");
            var aname = Console.ReadLine();
            Console.Write("Category ID: ");
            int.TryParse(Console.ReadLine(), out int catId);
            await assetsRepo.InsertAsset(new Assets { Name = aname!, CategoryId = catId });
            Console.WriteLine("Asset inserted.");
            break;

        case "7":
            var assets = await assetsRepo.GetAllAssets();
            foreach (var a in assets)
                Console.WriteLine($"[{a.Id}] {a.Name} (CategoryId: {a.CategoryId})");
            break;

        case "8":
            Console.Write("Client ID: ");
            int.TryParse(Console.ReadLine(), out int clientId);
            Console.Write("Asset ID: ");
            int.TryParse(Console.ReadLine(), out int assetId);
            Console.Write("Value: ");
            decimal.TryParse(Console.ReadLine(), out decimal value);
            Console.Write("Date (yyyy-mm-dd): ");
            DateTime.TryParse(Console.ReadLine(), out DateTime assetDate);
            await clientAssetsRepo.InsertClientAsset(new ClientAssets(clientId, assetId, value, assetDate));
            Console.WriteLine("Asset assigned to client.");
            break;

        case "9":
            Console.Write("Client ID: ");
            int.TryParse(Console.ReadLine(), out int cid);
            var clientAssets = await clientAssetsRepo.GetClientAssetsByClient(cid);
            foreach (var ca in clientAssets)
                Console.WriteLine($"AssetId: {ca.AssetId}, Value: {ca.Value}, Date: {ca.AssetDate:yyyy-MM-dd}");
            break;

        case "10":
            Console.Write("Manager name: ");
            var mname = Console.ReadLine();
            await accountManagersRepo.InsertAccountManager(new AccountManagers { Name = mname! });
            Console.WriteLine("Account manager inserted.");
            break;

        case "11":
            var managers = await accountManagersRepo.GetAllAccountManagers();
            foreach (var m in managers)
                Console.WriteLine($"[{m.Id}] {m.Name}");
            break;

        case "12":
            Console.Write("Manager ID: ");
            int.TryParse(Console.ReadLine(), out int mid);
            Console.Write("Client ID: ");
            int.TryParse(Console.ReadLine(), out int cmid);
            await accountManagerClientsRepo.InsertAccountManagerClient(new AccountManagerClients(mid, cmid));
            Console.WriteLine("Client assigned to manager.");
            break;

        case "13":
            Console.Write("Manager ID: ");
            int.TryParse(Console.ReadLine(), out int mgrid);
            var assigned = await accountManagerClientsRepo.GetClientsByAccountManagers(mgrid);
            foreach (var amc in assigned)
                Console.WriteLine($"Client ID: {amc.ClientId}");
            break;

        case "14":
            Console.Write("Category ID: ");
            int.TryParse(Console.ReadLine(), out int catid);
            Console.Write("Category Name: ");
            var catname = Console.ReadLine();
            Console.Write("Description: ");
            var desc = Console.ReadLine();
            await assetCategoriesRepo.InsertAssetCategory(new AssetCategories(catid, catname!, desc));
            Console.WriteLine("Asset category inserted.");
            break;

        case "15":
            var cats = await assetCategoriesRepo.GetAllAssetCategories();
            foreach (var cat in cats)
                Console.WriteLine($"[{cat.Id}] {cat.Name} - {cat.Description}");
            break;

        case "16":
            Console.Write("Action ID: ");
            int.TryParse(Console.ReadLine(), out int aid);
            Console.Write("Action Name: ");
            var an = Console.ReadLine();
            await auditActionsRepo.InsertAuditActions(new AuditActions(aid, an!));
            Console.WriteLine("Audit action inserted.");
            break;

        case "17":
            var acts = await auditActionsRepo.GetAllAuditActions();
            foreach (var act in acts)
                Console.WriteLine($"[{act.Id}] {act.Name}");
            break;

        case "18":
            Console.WriteLine("Starting Hangfire Server + Web Dashboard...");
            app.Run(); // Will block here — host the server
            return;

        case "0":
            Console.WriteLine("Exiting app...");
            return;

        default:
            Console.WriteLine("Invalid option. Try again.");
            break;
    }

    Console.WriteLine("\nPress any key to return to menu...");
    Console.ReadKey();
}
