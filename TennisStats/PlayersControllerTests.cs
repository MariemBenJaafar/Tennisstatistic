using Microsoft.AspNetCore.Mvc;
using Moq;
using TennisStatistics.Api.Controllers;
using TennisStatistics.Api.DTOs;
using TennisStatistics.Api.Services;

namespace TennisStatsTests
{
    public class PlayersControllerTests
    {

        private readonly PlayersController _controller;
        private readonly Mock<IPlayerService> _mockService;

        public PlayersControllerTests()
        {
            _mockService = new Mock<IPlayerService>();

            // Exemple de données simulées avec Country et Data obligatoires
            _mockService.Setup(s => s.GetAllPlayers()).Returns(new List<PlayerDto>
        {
            new PlayerDto
            {
                Id = 1,
                Fullname = "Novak Djokovic",
                CountryCode = "SRB",
                Rank = 1,
                Points = 1000

            }
        });

            _mockService.Setup(s => s.GetPlayerById(1)).Returns(new PlayerDto
            {
                Id = 1,
                Fullname = "Novak Djokovic",
                CountryCode = "SRB",
                Rank = 1,
                Points = 1000
            });

            _controller = new PlayersController(_mockService.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkWithPlayers()
        {
            var result = _controller.GetAllPlayers();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var players = Assert.IsType<List<PlayerDto>>(okResult.Value);

            Assert.Single(players);
            Assert.Equal("Novak Djokovic", players[0].Fullname);
            Assert.Equal("SRB", players[0].CountryCode);
            Assert.Equal(1000,players[0].Points);
        }

        [Fact]
        public void GetById_ReturnsPlayer_WhenFound()
        {
            var result = _controller.GetPlayerById(1);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var player = Assert.IsType<PlayerDto>(okResult.Value);

            Assert.Equal(1, player.Id);
            Assert.Equal("Novak Djokovic", player.Fullname);
            Assert.Equal(1000,player.Points);
            Assert.NotNull(player.CountryCode);
        }

        [Fact]
        public void GetById_ReturnsNotFound_WhenMissing()
        {
            _mockService.Setup(s => s.GetPlayerById(999)).Returns((PlayerDto?)null);

            var result = _controller.GetPlayerById(999);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_ReturnsCreatedPlayer()
        {
            var newPlayerDto = new CreatePlayerDto { Firstname = "Rafael", Lastname = "Nadal", CountryCode = "ESP"  };
            var createdPlayer = new PlayerDto
            {
                Id = 2,
                Fullname = "Rafael Nadal",
                CountryCode = "ESP",
                Rank = 2,
                Points = 900
            };

            _mockService.Setup(s => s.AddPlayer(newPlayerDto)).Returns(createdPlayer);

            var result = _controller.CreatePlayer(newPlayerDto);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var player = Assert.IsType<PlayerDto>(createdResult.Value);

            Assert.Equal("Rafael Nadal", player.Fullname);
            Assert.Equal(2, player.Id);
            Assert.Equal(900,player.Points);
            Assert.Equal("ESP", player.CountryCode);
        }
    }
}