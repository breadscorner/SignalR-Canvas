using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pictionary.Hubs;
using Pictionary.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// Register DbContext with InMemoryDatabase
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseInMemoryDatabase("InMemoryDatabase");

    // Check if the environment is Development and configure EF Core accordingly
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure routing and endpoints
app.UseRouting();

// Define endpoints
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map the root URL to return "Hello World!"
app.MapGet("/", () => "Hello World!");

// Map SignalR hub endpoint
app.MapHub<PictionaryHub>("/r/pictionaryhub");

app.Run();
