using Microsoft.AspNetCore.SignalR;
using TweetsAnalysis.Data.Service;
using TweetsAnalysis.Web.Hubs;

namespace TweetsAnalysis.Web
{
    public class PopulateTweetAnalyticWorker : BackgroundService
    {
        private readonly IHubContext<TwitterAnalysisHub, ITwitterAnalysisHub> _hub;
        private readonly ILogger<PopulateTweetAnalyticWorker> _logger;
        private readonly IAverageTweetsPerMinuteService _averageTweetsPerMinuteService;
        private readonly ITotalTweetsReceivedService _totalTweetsReceivedService;

        public PopulateTweetAnalyticWorker(ILogger<PopulateTweetAnalyticWorker> logger, IServiceScopeFactory factory, IHubContext<TwitterAnalysisHub, ITwitterAnalysisHub> hub)
        {
            _logger = logger;
            _hub = hub;
            _averageTweetsPerMinuteService = factory.CreateScope().ServiceProvider.GetRequiredService<IAverageTweetsPerMinuteService>();
            _totalTweetsReceivedService = factory.CreateScope().ServiceProvider.GetRequiredService<ITotalTweetsReceivedService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var tweetCount = await _totalTweetsReceivedService.GetTotalTweetsReceived();
                    var averageTweetPerMinute = await _averageTweetsPerMinuteService.GetAverageTweetsPerMinuteAllOfTime();
                    await _hub.Clients.All.ReceiveTweetsAnalytic(10, 20);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "There was error when analytic tweet");
                    await _hub.Clients.All.ReceiveError(ex.Message);
                }

                await Task.Delay(1000);
            }
        }
    }
}
