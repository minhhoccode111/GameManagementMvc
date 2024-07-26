using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using MvcMovie.Models;

var builder = WebApplication.CreateBuilder(args);

// database context default config to development using sqlite
builder.Services.AddDbContext<MvcMovieContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("MvcMovieContext")
            // or throw an error
            ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")
    )
);

// dotnet ef migrations add InitialCreate
// dotnet ef database update
if (builder.Environment.IsDevelopment())
{
    // use sqlite in development and MvcMovieContext string in appsettings.json
    builder.Services.AddDbContext<MvcMovieContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("MvcMovieContext"))
    );
}
else
{
    // use ms sql in production and MvcMovieContext string in appsettings.json
    builder.Services.AddDbContext<MvcMovieContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionMvcMovieContext"))
    );
}

// Add services to the container.
builder.Services.AddControllersWithViews();

// build the app
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// redirect http request to https
app.UseHttpsRedirection();

// Enables static file serving for the current request path like css and js
// default is wwwroot sub dir
app.UseStaticFiles();

//
app.UseRouting();

// enables authorization capabilities
app.UseAuthorization();

/*
   E.g.
   home/ == home/index
   movies/ == movies/index
   movies/create
   movies/delete/id

   (extension) ControllerActionEndpointConventionBuilder IEndpointRouteBuilder.MapControllerRoute(string name, string pattern, [object? defaults = null], [object? constraints = null], [object? dataTokens = null])

   Adds endpoints for controller actions to the `IEndpointRouteBuilder` and specifies a route with the given `name`, `pattern`, `defaults`, `constraints`, and `dataTokens`

   define a url pattern
*/
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

// runs an application and block the calling thread until host shutdown
app.Run();
