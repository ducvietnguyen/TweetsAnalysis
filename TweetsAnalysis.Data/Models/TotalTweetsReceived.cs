using System.ComponentModel.DataAnnotations;

namespace TweetsAnalysis.Data.Models
{
    public class TotalTweetsReceived
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TotalTweets { get; set; }

        /// <summary>
        /// The point of time that total tweets was calculated
        /// </summary>
        [Required]
        public DateTime LatestDateCalculate { get; set; }
    }
}
