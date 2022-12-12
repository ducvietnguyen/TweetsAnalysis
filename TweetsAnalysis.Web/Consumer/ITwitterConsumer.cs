namespace TweetsAnalysis.Web.Consumer
{
    public interface ITwitterConsumer
    {
        Task<Stream> GetStreamAsync();
    }
}
