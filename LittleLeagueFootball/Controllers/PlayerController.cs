using Microsoft.AspNetCore.Mvc;
using LittleLeagueFootball.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using LittleLeagueFootball.Services;

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
    }
}
