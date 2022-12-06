using System.ComponentModel.DataAnnotations;

namespace TweetsAnalysis.Data.Models
{
    public class TotalTweetsReceived
    {
        [Required]
        public int TotalTweets { get; set; }

        [Required]
        public DateTime LatestDateCalculate { get; set; }
    }
}
