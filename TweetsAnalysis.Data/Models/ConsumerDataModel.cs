using System.Text.Json.Serialization;

namespace TweetsAnalysis.Data.Models
{
    public class ConsumerDataModel
    {
        [JsonPropertyName("data")]
        public TweetRawData Data { get; set; }
    }
}
