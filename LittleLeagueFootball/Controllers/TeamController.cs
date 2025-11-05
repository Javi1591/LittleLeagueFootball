using Microsoft.AspNetCore.Mvc;
using LittleLeagueFootball.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LittleLeagueFootball.Services;
using LittleLeagueFootball.Models;

namespace LittleLeagueFootball.Controllers
{
    
    public class TeamController : Controller
    {
        // Step 1: Database context for LeagueContext
        //  Make private readonly field
        private readonly ILeagueService _leagueService;

        // Step 2: Constructor for Dependency Injection
        public TeamController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
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
            // var await team
            var team = await _leagueService.GetTeamsAsync(id);

            // Use if statement to check for no team found
            if (team == null)
            {
                return NotFound();
            }

            // var await players
            var players = await _leagueService.GetPlayersByTeamAsync(id);

            // Create ViewModel to pass both team and players to View
            var viewModel = new TeamPlayersViewModel
            {
                Team = team,
                Players = players
            };

            // return viewModel to View
            return View(viewModel);
        }

        // Step 8 GET: /Team/RosterPrint/{id}
        //  Returns printable Roster by Team Id
        public async Task<IActionResult> RosterPrint(int id)
        {

            // var await team
            var team = await _leagueService.GetTeamsAsync(id);

            // Use if statement to check for no team found
            if (team == null)
            {
                return NotFound();
            }

            // var await players
            var players = await _leagueService.GetPlayersByTeamAsync(id);

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
