using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetsAnalysis.Data.Extensions;
using TweetsAnalysis.Data.Models;
using TweetsAnalysis.Data.Service;

namespace TweetsAnalysis.Test
{
    [TestClass]
    public class TotalTweetsReceivedServiceTest : TestBase
    {
        private TotalTweetsReceivedService _service;

        public TotalTweetsReceivedServiceTest() : base()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            _service = new TotalTweetsReceivedService(Context);
        }

        [TestMethod]
        public async Task CalculateTotalTweetsReceived_Test_TotalTweetsReceived_Sould_Be_Created_and_Calculated()
        {
            Context.DeleteAll<TotalTweetsReceived>();

            var totalTweetsReceived = Context.TotalTweetsReceiveds.FirstOrDefault()?.TotalTweets;

            await Context.TweetRawDatas.AddAsync(new Data.Models.TweetRawData { DateTimeTweet = DateTime.Now, Content = "CalculateTotalTweetsReceived_Test_1" });
            await Context.SaveChangesAsync();

            await _service.CalculateTotalTweetsReceived();

            Assert.AreNotEqual(null, Context.TotalTweetsReceiveds.FirstOrDefault());
            Assert.AreEqual(1, Context.TotalTweetsReceiveds.Count());
            Assert.AreEqual((totalTweetsReceived ?? 0) + 1, Context.TotalTweetsReceiveds.FirstOrDefault().TotalTweets);
        }

        [TestMethod]
        public async Task CalculateTotalTweetsReceived_Test_TotalTweetsReceived_Sould_Be_Increased()
        {

            // Clear total tweets recieved before test
            Context.DeleteAll<TotalTweetsReceived>();

            // Add a new total tweets recieved object
            await Context.TotalTweetsReceiveds.AddAsync(new TotalTweetsReceived { TotalTweets = 1, LatestDateCalculate = DateTime.Now });
            await Context.SaveChangesAsync();

            var totalTweetsReceived = Context.TotalTweetsReceiveds.FirstOrDefault().TotalTweets;

            await Context.TweetRawDatas.AddAsync(new Data.Models.TweetRawData { DateTimeTweet = DateTime.Now, Content = "CalculateTotalTweetsReceived_Test_2" });
            await Context.SaveChangesAsync();

            await _service.CalculateTotalTweetsReceived();

            Assert.AreNotEqual(null, Context.TotalTweetsReceiveds.FirstOrDefault());
            Assert.AreEqual(1, Context.TotalTweetsReceiveds.Count());
            Assert.AreEqual(totalTweetsReceived + 1, Context.TotalTweetsReceiveds.FirstOrDefault().TotalTweets);
        }
    }
}
