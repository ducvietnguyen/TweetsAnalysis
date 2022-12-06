using TweetsAnalysis.Data.Service;

public class BackgroundWorkerService : BackgroundService
{
    private readonly ILogger<BackgroundService> _logger;
    private readonly IAverageTweetsPerMinuteService _averageTweetsPerMinuteService;
    private readonly ITotalTweetsReceivedService _totalTweetsReceivedService;

    public BackgroundWorkerService(ILogger<BackgroundService> logger, 
        IServiceScopeFactory factory)
    {
        _logger = logger;
        _averageTweetsPerMinuteService = factory.CreateScope().ServiceProvider.GetRequiredService<IAverageTweetsPerMinuteService>();
        _totalTweetsReceivedService = factory.CreateScope().ServiceProvider.GetRequiredService<ITotalTweetsReceivedService>();

    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"Worker run at time: {DateTime.Now.ToString("dd-MM-yyy hh:MM:ss")}");

            var now = DateTime.Now;

            if (now.Hour == 0 && now.Minute == 0 && now.Second <= 5 + 1)
            {
                await _averageTweetsPerMinuteService.CalculateAverageTweetsPerMinuteByYesterday();
            }

            await _averageTweetsPerMinuteService.CalculateAverageTweetsPerMinuteToday();

            await _totalTweetsReceivedService.CalculateTotalTweetsReceived();

            await Task.Delay(5000, stoppingToken);
        }
    }
}