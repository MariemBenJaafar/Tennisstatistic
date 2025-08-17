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
        /// <summary>
        /// Gets the country with the highest win ratio.
        /// </summary>
        /// <returns>The country name with the best win ratio.</returns>
        /// <response code="200">Returns the country with the best win ratio</response>
        [HttpGet("best-country")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult BestCountry()
        {
            var result = _statsService.GetCountryWithBestWinRatio();
            if (string.IsNullOrWhiteSpace(result))
                return NotFound(new { message = "No data available to compute win ratio." });

            return Ok(new { bestCountry = result });
        }

        /// <summary>
        /// Gets the average IMC of all players.
        /// </summary>
        /// <returns>The average IMC as a double.</returns>
        /// <response code="200">Returns the average IMC of players</response>
        [HttpGet("average-imc")]
        public IActionResult AverageIMC()
        {
            return Ok(new { averageIMC = _statsService.GetAverageIMC() });
        }

        /// <summary>
        /// Gets the median height of all players.
        /// </summary>
        /// <returns>The median height in centimeters.</returns>
        /// <response code="200">Returns the median height of players</response>
        [HttpGet("median-height")]
        public IActionResult MedianHeight()
        {
            return Ok(new { medianHeight = _statsService.GetMedianHeight() });
        }


    }
}