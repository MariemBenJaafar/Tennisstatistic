using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TennisStatistics.Api.Models;

namespace TennisStatsTests
{
    public class PlayersControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PlayersControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsPlayers()
        {
            var response = await _client.GetAsync("/api/players");
            response.EnsureSuccessStatusCode();

            var players = await response.Content.ReadFromJsonAsync<List<Player>>();
            Assert.NotNull(players);
            Assert.True(players.Count > 0);
        }

        [Fact]
        public async Task GetById_ReturnsPlayerOrNotFound()
        {
            var response = await _client.GetAsync("/api/players/52");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
            else
            {
                var player = await response.Content.ReadFromJsonAsync<Player>();
                Assert.NotNull(player);
                Assert.Equal(52, player.Id);
            }
        }
    }
}
