using Microsoft.EntityFrameworkCore;
using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Data.Service
{
    public class TweetRawDataService : ITweetRawDataService
    {
        private readonly TweetsAnalysisDbContext _context;

        public TweetRawDataService(TweetsAnalysisDbContext context)
        {
            _context = context;
        }
        public async Task CreateTweetRowData(TweetRawData tweetRowData)
        {
            if (tweetRowData == null)
            {
                throw new ArgumentNullException(nameof(tweetRowData));
            }

            await _context.TweetRawDatas.AddAsync(tweetRowData);
        }
        public async Task CreateMultiTweetRowDatas(List<TweetRawData> tweetRowDatas)
        {
            if (tweetRowDatas == null)
            {
                throw new ArgumentNullException(nameof(TweetRawData));
            }

            await _context.TweetRawDatas.AddRangeAsync(tweetRowDatas);
        }


        public async Task<IEnumerable<TweetRawData>> GetAllTweetRowData()
        {
            return await _context.TweetRawDatas.ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
