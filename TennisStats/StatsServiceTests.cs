using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisStatistics.Api.Models;
using TennisStatistics.Api.Repositories;
using TennisStatistics.Api.Services;

namespace TennisStatsTests
{
    public class StatsServiceTests
    {
        private static List<Player> SamplePlayers() => new()
        {
            new Player { Id = 52, Firstname = "Novak", Lastname = "Djokovic", Country = new Country { Code = "SRB" }, Data = new PlayerData { Rank = 2, Points = 2542, Height = 188, Weight = 80000, Age = 31, Last = new(){1,1,1,1,1} } },
            new Player { Id = 17, Firstname = "Rafael", Lastname = "Nadal",  Country = new Country { Code = "ESP" }, Data = new PlayerData { Rank = 1, Points = 1982, Height = 185, Weight = 85000, Age = 33, Last = new(){1,0,0,0,1} } },
            new Player { Id = 102, Firstname = "Serena", Lastname = "Williams", Country = new Country { Code = "USA" }, Data = new PlayerData { Rank = 10, Points = 3521, Height = 175, Weight = 72000, Age = 37, Last = new(){0,1,1,1,0} } },
            new Player { Id = 95, Firstname = "Venus", Lastname = "Williams", Country = new Country { Code = "USA" }, Data = new PlayerData { Rank = 52, Points = 1105, Height = 185, Weight = 74000, Age = 38, Last = new(){0,1,0,0,1} } },
        };

        private static IStatsService BuildService()
        {
            var mock = new Mock<IPlayerRepository>();
            mock.Setup(r => r.GetAllPlayers()).Returns(SamplePlayers());
            return new StatsService(mock.Object);
        }

        [Fact]
        public void CountryWithBestWinRatio_Should_Return_Country_With_Best_Win_Ratio()
        {
            var stats = BuildService();

            var country = stats.GetCountryWithBestWinRatio();

            country.Should().Be("SRB"); // Djokovic 5/5 => ratio 1.0
        }

        [Fact]
        public void AverageIMC_Should_Positive()
        {
            var stats = BuildService();

            var bmi = stats.GetAverageIMC();


            // Average BMI = (80000/1000)/(188/100)^2 + (85000/1000)/(185/100)^2 + (72000/1000)/(175/100)^2 + (74000/1000)/(185/100)^2 (IMC sould be= 23.15)
            bmi.Should().BeGreaterThan(23).And.BeLessThan(24); 
        }

        [Fact]
        public void MedianHeight_Should_return_Median_Height()
        {
            var stats = BuildService();

            var median = stats.GetMedianHeight();

            // sorted Heights : 175, 185, 185, 188 -> Median = (185+185)/2 = 185
            median.Should().Be(185);
        }
    }
}
    
