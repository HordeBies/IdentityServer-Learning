using IdentityServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients)
    //.AddInMemoryIdentityResources(Config.IdentityResources)
    //.AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    //.AddTestUsers(Config.TestUsers)
    .AddDeveloperSigningCredential();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("default", policy =>
//    {
//        policy.WithOrigins("http://localhost:5001", "https://jwt.io")
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//    });
//});

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
//app.UseCors("default");
app.UseIdentityServer();

app.MapGet("/", () => "Hello World!");

app.Run();
