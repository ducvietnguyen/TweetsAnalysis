using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TweetsAnalysis.Data.Models
{
    public class TweetRawData
    {
        [Key]
        [Required]
        [JsonPropertyName("id")]
        public string Id { get; set; }
       
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
