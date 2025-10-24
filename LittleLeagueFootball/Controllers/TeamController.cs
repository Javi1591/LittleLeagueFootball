using Microsoft.AspNetCore.Mvc;
using LittleLeagueFootball.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LittleLeagueFootball.Controllers
{
    
    public class TeamController : Controller
    {
        // Step 1: Database context for LeagueContext
        //  Make private readonly field
        private readonly LittleLeagueFootball.Data.LeagueContext _context;

        // Step 2: Constructor for Dependency Injection
        //  Set up at later week
        public TeamController(LittleLeagueFootball.Data.LeagueContext context)
        {
            _context = context;
        }

        // /GET: /Team
        //  Return Seeded Teams
        public async Task<IActionResult> Index()
        {
            // Step 3: Retrieve Teams from database
            //  Use teams for variable
            var teams = await _context.Teams.ToListAsync();

            // Step 4: Return Teams to view
            return View(teams);
        }
    }
}
