using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Data.Repository
{
    public interface IAverageTweetsPerMinuteRepo
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<AverageTweetsPerMinute>> GetAllAverageTweetsPerMinute();
        Task<AverageTweetsPerMinute> GetAverageTweetsPerMinuteByDate(DateTime dateTime);
        Task CreateAverageTweetsPerMinute(AverageTweetsPerMinute newEntity);
        Task UpdateAverageTweetsPerMinute(AverageTweetsPerMinute updateEntity);
    }
}
