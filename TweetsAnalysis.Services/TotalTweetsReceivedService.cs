using TweetsAnalysis.Data.Models;
using TweetsAnalysis.Data.Repository;

namespace TweetsAnalysis.Services
{
    public class TotalTweetsReceivedService : ITotalTweetsReceivedService
    {
        private readonly ITotalTweetsReceivedRepo _totalTweetsReceivedRepo;
        private readonly ITweetRawDataRepo _tweetRawDataRepo;

        public TotalTweetsReceivedService(ITotalTweetsReceivedRepo totalTweetsReceivedRepo, ITweetRawDataRepo tweetRawDataRepo)
        {
            _totalTweetsReceivedRepo = totalTweetsReceivedRepo;
            _tweetRawDataRepo = tweetRawDataRepo;
        }

        public async Task CalculateTotalTweetsReceived()
        {
            var totalTweetsReceived = await _totalTweetsReceivedRepo.GetTotalTweetsReceived();

            if (totalTweetsReceived == null)
            {
                var tweetRawData = await _tweetRawDataRepo.GetAllTweetRowData();
                var totalTweets = tweetRawData.Count();

                var maxDate = tweetRawData.Select(m => m.DateTimeTweet).Max();

                var newTotalTweetsReceived = new TotalTweetsReceived { LatestDateCalculate = maxDate, TotalTweets = totalTweets };

                await _totalTweetsReceivedRepo.CreateTotalTweetsReceived(newTotalTweetsReceived);
                await _totalTweetsReceivedRepo.SaveChanges();

            }
            else
            {
                var tweetRawData = await _tweetRawDataRepo.GetTweetRowDataFromDate(totalTweetsReceived.LatestDateCalculate.AddSeconds(1));
                var totalTweets = tweetRawData.Count();

                var maxDate = tweetRawData.Select(m => m.DateTimeTweet).Max();

                totalTweetsReceived.LatestDateCalculate = maxDate;
                totalTweetsReceived.TotalTweets = totalTweetsReceived.TotalTweets + totalTweets;

                await _totalTweetsReceivedRepo.UpdateTotalTweetsReceived(totalTweetsReceived);
                await _totalTweetsReceivedRepo.SaveChanges();
            }
        }

        public async Task<TotalTweetsReceived> GetTotalTweetsReceived()
        {
            return await _totalTweetsReceivedRepo.GetTotalTweetsReceived();
        }
    }
}
