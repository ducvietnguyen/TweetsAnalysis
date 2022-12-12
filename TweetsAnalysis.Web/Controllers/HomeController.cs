using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TweetsAnalysis.Data.Service;
using TweetsAnalysis.Web.Models;

namespace TweetsAnalysis.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IAverageTweetsPerMinuteService _averageTweetsPerMinuteService;
        private readonly ITotalTweetsReceivedService _totalTweetsReceivedService;

        public HomeController(ITotalTweetsReceivedService totalTweetsReceivedService,
            IAverageTweetsPerMinuteService averageTweetsPerMinuteService,
            ILogger<HomeController> logger)
        {
            _totalTweetsReceivedService = totalTweetsReceivedService;
            _averageTweetsPerMinuteService = averageTweetsPerMinuteService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<JsonResult> GetAverageTweetsPerMinute()
        {
            var averageTweetsPerMinute = await _averageTweetsPerMinuteService.GetAverageTweetsPerMinuteAllOfTime();
            return Json(new { averageTweetsPerMinute = averageTweetsPerMinute });
        }

        public async Task<JsonResult> GetTotalTweetsReceived()
        {
            var totalTweetsReceived = await _totalTweetsReceivedService.GetTotalTweetsReceived();
            return Json(new { totalTweetsReceived = totalTweetsReceived});
        }
    }
}