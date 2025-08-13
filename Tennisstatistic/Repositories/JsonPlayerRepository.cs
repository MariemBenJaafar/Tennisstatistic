using System.Text.Json;
using TennisStatistics.Api.Models;

namespace TennisStatistics.Api.Repositories
{
    public class JsonPlayerRepository : IPlayerRepository
    {
        private readonly string _filePath;
        private readonly List<Player> _players;

        public JsonPlayerRepository(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "headtohead.json");
            var json = File.ReadAllText(_filePath);
            var playerList = JsonSerializer.Deserialize<Players>(json);
            _players = playerList?.PlayersList ?? new();
        }

        public List<Player> GetAllPlayers() => _players;

        public Player? GetPlayerById(int id) => _players.FirstOrDefault(p => p.Id == id);

        public void AddPlayer(Player player) => _players.Add(player);

        public void SaveChanges()
        {
            var playerslist = new Players { PlayersList = _players };
            var json = JsonSerializer.Serialize(playerslist, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }

}
       