using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Data.Repository
{
    public interface ITweetRawDataRepo
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<TweetRawData>> GetAllTweetRowData();
        Task<IEnumerable<TweetRawData>> GetTweetRowDataFromDate(DateTime dateTime);
        Task<IEnumerable<TweetRawData>> GetTweetRowDataFromDateToDate(DateTime dateTimeFrom, DateTime dateTimeTo);
        Task<IEnumerable<TweetRawData>> GetTweetRowDataByDate(DateTime dateTime);
        Task CreateTweetRowData(TweetRawData tweetRowData);
        Task CreateMultiTweetRowDatas(List<TweetRawData> tweetRowDatas);

    }
}
