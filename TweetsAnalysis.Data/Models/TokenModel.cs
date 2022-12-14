using System.Text.Json.Serialization;

namespace TweetsAnalysis.Data.Models
{
    public class TokenModel
    {
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}
