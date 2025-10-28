using LittleLeagueFootball.Data;
using LittleLeagueFootball.Models;
using Microsoft.EntityFrameworkCore;

namespace LittleLeagueFootball.Services
{
    // Register to ILeagueService dependency injection
    public class LeagueService : ILeagueService
    {
        // Step 1: Inject LeagueContext _context
        //  Make pricate readonly
        private readonly LeagueContext _context;

        // Dependency Injection Constructor
        public LeagueService(LeagueContext context)
        {
            _context = context;
        }

        // Step 2: GET Teams
        public async Task<IReadOnlyList<Team>> GetTeamsAsync()
        {
            // var await for teams in db
            //  Order by alpha name, Use t for variable
            var teams = await _context.Teams
                    .AsNoTracking()
                    .OrderBy(t => t.Name)
                    .ToListAsync();

            // return teams
            return teams;
        }

        // Step 3: Get Players
        public async Task<IReadOnlyList<Player>> GetPlayersAsync()
        {
            // var await for players in db
            //  Order by TeamId > Player LastName > FirstName
            //  Use p for variable
            var players = await _context.Players
                .Include(p => p.Team)
                .AsNoTracking()
                .OrderBy(p => p.TeamId)
                .ThenBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync();

            // return Players
            return players;
        }

        // Step 4: Get 1 Team's Roster
        //  Order by Player LastName > FirstName
        //  Use p for variable, and teamId
        public async Task<IReadOnlyList<Player>> GetRosterAsync(int teamId)
        {
            // var await for roster in db
            var roster = await _context.Players
                .Where(p => p.TeamId == teamId)
                .Include(p => p.Team)
                .AsNoTracking()
                .OrderBy (p => p.LastName)
                .ThenBy (p => p.FirstName)
                .ToListAsync();

            // return roster
            return roster;
        }
    }
}
