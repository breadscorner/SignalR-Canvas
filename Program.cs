using Microsoft.EntityFrameworkCore;
using Pictionary.Hubs;
using Pictionary.Models; // Add this line

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddDbContext<DatabaseContext>(
      opt =>
    {
        opt.UseInMemoryDatabase("InMemoryDatabase");
      if (builder.Environment.IsDevelopment())
      {
          opt
          .LogTo(Console.WriteLine, LogLevel.Information)
          .EnableSensitiveDataLogging()
          .EnableDetailedErrors();
      }
    }
);

app.MapGet("/", () => "Hello World!");
app.MapHub<PictionaryHub>("/r/pictionaryhub");

app.Run();
