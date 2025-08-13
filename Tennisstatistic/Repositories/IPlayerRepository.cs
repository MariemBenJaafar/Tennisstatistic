using TennisStatistics.Api.Models;

namespace TennisStatistics.Api.Repositories
{
    public interface IPlayerRepository
    {
        List<Player> GetAllPlayers();
        Player? GetPlayerById(int id);
        void AddPlayer(Player player);
        void SaveChanges();

    }
}
