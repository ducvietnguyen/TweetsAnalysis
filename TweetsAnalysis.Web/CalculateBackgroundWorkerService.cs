using TweetsAnalysis.Data.Service;

public class CalculateBackgroundWorkerService : BackgroundService
{
    private readonly ILogger<CalculateBackgroundWorkerService> _logger;
    private readonly IAverageTweetsPerMinuteService _averageTweetsPerMinuteService;
    private readonly ITotalTweetsReceivedService _totalTweetsReceivedService;

    public CalculateBackgroundWorkerService(ILogger<CalculateBackgroundWorkerService> logger,
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
            var now = DateTime.Now;            
            try
            {
                // Calculate the average tweets per minute from the beginning of the day till now.
                await _averageTweetsPerMinuteService.CalculateAverageTweetsPerMinute(DateTime.Today, now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to calculate average tweets per minute");
            }

            try
            {
                // Calculate total of tweets received
                await _totalTweetsReceivedService.CalculateTotalTweetsReceived();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to calculate total tweets received");
            }            

            // Run the calculate background worker each 5 second.
            await Task.Delay(5000, stoppingToken);
        }
    }
}