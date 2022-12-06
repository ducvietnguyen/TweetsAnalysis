using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Data.Service
{
    public interface IAverageTweetsPerMinuteService
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<AverageTweetsPerMinute>> GetAllAverageTweetsPerMinute();
        Task<AverageTweetsPerMinute> GetAverageTweetsPerMinuteByDate(DateTime dateTime);
        Task CreateAverageTweetsPerMinute(AverageTweetsPerMinute newEntity);
        Task UpdateAverageTweetsPerMinute(AverageTweetsPerMinute updateEntity);
        Task CalculateAverageTweetsPerMinuteByYesterday();
        Task CalculateAverageTweetsPerMinuteToday();
    }
}
