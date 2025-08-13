using System.Text.Json;
using TennisStatistics.Api.DTOs;
using TennisStatistics.Api.Models;
using TennisStatistics.Api.Repositories;

namespace TennisStatistics.Api.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _repo;

        public PlayerService(IPlayerRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<PlayerDto> GetAllPlayers()
        {
            return _repo.GetAllPlayers()
             .OrderBy(p => p.Data.Rank)
             .Select(p => new PlayerDto
             {
                 Id = p.Id,
                 Fullname = $"{p.Firstname} {p.Lastname}",
                 CountryCode = p.Country.Code,
                 Rank = p.Data.Rank,
                 Points = p.Data.Points
             })
             .ToList();
        }

        public PlayerDto? GetPlayerById(int id)
        {
            var p = _repo.GetPlayerById(id);
            if (p == null) return null;

            return new PlayerDto
            {
                Id = p.Id,
                Fullname = $"{p.Firstname} {p.Lastname}",
                CountryCode = p.Country.Code,
                Rank = p.Data.Rank,
                Points = p.Data.Points
            };
        }

        public PlayerDto AddPlayer(CreatePlayerDto dto)
        {

            var newId = _repo.GetAllPlayers().Max(p => p.Id) + 1;
            var player = new Player
            {
                Id = newId,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Shortname = $"{dto.Firstname[0]}.{dto.Lastname[..3].ToUpper()}",
                Sex = "M", 
                Country = new Country { Code = dto.CountryCode ?? string.Empty, Picture = "" },
                Picture = "",
                Data = new PlayerData
                {
                    Rank = dto.Rank,
                    Points = dto.Points,
                    Weight = dto.Weight,
                    Height = dto.Height,
                    Age = dto.Age,
                    Last = dto.Last 
                }
            };

            _repo.AddPlayer(player);
            _repo.SaveChanges();

            return new PlayerDto
            {
                Id = player.Id,
                Fullname = $"{player.Firstname} {player.Lastname}",
                CountryCode = player.Country.Code,
                Rank = player.Data.Rank,
                Points = player.Data.Points
            };
        }
    }
}
    
       