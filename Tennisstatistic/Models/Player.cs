namespace TennisStatistics.Api.Models
{
    public class Player
    {
        public int Id { get; set; }
        public required string Firstname { get; set; } 
        public required string Lastname { get; set; } 
        public string? Shortname { get; set; } 
        public string? Sex { get; set; } 
        public required Country Country { get; set; } 
        public  string? Picture { get; set; } 
        public required PlayerData Data { get; set; }

    }
}
