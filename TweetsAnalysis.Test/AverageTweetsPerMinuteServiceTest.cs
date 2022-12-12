using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetsAnalysis.Data.Extensions;
using TweetsAnalysis.Data.Models;
using TweetsAnalysis.Data.Service;

namespace TweetsAnalysis.Test
{
    [TestClass]
    public class AverageTweetsPerMinuteServiceTest : TestBase
    {
        private IAverageTweetsPerMinuteService _service;

        public AverageTweetsPerMinuteServiceTest() : base()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            _service = new AverageTweetsPerMinuteService(Context);
        }

        [TestMethod]
        public async Task CalculateAverageTweetsPerMinute_Test_AverageTweets_Should_BeCalculated()
        {
            // Clear AverageTweetsPerMinute before testing
            ClearAverageTweetsPerMinute();

            var newRawData = BuildTweetRawDataForToday();

            await Context.TweetRawDatas.AddRangeAsync(newRawData);
            await Context.SaveChangesAsync();

            await _service.CalculateAverageTweetsPerMinute(DateTime.Today, DateTime.Now);

            var averageTweetsPerMinute = await _service.GetAverageTweetsPerMinuteAllOfTime();

            Assert.AreEqual(2, averageTweetsPerMinute);
        }

        [TestMethod]
        public async Task GetAverageTweetsPerMinuteAllOfTime_Test_Should_Return_Data()
        {
            // Clear AverageTweetsPerMinute before testing
            ClearAverageTweetsPerMinute();


            await Context.AverageTweetsPerMinutes.AddRangeAsync(new List<AverageTweetsPerMinute> {
                new AverageTweetsPerMinute
                {
                    AverageTweets = 4,
                    Date = DateTime.Today.AddDays(-1)
                },
                new AverageTweetsPerMinute
                {
                     AverageTweets = 12,
                    Date = DateTime.Today
                }
            });
            await Context.SaveChangesAsync();

            var averageTweetsPerMinute = await _service.GetAverageTweetsPerMinuteAllOfTime();

            Assert.AreEqual(8, averageTweetsPerMinute);
        }

        [TestMethod]
        public async Task GetAverageTweetsPerMinuteByDate_Test_Should_Return_Data()
        {
            // Clear AverageTweetsPerMinute before testing
            ClearAverageTweetsPerMinute();


            await Context.AverageTweetsPerMinutes.AddRangeAsync(new List<AverageTweetsPerMinute> {
                new AverageTweetsPerMinute
                {
                    AverageTweets = 4,
                    Date = DateTime.Today.AddDays(-1)
                },
                new AverageTweetsPerMinute
                {
                     AverageTweets = 12,
                    Date = DateTime.Today
                }
            });
            await Context.SaveChangesAsync();

            var averageTweetsPerMinute = await _service.GetAverageTweetsPerMinuteByDate(DateTime.Today);

            Assert.AreNotEqual(null, averageTweetsPerMinute);
            Assert.AreEqual(12, averageTweetsPerMinute);
        }

        [TestMethod]
        public async Task GetAverageTweetsPerMinuteByDate_Test_Should_Return_Zero()
        {
            // Clear AverageTweetsPerMinute before testing
            ClearAverageTweetsPerMinute();

            var averageTweetsPerMinute = await _service.GetAverageTweetsPerMinuteByDate(DateTime.Today);

            Assert.AreEqual(null, averageTweetsPerMinute);
        }

        [TestMethod]
        public async Task GetAverageTweetsPerMinuteAllOfTime_Test_Should_Return_Zero()
        {
            // Clear AverageTweetsPerMinute before testing
            ClearAverageTweetsPerMinute();

            var averageTweetsPerMinute = await _service.GetAverageTweetsPerMinuteAllOfTime();

            Assert.AreEqual(0, averageTweetsPerMinute);
        }

        private void ClearAverageTweetsPerMinute()
        {
            Context.DeleteAll<AverageTweetsPerMinute>();
            Context.SaveChanges();
        }

        private List<TweetRawData> BuildTweetRawDataForToday()
        {
            var dateTweet = DateTime.Now;

            var newRawData = new List<TweetRawData> {
                new TweetRawData {
                    Text = "CalculateAverageTweetsPerMinute_Test 1",
                    CreatedTime = dateTweet,
                    Id = Guid.NewGuid().ToString()
    },
                new TweetRawData {
                    Text = "CalculateAverageTweetsPerMinute_Test 2",
                    CreatedTime = dateTweet,
                    Id = Guid.NewGuid().ToString()
},
                new TweetRawData
                {
                    Text = "CalculateAverageTweetsPerMinute_Test 3",
                    CreatedTime = dateTweet,
                    Id = Guid.NewGuid().ToString()
                },
                new TweetRawData
                {
                    Text = "CalculateAverageTweetsPerMinute_Test 4",
                    CreatedTime = dateTweet.AddMinutes(-1),
                    Id = Guid.NewGuid().ToString()
                },
                new TweetRawData
                {
                    Text = "CalculateAverageTweetsPerMinute_Test 5",
                    CreatedTime = dateTweet.AddMinutes(-2),
                    Id = Guid.NewGuid().ToString()
                },
                new TweetRawData
                {
                    Text = "CalculateAverageTweetsPerMinute_Test 6",
                    CreatedTime = dateTweet.AddMinutes(-3),
                    Id = Guid.NewGuid().ToString()
                }
            };

            return newRawData;
        }
    }
}
