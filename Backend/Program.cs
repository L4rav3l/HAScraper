using DotNetEnv;
using HAScraper.Infrastructure;;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<Postgresql>();

builder.Services.AddHostedService<Scanner>();

var app = builder.Build();

app.MapControllers();
app.Run();