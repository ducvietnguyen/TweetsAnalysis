using TweetsAnalysis.Data.Service;

public class CalculateBackgroundWorkerService : BackgroundService
{
    private readonly ILogger<BackgroundService> _logger;
    private readonly IAverageTweetsPerMinuteService _averageTweetsPerMinuteService;
    private readonly ITotalTweetsReceivedService _totalTweetsReceivedService;

    public CalculateBackgroundWorkerService(ILogger<BackgroundService> logger,
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

            // Calculate the average tweets per minute from the beginning of the day till now.
            await _averageTweetsPerMinuteService.CalculateAverageTweetsPerMinute(DateTime.Today, now);

            // Calculate total of tweets received
            await _totalTweetsReceivedService.CalculateTotalTweetsReceived();

            // Run the calculate background worker each 5 second.
            await Task.Delay(5000, stoppingToken);
        }
    }
}