using Microsoft.AspNetCore.Mvc;
using LittleLeagueFootball.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LittleLeagueFootball.Services;
using LittleLeagueFootball.Models;
using Microsoft.Extensions.Logging;

namespace LittleLeagueFootball.Controllers
{
    
    public class TeamController : Controller
    {
        // Step 1: Database context for LeagueContext
        //  Make private readonly field
        private readonly ILeagueService _leagueService;

        // Step 1a: Logger for TeamController
        //  Make private readonly field
        private readonly ILogger<TeamController> _logger;

        // Step 2: Constructor for Dependency Injection
        public TeamController(ILeagueService leagueService, ILogger<TeamController> logger)
        {
            _leagueService = leagueService;
            _logger = logger;               // Initialize logger
        }

        // Step 3 GET: /Team
        //  Return Teams in db
        public async Task<IActionResult> Index()
        {
            //  Use teams for variable
            var teams = await _leagueService.GetTeamsAsync();

            // return teams to View
            return View(teams);
        }

        // CRUD Operations for Team here (Create, Edit, Delete)

        // Step 4 GET: /Team/Create
        public IActionResult Create()
        {
            return View();
        }

        // Step 4a POST: /Team/Create
        // Bind only Name property
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Team team)
        {

            // Use if statement to check ModelState
            //  await _leagueService to add team
            if (ModelState.IsValid)
            {
                await _leagueService.AddTeamAsync(team);
                return RedirectToAction(nameof(Index));
            }

            // return team to View
            return View(team);
        }

        // Step 5 GET: /Team/Edit/{id}
        //  Use id for parameter
        public async Task<IActionResult> Edit(int id)
        {
            // var await team
            var team = await _leagueService.GetTeamsAsync(id);

            // Use if statement to check for no team found
            if (team == null)
            {
                return NotFound();
            }

            // return team to View
            return View(team);
        }

