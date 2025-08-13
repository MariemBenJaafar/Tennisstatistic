using TennisStatistics.Api.DTOs;
using TennisStatistics.Api.Repositories;

namespace TennisStatistics.Api.Services
{
    public class StatsService : IStatsService
    {
        private readonly IPlayerRepository _repo;

        public StatsService(IPlayerRepository repo)
        {
            _repo = repo;
        }

        public string GetCountryWithBestWinRatio()
        {
            var players = _repo.GetAllPlayers();

            return players
                .GroupBy(p => p.Country.Code)
                .Select(g => new
                {
                    Country = g.Key,
                    Ratio = g.Average(p =>
                        p.Data.Last.Count > 0 ? p.Data.Last.Count(x => x == 1) / (double)p.Data.Last.Count : 0)
                })
                .OrderByDescending(x => x.Ratio)
                .First().Country;
        }

        public double GetAverageBMI()
        {
            var players = _repo.GetAllPlayers();
            return players.Average(p => p.Data.Weight / Math.Pow(p.Data.Height / 100.0, 2));
        }

        public double GetMedianHeight()
        {
            var heights = _repo.GetAllPlayers().Select(p => p.Data.Height).OrderBy(h => h).ToList();
            int count = heights.Count;
            if (count % 2 == 0)
                return (heights[count / 2 - 1] + heights[count / 2]) / 2.0;
            else
                return heights[count / 2];
        }
    }
}

