using DotNetEnv;
using HAScraper.Infrastructure;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<Postgresql>();

builder.Services.AddHostedService<Scanner>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactAppPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()  
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("ReactAppPolicy");
app.MapControllers();
app.Run();