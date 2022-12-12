using Microsoft.AspNetCore.Mvc;
using System.Net;
using TweetsAnalysis.Data.Models;
using TweetsAnalysis.Data.Service;
using System.Net.Http;

namespace TweetsAnalysis.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TweetsController : ControllerBase
    {
        private readonly IAverageTweetsPerMinuteService _averageTweetsPerMinuteService;
        private readonly ITotalTweetsReceivedService _totalTweetsReceivedService;
        private readonly ILogger<TweetsController> _logger;
        public TweetsController(ITotalTweetsReceivedService totalTweetsReceivedService, 
            IAverageTweetsPerMinuteService averageTweetsPerMinuteService,
            ILogger<TweetsController> logger)
        {
            _totalTweetsReceivedService = totalTweetsReceivedService;
            _averageTweetsPerMinuteService = averageTweetsPerMinuteService;
            _logger = logger;
        }      

        [HttpGet]
        [Route(nameof(GetTotalNumberOfTweetsReceived))]
        public async Task<int> GetTotalNumberOfTweetsReceived()
        {
            return await _totalTweetsReceivedService.GetTotalTweetsReceived();
        }

        [HttpGet]
        [Route(nameof(GetAverageTweetsPerMinute))]
        public async Task<int> GetAverageTweetsPerMinute()
        {
            var result =  await _averageTweetsPerMinuteService.GetAverageTweetsPerMinuteAllOfTime();

            return result;
        }

        [HttpGet]
        [Route(nameof(GetAverageTweetsPerMinuteByDate))]
        public async Task<int> GetAverageTweetsPerMinuteByDate(DateTime dateTime)
        {
            var result = await _averageTweetsPerMinuteService.GetAverageTweetsPerMinuteByDate(dateTime);

            return result;
        }
    }
}