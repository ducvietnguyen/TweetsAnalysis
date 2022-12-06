//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TweetsAnalysis.Services;

//namespace TweetsAnalysis.Test
//{
//    public class AverageTweetsPerMinuteByDateServiceTest
//    {
//        private AverageTweetsPerMinuteByDateService _service;

//        public AverageTweetsPerMinuteByDateServiceTest() : base()
//        {

//        }

//        private ITotalTweetsReceivedRepo GetInMemoryTotalTweetsReceivedRepo()
//        {

//            var repository = new TotalTweetsReceivedRepo(Context);

//            return repository;
//        }

//        private ITweetRawDataRepo GetInMemoryTweetRawDataRepo()
//        {

//            var repository = new TweetRawDataRepo(Context);

//            return repository;
//        }

//        [TestInitialize]
//        public void Setup()
//        {
//            _service = new TotalTweetsReceivedService(GetInMemoryTotalTweetsReceivedRepo(), GetInMemoryTweetRawDataRepo());
//        }
//    }
//}
