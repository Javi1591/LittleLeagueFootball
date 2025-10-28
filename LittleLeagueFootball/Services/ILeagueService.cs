using LittleLeagueFootball.Models;

namespace LittleLeagueFootball.Services
{
    public interface ILeagueService
    {
        // Make ALL ReadOnly
        // Step 1: Get Teams
        Task<IReadOnlyList<Team>> GetTeamsAsync();

        // Step 2: Get Players
        Task<IReadOnlyList<Player>> GetPlayersAsync();

        // Step 3: Get 1 Team's Roster
        //  Roster by teamId
        Task<IReadOnlyList<Player>> GetRosterAsync(int teamId);

    }
}
