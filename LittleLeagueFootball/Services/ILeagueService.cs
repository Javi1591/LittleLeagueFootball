using LittleLeagueFootball.Models;

namespace LittleLeagueFootball.Services
{
    public interface ILeagueService
    {
        // Make ALL ReadOnly
        // Step 1: Get Teams
        Task<IReadOnlyList<Team>> GetTeamsAsync();  // Get all Teams
        Task<Team?> GetTeamAsync(int teamId);       // Get Team by teamId
        Task AddTeamAsync(Team team);               // Add Team
        Task UpdateTeamAsync(Team teamId);          // Update Team by teamId
        Task DeleteTeamAsync(int teamId);           // Delete Team by teamId

        // Step 2: Get Players
        Task<IReadOnlyList<Player>> GetPlayersAsync();  // Get all Players
        Task<Player?> GetPlayerAsync(int playerId);     // Get Player by playerId
        Task AddPlayerAsync(Player player);             // Add Player
        Task UpdatePlayerAsync(Player player);          // Update Player
        Task DeletePlayerAsync(int id);                 // Delete Player by Id

        // Step 3: Get 1 Team's Roster
        //  Roster by teamId
        Task<IReadOnlyList<Player>> GetRosterAsync(int teamId);

        // Step 4: Helper Functions
        Task<Team?> GetTeamsAsync(int id);                          // GetTeamAsync by id
        Task<IReadOnlyList<Player>> GetPlayersByTeamAsync(int id);  // GetPlayerAsync by id

        // Step 5: Stored Procedure to get Players by Team
        Task<IReadOnlyList<PlayerByTeamResult>> GetPlayersByTeamStoredProcAsync(int teamId);
    }
}
