using IdentityServer;
using IdentityServer.Data.Services;
using IdentityServerHost.Quickstart.UI;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
var connString = builder.Configuration.GetConnectionString("IS4-MySql") ?? throw new Exception("IS4-MySql connection string is missing");

builder.Services.AddControllersWithViews();
builder.Services.AddIdentityServer()
    .AddTestUsers(TestUsers.Users)
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseMySQL(connString, sql => sql.MigrationsAssembly(migrationsAssembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => b.UseMySQL(connString, sql => sql.MigrationsAssembly(migrationsAssembly));
    })
    .AddDeveloperSigningCredential();

builder.Services.AddScoped<DbInitializerService>();

//builder.Services.AddIdentityServer()
//    .AddInMemoryClients(Config.Clients)
//    .AddInMemoryIdentityResources(Config.IdentityResources)
//    //.AddInMemoryApiResources(Config.ApiResources)
//    .AddInMemoryApiScopes(Config.ApiScopes)
//    .AddTestUsers(TestUsers.Users)
//    .AddDeveloperSigningCredential();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.MapDefaultControllerRoute();

InitializeDatabase();
app.Run();

void InitializeDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializerService>();
        dbInitializer.Initialize();
    }
}