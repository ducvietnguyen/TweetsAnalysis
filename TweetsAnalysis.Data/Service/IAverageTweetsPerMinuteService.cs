using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Data.Service
{
    public interface IAverageTweetsPerMinuteService
    {
        Task<int> GetAverageTweetsPerMinuteByDate(DateTime dateTime);
        Task CalculateAverageTweetsPerMinute(DateTime datetimeFrom, DateTime dateTimeTo);
        Task<int> GetAverageTweetsPerMinuteAllOfTime();
    }
}
