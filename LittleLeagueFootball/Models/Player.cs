using System.ComponentModel.DataAnnotations;

namespace LittleLeagueFootball.Models
{
    // Step 1: Define the Player class
    //  Use int Id for primary key
    //  Use string FirstName and LastName for player name
    //  Must be [Required], MAX LENGTH([StringLength (20)]) for both
    public class Player
    {
        // Primary key
        //  integer Id, One to One
        //  ONE player to ONE team
        public int Id { get; set; }

        // Player FirstName
        //  Required, StringLength 20
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; } = string.Empty;    // Initialize to avoid nulls

        // Player LastName
        //  Required, StringLength 20
        [Required]
        [StringLength(20)]
        public string LastName { get; set; } = string.Empty;    // Initialize to avoid nulls

        // Step 2: Foreign key for related Team
        //  One to One relationship with Team class
        //  Use int TeamId as foreign key
        public int TeamId { get; set; }

        // Step 3: Navigation property for related Team
        //  One to One relationship with Team class
        //  Can be null if player is not assigned to a team ("Free Agent")
        public Team? Team { get; set; }
    }
}
