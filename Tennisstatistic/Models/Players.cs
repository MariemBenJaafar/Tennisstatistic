namespace TennisStatistics.Api.Models
{
    public class Players
    {
        public List<Player> PlayersList { get; set; }

        public Players()
        {
            PlayersList = new List<Player>();
        }

    }
}
