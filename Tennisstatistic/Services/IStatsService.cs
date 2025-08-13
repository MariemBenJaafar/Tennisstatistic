using TennisStatistics.Api.DTOs;

namespace TennisStatistics.Api.Services
{
    public interface IStatsService
    {
        string GetCountryWithBestWinRatio();
        double GetAverageBMI();
        double GetMedianHeight();
    }
}
