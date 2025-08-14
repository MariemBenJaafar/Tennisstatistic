using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TennisStatistics.Api.Services;

namespace TennisStatistics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IStatsService _statsService;

        public StatsController(IStatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet("best-country")]
        public IActionResult BestCountry()
        {
            return Ok(new { country = _statsService.GetCountryWithBestWinRatio() });
        }

        [HttpGet("average-bmi")]
        public IActionResult AverageBmi()
        {
            return Ok(new { averageBMI = _statsService.GetAverageIMC() });
        }

        [HttpGet("median-height")]
        public IActionResult MedianHeight()
        {
            return Ok(new { medianHeight = _statsService.GetMedianHeight() });
        }
    }
}