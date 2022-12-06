using Microsoft.EntityFrameworkCore;
using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Data.Repository
{
    public class AverageTweetsPerMinuteRepo : IAverageTweetsPerMinuteRepo
    {
        private readonly TweetsAnalysisDbContext _context;
        public AverageTweetsPerMinuteRepo(TweetsAnalysisDbContext context)
        {
            _context = context;
        }

        public async Task CreateAverageTweetsPerMinute(AverageTweetsPerMinute newEntity)
        {
            if (newEntity == null)
            {
                throw new ArgumentNullException(nameof(newEntity));
            }

            await _context.AverageTweetsPerMinutes.AddAsync(newEntity);
        }

        public async Task<IEnumerable<AverageTweetsPerMinute>> GetAllAverageTweetsPerMinute()
        {
            return await _context.AverageTweetsPerMinutes.ToListAsync();
        }

        public async Task<AverageTweetsPerMinute> GetAverageTweetsPerMinuteById(int id)
        {
            return await _context.AverageTweetsPerMinutes.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<AverageTweetsPerMinute> GetAverageTweetsPerMinuteByDate(DateTime dateTime)
        {
            return await _context.AverageTweetsPerMinutes.FirstOrDefaultAsync(m => m.Date == dateTime);
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task UpdateAverageTweetsPerMinute(AverageTweetsPerMinute updateEntity)
        {
            if (updateEntity == null)
            {
                throw new ArgumentNullException(nameof(updateEntity));
            }

            var updateObj = await _context.AverageTweetsPerMinutes.FirstOrDefaultAsync(m => m.Id == updateEntity.Id);

            if (updateObj == null)
            {
                throw new ArgumentNullException(nameof(updateEntity));
            }

            updateObj.AverageTweets = updateEntity.AverageTweets;

        }
    }
}
