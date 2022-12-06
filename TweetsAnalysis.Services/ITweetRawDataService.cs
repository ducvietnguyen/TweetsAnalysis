using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Services
{
    public interface ITweetRawDataService
    {
        Task CreateMultiTweetRowDatas(List<TweetRawData> tweetRowData);
    }
}
