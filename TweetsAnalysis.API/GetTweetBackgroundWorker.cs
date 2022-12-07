using System.Text;
using TweetsAnalysis.API.Consumer;
using TweetsAnalysis.Common;
using TweetsAnalysis.Data.Models;
using TweetsAnalysis.Data.Service;

namespace TweetsAnalysis.API
{
    public class GetTweetBackgroundWorker : BackgroundService
    {
        private readonly ILogger<BackgroundService> _logger;
        private readonly ITweetRawDataService _tweetRawDataService;
        private readonly ITwitterConsumer _twitterConsumer;

        public GetTweetBackgroundWorker(ILogger<BackgroundService> logger, IServiceScopeFactory factory)
        {
            _logger = logger;
            _tweetRawDataService = factory.CreateScope().ServiceProvider.GetRequiredService<ITweetRawDataService>();
            _twitterConsumer = factory.CreateScope().ServiceProvider.GetRequiredService<ITwitterConsumer>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var stream = await _twitterConsumer.GetStreamAsync();
                var json = string.Empty;

                while (!stoppingToken.IsCancellationRequested)
                {
                    if (stream.CanRead)
                    {
                        // get json string
                        var buffer = new byte[1024];
                        var length = await stream.ReadAsync(buffer, 0, buffer.Length);
                        json += Encoding.UTF8.GetString(buffer, 0, length).Trim();
                        _logger.LogInformation("API Response: {Json}", json);
                        // convert to model
                        var consumerData = SerializationHelper<ConsumerDataModel>.Deserialize(ref json);

                        if (consumerData.Any())
                        {
                            // save to db
                            var tweetRawDatas = consumerData.Select(m => m.Data).ToList();

                            await _tweetRawDataService.CreateMultiTweetRowDatas(tweetRawDatas);
                            await _tweetRawDataService.SaveChanges();
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
