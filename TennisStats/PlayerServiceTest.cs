using FluentAssertions;
using Moq;
using System.Data;
using TennisStatistics.Api.DTOs;
using TennisStatistics.Api.Models;
using TennisStatistics.Api.Repositories;
using TennisStatistics.Api.Services;

namespace TennisStats
{
    
    public class PlayerServiceTest 
    {
        private static List<Player> MockPlayers() => new()
        {
            new Player { Id = 52, Firstname = "Novak", Lastname = "Djokovic", Country = new Country { Code = "SRB" }, Data = new PlayerData { Rank = 2, Points = 2542, Height = 188, Weight = 80000, Age = 31, Last = new(){1,1,1,1,1} } },
            new Player { Id = 17, Firstname = "Rafael", Lastname = "Nadal",  Country = new Country { Code = "ESP" }, Data = new PlayerData { Rank = 1, Points = 1982, Height = 185, Weight = 85000, Age = 33, Last = new(){1,0,0,0,1} } },
            new Player { Id = 102, Firstname = "Serena", Lastname = "Williams", Country = new Country { Code = "USA" }, Data = new PlayerData { Rank = 10, Points = 3521, Height = 175, Weight = 72000, Age = 37, Last = new(){0,1,1,1,0} } },
        };

        [Fact]
        public void GetAll_Should_Return_Sorted_By_Rank()
        {
            var mock = new Mock<IPlayerRepository>();
            mock.Setup(r => r.GetAllPlayers()).Returns(MockPlayers());

            var svc = new PlayerService(mock.Object);

            var result = svc.GetAllPlayers();

            result.Select(r => r.Rank).Should().ContainInOrder(1, 2, 10);
            result.First().Fullname.Should().Be("Rafael Nadal"); // rank 1
        }

        [Fact]
        public void GetById_Should_Return_Mapped_Dto()
        {
            var mock = new Mock<IPlayerRepository>();
            mock.Setup(r => r.GetPlayerById(52)).Returns(MockPlayers().First(p => p.Id == 52));

            var svc = new PlayerService(mock.Object);

            var dto = svc.GetPlayerById(52);

            dto.Should().NotBeNull();
            dto!.Id.Should().Be(52);
            dto.Fullname.Should().Be("Novak Djokovic");
            dto.CountryCode.Should().Be("SRB");
            dto.Rank.Should().Be(2);
        }

        [Fact]
        public void Add_Should_Assign_New_Id_And_Save()
        {
            var list = MockPlayers(); 

            var mock = new Mock<IPlayerRepository>();
            var _service = new PlayerService(mock.Object);
            mock.Setup(r => r.GetAllPlayers()).Returns(() => list); 
            mock.Setup(r => r.AddPlayer(It.IsAny<Player>()))
                .Callback<Player>(p => list.Add(p));
            mock.Setup(r => r.SaveChanges());

            var svc = new PlayerService(mock.Object);

            var dto = new CreatePlayerDto
            {
                Firstname = "Iga",
                Lastname = "Swiatek",
                CountryCode = "POL",
                Rank = 3,
                Points = 2000,
                Weight = 65000,
                Height = 176,
                Age = 22,
                Last = new() { 1, 1, 0, 1, 1 }
            };

            var created = svc.AddPlayer(dto);

            created.Id.Should().BeGreaterThan(0);
            created.Fullname.Should().Be("Iga Swiatek");
            list.Should().Contain(p => p.Id == created.Id);
            mock.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void UpdatePlayer_ShouldReturnUpdatedDto_WhenPlayerExists()
        {
            var mock = new Mock<IPlayerRepository>();
            var _service = new PlayerService(mock.Object);

            var player = new Player
            {
                Id = 1,
                Firstname = "Novak",
                Lastname = "Djokovic",
                Country = new Country { Code = "SRB" },
                Data = new PlayerData { Rank = 1, Points = 1000, Height = 188, Weight = 80000, Age = 36 }
            };

            var dto = new UpdatePlayerDto
            {
                Firstname = "Novak",
                Lastname = "Updated",
                CountryCode = "SRB",
                Rank = 2,
                Points = 1500,
                Height = 189,
                Weight = 81000,
                Age = 37
            };

            mock.Setup(r => r.GetPlayerById(1)).Returns(player);
            mock.Setup(r => r.SaveChanges());

            // Act
            var result = _service.UpdatePlayer(1, dto);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Novak Updated", result.Fullname);
            Xunit.Assert.Equal("SRB", result.CountryCode);
            Xunit.Assert.Equal(2, result.Rank);
            Xunit.Assert.Equal(1500, result.Points);
        }

    }
}