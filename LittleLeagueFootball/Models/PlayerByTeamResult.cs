using System;

namespace LittleLeagueFootball.Models
{
    public class PlayerByTeamResult
    {
        // Properties for PlayerByTeamResult
        //  Primary Key
        public int PlayerId { get; set; }

        // First Name
        public string FirstName { get; set; } = string.Empty;

        // Last Name
        public string LastName { get; set; } = string.Empty;

        // Team Name from team table
        public string TeamName { get; set; } = string.Empty;

        // Team Id
        public int TeamId { get; set; }
    }
}
