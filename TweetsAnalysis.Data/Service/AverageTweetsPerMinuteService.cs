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

        public async Task CalculateAverageTweetsPerMinute(DateTime datetimeFrom, DateTime dateTimeTo)
        {

            var rawData = await _context.TweetRawDatas.AsNoTracking().Where(p => p.CreatedTime >= datetimeFrom && p.CreatedTime <= dateTimeTo).ToListAsync();

            if (!rawData.Any())
                return;

            var totalTweets = rawData.Count();

            var dateRange = rawData.Select(m => m.CreatedTime);

            var maxDate = dateRange.Max();
            var minDate = dateRange.Min();
            var ts = maxDate - minDate;

            var totalMinutes = ts.TotalMinutes == 0 ? 1 : ts.TotalMinutes;

            var datetimeAverageTweetsCalculated = new DateTime(datetimeFrom.Year, datetimeFrom.Month, datetimeFrom.Day);

            var averageTweetsPerMinuteObj = await GetAverageTweetsPerMinuteByDate(datetimeAverageTweetsCalculated);

            var averageTweets = Convert.ToInt32(Math.Round(totalTweets / totalMinutes));

            if (averageTweetsPerMinuteObj == null)
            {

                averageTweetsPerMinuteObj = new AverageTweetsPerMinute { AverageTweets = averageTweets, Date = datetimeAverageTweetsCalculated };

                await _context.AverageTweetsPerMinutes.AddAsync(averageTweetsPerMinuteObj);
                await _context.SaveChangesAsync();

            }
            else
            {
                averageTweetsPerMinuteObj.AverageTweets = averageTweets;
                await _context.SaveChangesAsync();

            }
        }

        public async Task<AverageTweetsPerMinute> GetAverageTweetsPerMinuteByDate(DateTime dateTime)
        {
            return await _context.AverageTweetsPerMinutes.FirstOrDefaultAsync(m => m.Date == dateTime.Date);
        }

        public async Task<int> GetAverageTweetsPerMinuteAllOfTime()
        {
            var allAverageTweetsPerMinutes = await _context.AverageTweetsPerMinutes.AsNoTracking().Select(m => m.AverageTweets).ToListAsync();
            return allAverageTweetsPerMinutes.Any() ? Convert.ToInt32(Math.Round(allAverageTweetsPerMinutes.Average())) : 0;
        }
    }
}
