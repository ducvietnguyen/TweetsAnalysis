using System.ComponentModel.DataAnnotations;

namespace TweetsAnalysis.Data.Models
{
    public class TweetRowData
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Content { get; set; }

        [Required]
        public DateTime DateTimeTweet { get; set; }
    }
}
