using Microsoft.EntityFrameworkCore;

namespace TweetsAnalysis.Data.Models
{
    public class TweetsAnalysisDbContext: DbContext
    {
        public TweetsAnalysisDbContext(DbContextOptions<TweetsAnalysisDbContext> options) : base(options)
        {

        }

        public DbSet<TweetRowData> TweetRowDatas { get; set; }

    }
}
