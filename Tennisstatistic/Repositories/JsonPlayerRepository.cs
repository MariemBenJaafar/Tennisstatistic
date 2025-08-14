using System.Text.Json;
using TennisStatistics.Api.Models;

namespace TennisStatistics.Api.Repositories
{
    public class JsonPlayerRepository : IPlayerRepository
    {
        private readonly string _filePath;
        private readonly List<Player> _players;
        private readonly ILogger<JsonPlayerRepository> _logger; 

        public JsonPlayerRepository(IWebHostEnvironment env, ILogger<JsonPlayerRepository> logger, string? filePath = null) 
        {
            _logger = logger;

            _filePath = filePath?? Path.Combine(env.ContentRootPath, "headtohead.json");
            try
            {
                if (!File.Exists(_filePath))
                {
                    _players = new List<Player>();
                    return;
                }

                var json = File.ReadAllText(_filePath);
                var playerList = JsonSerializer.Deserialize<Players>(json);

                if (playerList == null || playerList.PlayersList == null)
                {
                    _players = new List<Player>();
                }
                else
                {
                    _players = playerList.PlayersList;
                }
            }
            catch (JsonException ex)
            {
                // Gestion des erreurs de parsing JSON
                Console.Error.WriteLine($"Erreur lors de la désérialisation du fichier JSON : {ex.Message}");
                _players = new List<Player>();
            }
            catch (IOException ex)
            {
                // Gestion des erreurs de lecture du fichier
                Console.Error.WriteLine($"Erreur lors de la lecture du fichier JSON : {ex.Message}");
                _players = new List<Player>();
            }
            catch (Exception ex)
            {
                // Gestion de toute autre erreur inattendue
                Console.Error.WriteLine($"Erreur inattendue : {ex.Message}");
                _players = new List<Player>();
            }
        }

        public List<Player> GetAllPlayers() => _players;

        public Player? GetPlayerById(int id) => _players.FirstOrDefault(p => p.Id == id);

        public void AddPlayer(Player player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            _players.Add(player);
        }

        public void SaveChanges()
        {
            try
            {
                var playerslist = new Players { PlayersList = _players };
                var json = JsonSerializer.Serialize(playerslist, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "Erreur lors de l’écriture du fichier JSON.");
                throw;
            }
        }
    }

}
       