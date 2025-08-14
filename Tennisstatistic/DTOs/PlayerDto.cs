namespace TennisStatistics.Api.DTOs
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public required string Fullname { get; set; } 
        public required string CountryCode { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
    }
}
