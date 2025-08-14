using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TennisStatistics.Api.Models;
using TennisStatistics.Api.Repositories;

namespace TennisStatsTests
{
    public class JsonPlayerRepositoryTests
    {
        private readonly string _testFilePath;

        public JsonPlayerRepositoryTests()
        {
            _testFilePath = Path.Combine(Path.GetTempPath(), $"players_{Guid.NewGuid()}.json");
        }

        private JsonPlayerRepository CreateRepositoryWithSampleData()
        {
            var players = new Players
            {
                PlayersList = new List<Player>
            {
                new Player { Id = 1, Firstname = "Novak", Lastname = "Djokovic", Country = new Country { Code = "SRB" }, Data = new PlayerData { Rank = 2, Points = 2542, Height = 188, Weight = 80000, Age = 31 } }
            }
            };
            File.WriteAllText(_testFilePath, JsonSerializer.Serialize(players, new JsonSerializerOptions { WriteIndented = true }));

            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv.Setup(e => e.ContentRootPath).Returns(Path.GetDirectoryName(_testFilePath)!);

            var mockLogger = new Mock<ILogger<JsonPlayerRepository>>();

            return new JsonPlayerRepository(mockEnv.Object, mockLogger.Object, _testFilePath);
        }

        [Fact]
        public void GetAllPlayers()
        {
            var repo = CreateRepositoryWithSampleData();

            var players = repo.GetAllPlayers();

            players.Should().NotBeNull();
            players.Should().HaveCount(1);
            players[0].Firstname.Should().Be("Novak");
        }

        [Fact]
        public void GetPlayerById()
        {
            var repo = CreateRepositoryWithSampleData();

            var player = repo.GetPlayerById(1);

            player.Should().NotBeNull();
            player!.Lastname.Should().Be("Djokovic");
        }

        [Fact]
        public void AddPlayer_ThenSaveChanges()
        {
            var repo = CreateRepositoryWithSampleData();

            var newPlayer = new Player { Id = 2, Firstname = "Rafael", Lastname = "Nadal", Country = new Country { Code = "ESP" }, Data = new PlayerData { Rank = 1, Points = 1982, Height = 185, Weight = 85000, Age = 33 } };
            repo.AddPlayer(newPlayer);
            repo.SaveChanges();

            var json = File.ReadAllText(_testFilePath);
            var playersFromFile = JsonSerializer.Deserialize<Players>(json);
            playersFromFile!.PlayersList.Should().ContainSingle(p => p.Id == 2 && p.Firstname == "Rafael");
        }


        [Fact]
        public void AddPlayer_NullPlayer_ShouldThrowArgumentNullException()
        {
            var repo = CreateRepositoryWithSampleData();

            Action act = () => repo.AddPlayer(null!);

            act.Should().Throw<ArgumentNullException>();
        }
    }
}