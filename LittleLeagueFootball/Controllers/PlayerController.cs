using Microsoft.AspNetCore.Mvc;
using LittleLeagueFootball.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LittleLeagueFootball.Controllers
{
    public class PlayerController : Controller
    {
        // Step 1:  Database context for LeagueContext
        //  Make private readonly field
        private readonly LeagueContext _context;

        // Step 2: Constructor for Dependency Injection
        //  Set up at later week
        public PlayerController(LeagueContext context)
        {
            _context = context;
        }

        // /Get: /Player
        //  Return Seeded Players
        public async Task<IActionResult> Index()
        {
            // Step 3: Retrieve Players from database
            //  Use players for variable
            var players = await _context.Players
                .Include(p => p.Team) // Include related Team data
                .ToListAsync();

            // Step 4: Return Players to view
            return View(players);
        }
    }
}
