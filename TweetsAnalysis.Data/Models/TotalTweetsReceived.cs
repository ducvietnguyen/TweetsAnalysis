using System.ComponentModel.DataAnnotations;

namespace TweetsAnalysis.Data.Models
{
    public class TotalTweetsReceived
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TotalTweets { get; set; }

        [Required]
        public DateTime LatestDateCalculate { get; set; }
    }
}
