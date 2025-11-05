using System.Collections.Generic;

namespace LittleLeagueFootball.Models
{
    public class TeamPlayersViewModel
    {
        // Step 1: Properties for Team and Players
        public Team Team { get; set; } = null!;
        public IEnumerable<Player> Players { get; set; } = new List<Player>();
    }
}