        // Step 5a POST: /Team/Edit/{id}
        // Bind only Id and Name properties
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Team team)
        {
            // Use if statement to check id and ModelState
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _leagueService.UpdateTeamAsync(team);
                return RedirectToAction(nameof(Index));
            }

            // return team to View
            return View(team);
        }

        // Step 6 GET: /Team/Delete/{id}
        //  Use id for parameter
        public async Task<IActionResult> Delete(int id)
        {
            // var await team
            var team = await _leagueService.GetTeamsAsync(id);

            // Use if statement to check for no team found
            if (team == null)
            {
                return NotFound();
            }

            // return team to View
            return View(team);
        }

        // Step 6a POST: /Team/Delete/{id}
        //  Use ActionName "Delete"
        //  Use id for parameter
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // await _leagueService to delete team
            await _leagueService.DeleteTeamAsync(id);

            // redirect to Index
            return RedirectToAction(nameof(Index));
        }

        // Step 7 GET: /Team/Roster/{id}
        //  Returns Roster by Team Id
        //  Keep team, await _leagueService for variable
        //  Use team id for parameter, players for variable
        public async Task<IActionResult> Roster(int id)
        {
            // GET: Request ID for logging
            //  Use var requestId
            //  Get from HttpContext TraceIdentifier
            var requestId = HttpContext.TraceIdentifier;

            // var await team
            var team = await _leagueService.GetTeamsAsync(id);

            // Use if statement to check for no team found
            if (team == null)
            {
                // Log warning for team not found
                //  Use RequestId and TeamId
                //  Log action as "Roster"
                _logger.LogWarning(
                    "Roster - Failed retrieval. Team with ID {TeamId} not found. " +
                    "Request ID: {RequestId} Action: {Action}",
                    id,
                    requestId,
                    "Roster");

                // return NotFound
                return NotFound();
            }

            // var await players
            var players = await _leagueService.GetPlayersByTeamAsync(id);

            // Log for successful roster viewing
            //  Use RequestId and TeamName, TeamId, PlayerCount
            //  Log action as "Roster"
            _logger.LogInformation(
                "Roster - Successfully retrieved roster for Team '{TeamName}'" +
                " (ID: {TeamId}) with {PlayerCount} players. " +
                "Request ID: {RequestId}, Action: {Action}",
                team.Name,
                team.Id,
                players.Count(),
                requestId,
                "Roster");

            // Create ViewModel to pass both team and players to View
            var viewModel = new TeamPlayersViewModel
            {
                Team = team,
                Players = players
            };

            // return viewModel to View
            return View(viewModel);
        }

        // Step 7a GET: /Team/RosterSp/{id}

        //  Returns Roster by Team Id using GetPlayersByTeam stored procedure.
        public async Task<IActionResult> RosterSp(int id)
        {
            // GET: Request ID for logging
            //  Use var requestId
            //  Get from HttpContext TraceIdentifier
            var requestId = HttpContext.TraceIdentifier;

            // var await team (used for logging and display)
            var team = await _leagueService.GetTeamsAsync(id);

            // Use if statement to check for no team found
            if (team == null)
            {
                // Log warning for team not found (stored procedure path)
                //  Use RequestId and TeamId
                //  Log action as "RosterSp"
                _logger.LogWarning(
                    "RosterSp - Failed retrieval (Stored Procedure). Team with ID {TeamId} not found. " +
                    "Request ID: {RequestId}, Action: {Action}",
                    id,
                    requestId,
                    "RosterSp");

                // return NotFound
                return NotFound();
            }

            // var await players using stored procedure
            var players = await _leagueService.GetPlayersByTeamStoredProcAsync(id);

            // Log for successful roster viewing using stored procedure
            //  Use RequestId and TeamName, TeamId, PlayerCount
            //  Log action as "RosterSp"
            _logger.LogInformation(
                "RosterSp - Successfully retrieved roster via stored procedure for Team '{TeamId}'" +
                " (ID: {TeamId}) with {PlayerCount} players. " +
                "Request ID: {RequestId}, Action: {Action}",
                team.Name,
                team.Id,
                players.Count,
                requestId,
                "RosterSp");

            // Pass result list directly to View
            //  View will use PlayerByTeamResult to render players
            return View(players);
        }

        // Step 8 GET: /Team/RosterPrint/{id}
        //  Returns printable Roster by Team Id
        public async Task<IActionResult> RosterPrint(int id)
        {
            // GET: Request ID for logging
            //  Use var requestId
            //  Get from HttpContext TraceIdentifier
            var requestId = HttpContext.TraceIdentifier;

            // var await team
            var team = await _leagueService.GetTeamsAsync(id);

            // Use if statement to check for no team found
            if (team == null)
            {
                // Log warning for team not found
                //  Use requestId and team id
                //  Log action as "RosterPrint"
                _logger.LogWarning(
                    "RosterPrint: Failed retrieval. Team with ID {TeamId} not found. " +
                    "Request ID: {RequestId}, Action: {Action}",
                    id,
                    requestId,
                    "RosterPrint");

                // return NotFound
                return NotFound();
            }

            // var await players
            var players = await _leagueService.GetPlayersByTeamAsync(id);

            // Log for successful printable roster viewing
            //  Use requestId and team name, team id, player count
            //  Log action as "RosterPrint"
            _logger.LogInformation(
                "RosterPrint: Successfully retrieved printable roster for Team '{TeamName}' " +
                "(ID: {TeamId}) with {PlayerCount} players. " +
                "Request ID: {RequestId}, Action: {Action}",
                team.Name,
                team.Id,
                players.Count(),
                requestId,
                "RosterPrint");

            // Create ViewModel to pass both team and players to View
            var viewModel = new TeamPlayersViewModel
            {
                Team = team,
                Players = players
            };

            // return viewModel to View
            return View(viewModel);
        }
        
        // Step 9: Helper method to check if Team exists
        private async Task<bool> TeamExists(int id)
        {
            var team = await _leagueService.GetTeamsAsync(id);
            return team != null;
        }
    }
}
