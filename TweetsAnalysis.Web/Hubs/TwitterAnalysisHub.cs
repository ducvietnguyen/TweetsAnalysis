using Microsoft.AspNetCore.SignalR;

namespace TweetsAnalysis.Web.Hubs
{
    public class TwitterAnalysisHub : Hub<ITwitterAnalysisHub>
    {
        public async Task SendAnalytic(int tweetCount, int averageTweetPerMinute)
        {
            await Clients.All.ReceiveTweetsAnalytic(tweetCount, averageTweetPerMinute);
        }
    }
}
