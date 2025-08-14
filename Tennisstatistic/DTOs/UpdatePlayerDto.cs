namespace TennisStatistics.Api.DTOs
{
    public class UpdatePlayerDto
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string CountryCode { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Age
        {
            get; set;

        }
    }
}
