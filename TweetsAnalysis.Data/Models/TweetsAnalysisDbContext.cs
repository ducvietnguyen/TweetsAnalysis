using Microsoft.EntityFrameworkCore;

namespace TweetsAnalysis.Data.Models
{
    public class TweetsAnalysisDbContext : DbContext
    {
        public TweetsAnalysisDbContext(DbContextOptions<TweetsAnalysisDbContext> options) : base(options)
        {

        }

        public DbSet<TweetRawData> TweetRawDatas { get; set; }
        public DbSet<TotalTweetsReceived> TotalTweetsReceiveds { get; set; }
        public DbSet<AverageTweetsPerMinute> AverageTweetsPerMinutes { get; set; }
        
    }
}
