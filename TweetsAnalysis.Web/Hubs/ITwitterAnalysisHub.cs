namespace TweetsAnalysis.Web.Hubs
{
    public interface ITwitterAnalysisHub
    {
        Task ReceiveTweetsAnalytic(int tweetCount, int averageTweetPerMinute);
        Task ReceiveError(string message);
    }
}
