namespace TweetsAnalysis.API.Consumer
{
    public interface ITwitterConsumer
    {
        Task<Stream> GetStreamAsync();
    }
}
