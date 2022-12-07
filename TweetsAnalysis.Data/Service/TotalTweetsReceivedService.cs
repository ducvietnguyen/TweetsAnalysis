using Microsoft.EntityFrameworkCore;
using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Data.Service
{
    public class TotalTweetsReceivedService : ITotalTweetsReceivedService
    {
        private readonly TweetsAnalysisDbContext _context;

        public TotalTweetsReceivedService(TweetsAnalysisDbContext context)
        {
            _context = context;
        }
        public async Task CreateTotalTweetsReceived(TotalTweetsReceived entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.TotalTweetsReceiveds.AddAsync(entity);
        }

        public async Task<TotalTweetsReceived> GetTotalTweetsReceived()
        {
            var totalTweetsReceivedObj = await _context.TotalTweetsReceiveds.FirstOrDefaultAsync();
            return totalTweetsReceivedObj;
        }

        public async Task UpdateTotalTweetsReceived(TotalTweetsReceived updateEntity)
        {
            if (updateEntity == null)
            {
                throw new ArgumentNullException(nameof(updateEntity));
            }

            var updateObj = await _context.TotalTweetsReceiveds.FirstOrDefaultAsync();

            if (updateObj == null)
            {
                throw new ArgumentNullException(nameof(updateEntity));
            }

            updateObj.LatestDateCalculate = updateEntity.LatestDateCalculate;
            updateObj.TotalTweets = updateObj.TotalTweets;
        }

        public async Task CalculateTotalTweetsReceived()
        {
            var totalTweetsReceived = await GetTotalTweetsReceived();

            if (totalTweetsReceived == null)
            {
                var tweetRawData = await _context.TweetRawDatas.ToListAsync();

                // If there are no tweetRawData then stop processing.
                if (!tweetRawData.Any())
                    return;

                var totalTweets = tweetRawData.Count();

                var maxDate = tweetRawData.Select(m => m.CreatedTime).Max();

                var newTotalTweetsReceived = new TotalTweetsReceived { LatestDateCalculate = maxDate, TotalTweets = totalTweets };

                await CreateTotalTweetsReceived(newTotalTweetsReceived);
                await SaveChanges();

            }
            else
            {
                var tweetRawData = await _context.TweetRawDatas.AsNoTracking().Where(p => p.CreatedTime > totalTweetsReceived.LatestDateCalculate).ToListAsync();
                var totalTweets = tweetRawData.Count();

                var maxDate = tweetRawData.Select(m => m.CreatedTime).Max();

                totalTweetsReceived.LatestDateCalculate = maxDate;
                totalTweetsReceived.TotalTweets = totalTweetsReceived.TotalTweets + totalTweets;

                await UpdateTotalTweetsReceived(totalTweetsReceived);
                await SaveChanges();
            }
        }
        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

    }
}
