DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

var allowedOrigin = Environment.GetEnvironmentVariable("CORS_ALLOWED_ORIGIN") ?? "http://localhost:5173";

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactClient", policy =>
    {
        policy.WithOrigins(allowedOrigin)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddSignalR();
builder.Services.AddHostedService<MatchEventSim>(); 
builder.Services.AddControllers();

var app = builder.Build();
app.UseCors("AllowReactClient");
app.UseRouting();
app.MapControllers();
app.MapHub<MatchHub>("/matchHub");
app.Run();