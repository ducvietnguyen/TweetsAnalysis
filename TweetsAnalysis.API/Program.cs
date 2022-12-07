using Microsoft.EntityFrameworkCore;
using TweetsAnalysis.API;
using TweetsAnalysis.API.Consumer;
using TweetsAnalysis.Data.Models;
using TweetsAnalysis.Data.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHostedService<CalculateBackgroundWorkerService>();
builder.Services.AddHostedService<GetTweetBackgroundWorker>(); 

builder.Services.AddDbContext<TweetsAnalysisDbContext>(opt => opt.UseInMemoryDatabase("TweetsAnalysisDatabase"));

builder.Services.AddScoped<ITweetRawDataService, TweetRawDataService>();

builder.Services.AddScoped<ITotalTweetsReceivedService, TotalTweetsReceivedService>();

builder.Services.AddScoped<IAverageTweetsPerMinuteService, AverageTweetsPerMinuteService>();

builder.Services.AddScoped<ITwitterConsumer, TwitterConsumer>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
