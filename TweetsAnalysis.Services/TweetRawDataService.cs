using TweetsAnalysis.Data.Models;
using TweetsAnalysis.Data.Repository;

namespace TweetsAnalysis.Services
{
    public class TweetRawDataService : ITweetRawDataService
    {
        private readonly ITweetRawDataRepo _tweetRawDataRepo;

        public TweetRawDataService(ITweetRawDataRepo tweetRawDataRepo)
        {
            _tweetRawDataRepo = tweetRawDataRepo;
        }

        public async Task CreateMultiTweetRowDatas(List<TweetRawData> tweetRowData)
        {
            await _tweetRawDataRepo.CreateMultiTweetRowDatas(tweetRowData);
            await _tweetRawDataRepo.SaveChanges();
        }
    }
}
