using Krtshk.Models;
using Krtshk.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<ILinkRepository, LinkRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<IDatabaseContext>();
    context.InitializeDatabase();
}

app.MapRazorPages();

app.Run();