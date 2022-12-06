using Microsoft.EntityFrameworkCore;
using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Data.Service
{
    public class AverageTweetsPerMinuteService : IAverageTweetsPerMinuteService
    {
        private readonly TweetsAnalysisDbContext _context;
        public AverageTweetsPerMinuteService(TweetsAnalysisDbContext context)
        {
            _context = context;
        }

        public async Task CalculateAverageTweetsPerMinuteByYesterday()
        {

            var yesterdayFrom = DateTime.Today.AddDays(-1);
            var yesterdayTo = new DateTime(yesterdayFrom.Year, yesterdayFrom.Month, yesterdayFrom.Day, 23, 59, 59);


            var rawDataYesterday = await _context.TweetRawDatas.AsNoTracking().Where(p => p.DateTimeTweet >= yesterdayFrom && p.DateTimeTweet <= yesterdayTo).ToListAsync();

            if (rawDataYesterday == null)
                return;

            var totalTweets = rawDataYesterday.Count();

            var dateRange = rawDataYesterday.Select(m => m.DateTimeTweet);

            var maxDate = dateRange.Max();
            var minDate = dateRange.Min();
            var ts = maxDate - minDate;
            var totalMinutes = ts.TotalMinutes == 0 ? 1 : ts.TotalMinutes;


            var existedObj = await GetAverageTweetsPerMinuteByDate(yesterdayFrom);

            if (existedObj == null)
            {

                existedObj = new AverageTweetsPerMinute { AverageTweets = totalTweets / totalMinutes, Date = yesterdayFrom };

                await CreateAverageTweetsPerMinute(existedObj);
                await SaveChanges();

            }
            else
            {
                existedObj.AverageTweets = totalTweets / totalMinutes;
                await UpdateAverageTweetsPerMinute(existedObj);
                await SaveChanges();

            }

        }

        public async Task CalculateAverageTweetsPerMinuteToday()
        {
            var today = DateTime.Today;
            var rawDataByToday = await _context.TweetRawDatas.AsNoTracking().Where(p => p.DateTimeTweet >= today).ToListAsync();

            if (!rawDataByToday.Any())
                return;

            var totalTweets = rawDataByToday.Count();

            var dateRange = rawDataByToday.Select(m => m.DateTimeTweet);

            var maxDate = dateRange.Max();
            var minDate = dateRange.Min();
            var ts = maxDate - minDate;
            var totalMinutes = ts.TotalMinutes == 0 ? 1 : ts.TotalMinutes;

            var averageTweetsPerMinute = totalTweets / totalMinutes;

            var existedObj = await GetAverageTweetsPerMinuteByDate(today);

            if (existedObj == null)
            {

                existedObj = new AverageTweetsPerMinute { AverageTweets = totalTweets, Date = today };

                await CreateAverageTweetsPerMinute(existedObj);
                await SaveChanges();

            }
            else
            {
                existedObj.AverageTweets = averageTweetsPerMinute;
                await UpdateAverageTweetsPerMinute(existedObj);
                await SaveChanges();

            }

        }

        public async Task<AverageTweetsPerMinute> GetAverageTweetsPerMinuteByDateByDate(DateTime dateTime)
        {
            return await GetAverageTweetsPerMinuteByDate(dateTime);
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
