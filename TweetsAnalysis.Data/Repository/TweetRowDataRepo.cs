using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetsAnalysis.Data.Models;

namespace TweetsAnalysis.Data.Repository
{
    public class TweetRowDataRepo : ITweetRowDataRepo
    {
        private readonly TweetsAnalysisDbContext _context;

        public TweetRowDataRepo(TweetsAnalysisDbContext context)
        {
            _context = context;
        }
        public void CreateTweetRowData(TweetRowData tweetRowData)
        {
            if (tweetRowData == null)
            {
                throw new ArgumentNullException(nameof(tweetRowData));
            }

            _context.TweetRowDatas.Add(tweetRowData);
        }

        public IEnumerable<TweetRowData> GetAllTweetRowData()
        {
            return _context.TweetRowDatas.ToList();
        }

        public TweetRowData GetTweetRowDataById(int id)
        {
            return _context.TweetRowDatas.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
