using TennisStatistics.Api.DTOs;

namespace TennisStatistics.Api.Services
{
    public interface IPlayerService
    {
        IEnumerable<PlayerDto> GetAllPlayers();
        PlayerDto? GetPlayerById(int id);
        PlayerDto AddPlayer(CreatePlayerDto dto);
    }
}
