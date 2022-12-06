using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Services
{
    public interface IAverageTweetsPerMinuteByDateService
    {
        Task<AverageTweetsPerMinute> GetAverageTweetsPerMinuteByDateByDate(DateTime dateTime);

        Task CalculateAverageTweetsPerMinuteByYesterday();
        Task CalculateAverageTweetsPerMinuteToday();
    }
}