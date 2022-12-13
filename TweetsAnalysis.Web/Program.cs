
using Microsoft.EntityFrameworkCore;
using Serilog;
using TweetsAnalysis.Data.Models;
using TweetsAnalysis.Data.Service;
using TweetsAnalysis.Web;
using TweetsAnalysis.Web.Consumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddHostedService<CalculateBackgroundWorkerService>();
builder.Services.AddHostedService<GetTweetBackgroundWorker>();

builder.Services.AddDbContext<TweetsAnalysisDbContext>(opt => opt.UseInMemoryDatabase("TweetsAnalysisDatabase"));

builder.Services.AddScoped<ITweetRawDataService, TweetRawDataService>();

builder.Services.AddScoped<ITotalTweetsReceivedService, TotalTweetsReceivedService>();

builder.Services.AddScoped<IAverageTweetsPerMinuteService, AverageTweetsPerMinuteService>();

builder.Services.AddScoped<ITwitterConsumer, TwitterConsumer>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
