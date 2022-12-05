using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Data.Repository
{
    public interface ITweetRowDataRepo
    {
        bool SaveChanges();
        IEnumerable<TweetRowData> GetAllTweetRowData();
        TweetRowData GetTweetRowDataById(int id);
        void CreateTweetRowData(TweetRowData tweetRowData);

    }
}
