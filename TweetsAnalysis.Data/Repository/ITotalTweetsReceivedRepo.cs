using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Data.Repository
{
    public interface ITotalTweetsReceivedRepo
    {
        Task<bool> SaveChanges();
        Task<TotalTweetsReceived> GetTotalTweetsReceived();
        Task CreateTotalTweetsReceived(TotalTweetsReceived entity);

        Task UpdateTotalTweetsReceived(TotalTweetsReceived updateEntity);
    }
}
