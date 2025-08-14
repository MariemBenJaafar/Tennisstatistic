using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var value = Assert.IsType<Dictionary<string, string>>(okResult.Value);
            Assert.Equal("ESP", value["country"]);
        }

        [Fact]
        public void AverageBmi_ReturnsExpectedValue()
        {
            _mockService.Setup(s => s.GetAverageIMC()).Returns(22.5);

            var result = _controller.AverageBmi();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<Dictionary<string, double>>(okResult.Value);
            Assert.Equal(22.5, value["averageBMI"]);
        }

        [Fact]
        public void MedianHeight_ReturnsExpectedValue()
        {
            _mockService.Setup(s => s.GetMedianHeight()).Returns(185);

            var result = _controller.MedianHeight();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<Dictionary<string, int>>(okResult.Value);
            Assert.Equal(185, value["medianHeight"]);
        }
    }
}
