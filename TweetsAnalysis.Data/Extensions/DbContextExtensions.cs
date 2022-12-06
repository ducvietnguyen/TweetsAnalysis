using Microsoft.EntityFrameworkCore;
using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static void DeleteAll<T>(this TweetsAnalysisDbContext context)
        where T : class
        {
            foreach (var p in context.Set<T>())
            {
                context.Entry(p).State = EntityState.Deleted;
            }
        }
    }
}
