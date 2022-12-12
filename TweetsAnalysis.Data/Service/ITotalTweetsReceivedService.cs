using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Data.Service
{
    public interface ITotalTweetsReceivedService
    {
        Task<bool> SaveChanges();
        Task<int> GetTotalTweetsReceived();
        Task CreateTotalTweetsReceived(TotalTweetsReceived entity);

        Task UpdateTotalTweetsReceived(TotalTweetsReceived updateEntity);
        Task CalculateTotalTweetsReceived();
    }
}
