using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TennisStatistics.Api.Controllers;
using TennisStatistics.Api.Services;

namespace TennisStatsTests
{
    public class StatsControllerTests
    {
        private readonly Mock<IStatsService> _mockService;
        private readonly StatsController _controller;

        public StatsControllerTests()
        {
            _mockService = new Mock<IStatsService>();
            _controller = new StatsController(_mockService.Object);
        }

        [Fact]
        public void BestCountry_ReturnsExpectedCountry()
        {
            _mockService.Setup(s => s.GetCountryWithBestWinRatio()).Returns("ESP");

            var result = _controller.BestCountry();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var json = JsonSerializer.Serialize(okResult.Value);
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            Assert.Equal("ESP", dict["country"]);
        }

        [Fact]
        public void AverageIMC_ReturnsExpectedValue()
        {
            _mockService.Setup(s => s.GetAverageIMC()).Returns(22.5);

            var result = _controller.AverageIMC();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var json = JsonSerializer.Serialize(okResult.Value);
            var dict = JsonSerializer.Deserialize<Dictionary<string, double>>(json);
            Assert.Equal(22.5, dict["averageIMC"]);
        }

        [Fact]
        public void MedianHeight_ReturnsExpectedValue()
        {
            _mockService.Setup(s => s.GetMedianHeight()).Returns(185);

            var result = _controller.MedianHeight();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var json = JsonSerializer.Serialize(okResult.Value);
            var dict = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
            Assert.Equal(185, dict["medianHeight"]);
        }
    }
}
