using Microsoft.EntityFrameworkCore;
using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Test
{
    public class TestBase
    {
        private DbContextOptions<TweetsAnalysisDbContext> options;
        public TweetsAnalysisDbContext Context;

        public TestBase()
        {
            options = new DbContextOptionsBuilder<TweetsAnalysisDbContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;

            var initContext = new TweetsAnalysisDbContext(options);
            initContext.Database.EnsureDeleted();
            Context = new TweetsAnalysisDbContext(options);
        }

    }
}
