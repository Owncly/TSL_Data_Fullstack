var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddHostedService<MatchEventSim>();
builder.Services.AddControllers();

var app = builder.Build();
app.Run();