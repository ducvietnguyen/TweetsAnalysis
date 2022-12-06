using System.ComponentModel.DataAnnotations;

namespace TweetsAnalysis.Data.Models
{
    public class AverageTweetsPerMinute
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double AverageTweets { get; set; }
    }
}
