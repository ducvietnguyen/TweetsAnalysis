using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetsAnalysis.Data.Models;
using TweetsAnalysis.Data.Service;
namespace TweetsAnalysis.Test
{
    [TestClass]
    public class TweetRawDataServiceTest : TestBase
    {
        private  ITweetRawDataService _tweetRawDataService;
        public TweetRawDataServiceTest() : base()
        {
           
        }

        [TestInitialize]
        public void Setup()
        {
            _tweetRawDataService = new TweetRawDataService(Context);
        }


        [TestMethod]
        public async Task CreateMultiTweetRowDatas_Test_Should_Insert_To_Database()
        {
            var dateTweet = DateTime.Now;

            var newRawData = new TweetRawData { Text = "CreateMultiTweetRowDatas Some content", CreatedTime = dateTweet, Id = Guid.NewGuid().ToString() };

            await _tweetRawDataService.CreateMultiTweetRowDatas(new List<TweetRawData> { newRawData });
            await _tweetRawDataService.SaveChanges();

            var inserted = (await _tweetRawDataService.GetAllTweetRowData()).Where(m => m.CreatedTime == dateTweet);

            Assert.AreEqual(1, inserted.Count());
            Assert.AreEqual("CreateMultiTweetRowDatas Some content", inserted.First().Text);
        }

        [TestMethod]
        public async Task GetAllTweetRowData_Test_Should_Return_All_Data()
        {
            var dateTweet = DateTime.Now;

            var newRawData = new TweetRawData { Text = "GetAllTweetRowData Some content", CreatedTime = dateTweet , Id = Guid.NewGuid().ToString() };

            await _tweetRawDataService.CreateMultiTweetRowDatas(new List<TweetRawData> { newRawData });

            var databaseCount = Context.TweetRawDatas.Count();

            var getAllRresult = await _tweetRawDataService.GetAllTweetRowData();

            Assert.AreEqual(databaseCount, getAllRresult.Count());
        }
    }
}