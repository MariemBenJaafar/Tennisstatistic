using TennisStatistics.Api.Repositories;
using TennisStatistics.Api.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
builder.Services.AddSingleton<IPlayerRepository, JsonPlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IStatsService, StatsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();