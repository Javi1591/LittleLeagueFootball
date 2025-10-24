using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LittleLeagueFootball.Models
{
    // Step 1: Define the Team class
    //  Use int Id for primary key, One to many relationship with Player class
    //  Use string Name for team name
    //  MUST be [Required], name MAX LENGTH([StringLength (15)])
    public class Team
    {
        // Primary key
        //  integer Id, One to many
        public int Id { get; set; }

        // Team name
        //  Required, StringLength 15
        [Required]
        [StringLength(15)]
        public string Name { get; set; } = string.Empty;    // Initialize to avoid nulls

        // Step 2: Navigation property for related players
        //  One to many relationship with Player class
        //  Initialize to avoid null reference issues
        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
