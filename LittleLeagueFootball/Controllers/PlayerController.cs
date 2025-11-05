using Microsoft.AspNetCore.Mvc;
using LittleLeagueFootball.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using LittleLeagueFootball.Services;
using LittleLeagueFootball.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LittleLeagueFootball.Controllers
{
    public class PlayerController : Controller
    {
        // Step 1:  Database context for LeagueContext
        //  Make private readonly field
        private readonly ILeagueService _leagueService;

        // Step 2: Constructor for Dependency Injection
        public PlayerController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        // Step 3 Get: /Player
        //  Return Players in db
        public async Task<IActionResult> Index()
        {
            //  Use players for variable
            var players = await _leagueService.GetPlayersAsync();

            // return Players to View
            return View(players);
        }

        // CRUD Operations for Player here (Create, Edit, Delete)

        // Helper Function to populate Teams dropdown
        //  Used in Create and Edit views
        //  selectedTeamId is optional parameter
        private async Task PopulateTeamsAsync(int? selectedTeamId = null)
        {
            // await _leagueService to get teams
            var teams = await _leagueService.GetTeamsAsync();

            // Set ViewBag.TeamId to SelectList of teams
            ViewBag.TeamId = new SelectList(teams, "Id", "Name", selectedTeamId);
        }

        // Step 4 GET: /Player/Create
        public async Task<IActionResult> Create()
        {
            // Populate teams for dropdown
            await PopulateTeamsAsync();

            // return View
            return View();
        }

        // Step 4a POST: /Player/Create
        // Bind FirstName, LastName, TeamId property
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName, LastName, TeamId")] Player player)
        {
            // Use if statement to check ModelState
            if (!ModelState.IsValid)
            {
                // await PopulateTeamsAsync with player.TeamId
                await PopulateTeamsAsync(player.TeamId);

                // return player to View
                return View(player);
            }

            // await _leagueService to add player
            await _leagueService.AddPlayerAsync(player);

            // return to Index
            return RedirectToAction(nameof(Index));
        }

        // Step 5 GET: /Player/Edit/{id}
        // returns Player Edit by id
        public async Task<IActionResult> Edit(int id)
        {
            // Keep player for variable / await _leagueService
            var player = await _leagueService.GetPlayerAsync(id);

            // If player is null, return NotFound
            if (player == null)
            {
                return NotFound();
            }

            // await PopulateTeamsAsync with player.TeamId
            await PopulateTeamsAsync(player.TeamId);

            // return player to View
            return View(player);
        }

        // Step 5a POST: /Player/Edit/{id}
        // Bind Id and Name property
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName, LastName, TeamId")] Player player)
        {
            // If id does not match player.Id, return NotFound
            if (id != player.Id)
            {
                return NotFound();
            }

            // Use if statement to check ModelState
            if (!ModelState.IsValid)
            {
                // await PopulateTeamsAsync with player.TeamId
                await PopulateTeamsAsync(player.TeamId);

                // return player to View
                return View(player);
            }

            // Use if statement if player does not exist
            if (await _leagueService.GetPlayerAsync(id) == null)
            {
                return NotFound();
            }

            // await _leagueService to update player
            await _leagueService.UpdatePlayerAsync(player);

            // return to Index
            return RedirectToAction(nameof(Index));
        }

        // Step 6 GET: /Player/Delete/{id}
        // returns Player Delete by id
        public async Task<IActionResult> Delete(int id)
        {
            // Keep player for variable / await _leagueService
            var player = await _leagueService.GetPlayerAsync(id);

            // If player is null, return NotFound
            if (player == null)
            {
                return NotFound();
            }

            // return player to View
            return View(player);
        }

        // Step 6a POST: /Player/Delete/{id}
        //  ActionName "Delete"
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            // await _leagueService to delete player by id
            await _leagueService.DeletePlayerAsync(id);

            // return to Index
            return RedirectToAction(nameof(Index));
        }
    }
}
