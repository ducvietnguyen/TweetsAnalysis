using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Data.Service
{
    public interface ITweetRawDataService
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<TweetRawData>> GetAllTweetRowData();        
        Task CreateTweetRowData(TweetRawData tweetRowData);
        Task CreateMultiTweetRowDatas(List<TweetRawData> tweetRowDatas);

    }
}
