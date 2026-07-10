using DotNetEnv;
using HAScraper.Infrastructure;;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();