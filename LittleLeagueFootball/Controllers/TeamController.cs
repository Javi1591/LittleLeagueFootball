using Microsoft.AspNetCore.Mvc;
using LittleLeagueFootball.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LittleLeagueFootball.Services;

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

        // Step 4: Get Roster by Team
        //  GET: /Team/Roster/{id}
        //  Use roster for variable
        public async Task<IActionResult> Roster(int id)
        {
            // var await roster
            var roster = await _leagueService.GetRosterAsync(id);

            // return roster to View
            return View(roster);
        }
    }
}
