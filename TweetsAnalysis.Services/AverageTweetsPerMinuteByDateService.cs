//using TweetsAnalysis.Data.Models;
//using TweetsAnalysis.Data.Repository;

//namespace TweetsAnalysis.Services
//{
//    public class AverageTweetsPerMinuteByDateService : IAverageTweetsPerMinuteByDateService
//    {
//        private readonly IAverageTweetsPerMinuteRepo _averageTweetsPerMinuteByDateRepo;
//        private readonly ITweetRawDataRepo _tweetRawDataRepo;

//        public AverageTweetsPerMinuteByDateService(IAverageTweetsPerMinuteRepo averageTweetsPerMinuteByDateRepo, ITweetRawDataRepo tweetRawDataRepo)
//        {
//            _averageTweetsPerMinuteByDateRepo = averageTweetsPerMinuteByDateRepo;
//            _tweetRawDataRepo = tweetRawDataRepo;
//        }

//        public async Task CalculateAverageTweetsPerMinuteByYesterday()
//        {

//            var yesterdayFrom = DateTime.Today.AddDays(-1);
//            var yesterdayTo = new DateTime(yesterdayFrom.Year, yesterdayFrom.Month, yesterdayFrom.Day, 23, 59, 59);


//            var rawDataYesterday = await _tweetRawDataRepo.GetTweetRowDataFromDateToDate(yesterdayFrom, yesterdayTo);

//            if (rawDataYesterday == null)
//                return;

//            var totalTweets = rawDataYesterday.Count();

//            var dateRange = rawDataYesterday.Select(m => m.DateTimeTweet);

//            var maxDate = dateRange.Max();
//            var minDate = dateRange.Min();
//            var ts = maxDate - minDate;
//            var totalMinutes = ts.TotalMinutes == 0 ? 1 : ts.TotalMinutes;


//            var existedObj = await _averageTweetsPerMinuteByDateRepo.GetAverageTweetsPerMinuteByDate(yesterdayFrom);

//            if (existedObj == null)
//            {

//                existedObj = new AverageTweetsPerMinute { AverageTweets = totalTweets / totalMinutes, Date = yesterdayFrom };

//                await _averageTweetsPerMinuteByDateRepo.CreateAverageTweetsPerMinute(existedObj);
//                await _averageTweetsPerMinuteByDateRepo.SaveChanges();

//            }
//            else
//            {
//                existedObj.AverageTweets = totalTweets / totalMinutes;
//                await _averageTweetsPerMinuteByDateRepo.UpdateAverageTweetsPerMinute(existedObj);
//                await _averageTweetsPerMinuteByDateRepo.SaveChanges();

//            }

//        }

//        public async Task CalculateAverageTweetsPerMinuteToday()
//        {
//            var today = DateTime.Today;
//            var rawDataByToday = await _tweetRawDataRepo.GetTweetRowDataFromDate(today, false);

//            if (!rawDataByToday.Any())
//                return;

//            var totalTweets = rawDataByToday.Count();

//            var dateRange = rawDataByToday.Select(m => m.DateTimeTweet);

//            var maxDate = dateRange.Max();
//            var minDate = dateRange.Min();
//            var ts = maxDate - minDate;
//            var totalMinutes = ts.TotalMinutes == 0 ? 1 : ts.TotalMinutes;

//            var averageTweetsPerMinute = totalTweets / totalMinutes;

//            var existedObj = await _averageTweetsPerMinuteByDateRepo.GetAverageTweetsPerMinuteByDate(today);

//            if (existedObj == null)
//            {

//                existedObj = new AverageTweetsPerMinute { AverageTweets = totalTweets, Date = today };

//                await _averageTweetsPerMinuteByDateRepo.CreateAverageTweetsPerMinute(existedObj);
//                await _averageTweetsPerMinuteByDateRepo.SaveChanges();

//            }
//            else
//            {
//                existedObj.AverageTweets = averageTweetsPerMinute;
//                await _averageTweetsPerMinuteByDateRepo.UpdateAverageTweetsPerMinute(existedObj);
//                await _averageTweetsPerMinuteByDateRepo.SaveChanges();

//            }

//        }

//        public async Task<AverageTweetsPerMinute> GetAverageTweetsPerMinuteByDateByDate(DateTime dateTime)
//        {
//            return await _averageTweetsPerMinuteByDateRepo.GetAverageTweetsPerMinuteByDate(dateTime);
//        }

//    }
//}
