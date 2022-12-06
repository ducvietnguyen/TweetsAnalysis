using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Services
{
    public interface ITotalTweetsReceivedService
    {
        Task<TotalTweetsReceived> GetTotalTweetsReceived();       

        Task CalculateTotalTweetsReceived();
    }
}
