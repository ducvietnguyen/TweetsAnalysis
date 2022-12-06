using Microsoft.EntityFrameworkCore;
using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Data.Repository
{
    public class TotalTweetsReceivedRepo : ITotalTweetsReceivedRepo
    {
        private readonly TweetsAnalysisDbContext _context;

        public TotalTweetsReceivedRepo(TweetsAnalysisDbContext context)
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
        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

    }
}
