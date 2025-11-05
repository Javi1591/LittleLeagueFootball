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

        // CRUD Operations for Team here (Create, Edit, Delete)
        // Step 2a: GET 1 Team by teamId
        public async Task<Team?> GetTeamAsync(int teamId)
        {
            return await _context.Teams
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == teamId);
        }

        // Step 2b: Add Team
        public async Task AddTeamAsync(Team team)
        {
            // Keep Team for variable
            _context.Teams.Add(team);

            //  Save changes
            await _context.SaveChangesAsync();
        }

        // Step 2c: Update Team by team
        public async Task UpdateTeamAsync(Team team)
        {
            // Keep Team for variable
            var teamid = await _context.Teams.FindAsync(team);

            // If team not null, update
            if (team != null)
            {
                _context.Teams.Update(team);

                // Save changes
                await _context.SaveChangesAsync();
            }
        }

        // Step 2d: Delete Team by teamId
        public async Task DeleteTeamAsync(int teamId)
        {
            // Keep Team for variable
            var team = await _context.Teams.FindAsync(teamId);

            // If team not null, remove
            if (team != null)
            {
                _context.Teams.Remove(team);

                // Save changes
                await _context.SaveChangesAsync();
            }
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

        // CRUD Operations for Player here (Create, Edit, Delete)

        // Step 3a: GET 1 Player by playerId
        public async Task<Player?> GetPlayerAsync(int playerId)
        {
            // return await player from db
            return await _context.Players
                .Include(p => p.Team)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == playerId);
        }

        // Step 3b: Add Player
        public async Task AddPlayerAsync(Player player)
        {

            // Keep Player for variable
            _context.Players.Add(player);

            // Save changes
            await _context.SaveChangesAsync();
        }

        // Step 3c: Update Player
        //  Use player parameter
        public async Task UpdatePlayerAsync(Player player)
        {

            // Keep Player for variable
            var existingPlayer = await _context.Players.FindAsync(player.Id);

            // If existingPlayer not null, update
            if (existingPlayer != null)
            {
                _context.Players.Update(player);

                // Save changes
                await _context.SaveChangesAsync();
            }
        }

        // Step 3d: Delete Player by Id
        //  Use id parameter
        public async Task DeletePlayerAsync(int id)
        {

            // Keep Player for variable
            var player = await _context.Players.FindAsync(id);

            // If player not null, remove
            if (player != null)
            {
                _context.Players.Remove(player);

                // Save changes
                await _context.SaveChangesAsync();
            }
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
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync();

            // return roster
            return roster;
        }

        // Step 5: Helper Functions

        // GetTeamAsync by id
        public async Task<Team?> GetTeamsAsync(int id)
        {
            return await _context.Teams
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        // GetPlayersByTeamAsync by id
        public async Task<IReadOnlyList<Player>> GetPlayersByTeamAsync(int id)
        {
            // var await for players in db
            //  Order by LastName > FirstName
            //  Use p for variable
            var players = await _context.Players
                .Where(p => p.TeamId == id)
                .Include(p => p.Team)
                .AsNoTracking()
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync();

            // return players
            return players;
        }
    }
}
