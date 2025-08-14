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

            var newId = _repo.GetAllPlayers().Any()? _repo.GetAllPlayers().Max(p => p.Id) + 1 : 1;
            var player = new Player
            {
                Id = newId,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Shortname = $"{dto.Firstname[0]}.{dto.Lastname.ToUpper()}",
                Sex = dto.Sex, 
                Country = new Country { Code = dto.CountryCode ?? string.Empty, Picture = dto.Picture },
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

        public PlayerDto? UpdatePlayer(int id, UpdatePlayerDto dto)
        {
            var player = _repo.GetPlayerById(id);
            if (player == null) return null;

            // Mise à jour des propriétés
            player.Firstname = dto.Firstname;
            player.Lastname = dto.Lastname;
            player.Country.Code = dto.CountryCode;
            player.Data.Rank = dto.Rank;
            player.Data.Points = dto.Points;
            player.Data.Height = dto.Height;
            player.Data.Weight = dto.Weight;
            player.Data.Age = dto.Age;

            // Sauvegarde dans le repository
            _repo.SaveChanges();

            // Conversion vers DTO pour ne pas exposer l'entité directement
            return new PlayerDto
            {
                Id = player.Id,
                Fullname = $"{player.Firstname} {player.Lastname}",
                CountryCode = player.Country.Code,
                Rank = player.Data.Rank,
                Points = player.Data.Points
            };
        }

        public bool DeletePlayer(int id)
        {
            var player = _repo.GetPlayerById(id);
            if (player == null) return false;

            _repo.DeletePlayer(id);
            return true;
        }

    }

}
    
       