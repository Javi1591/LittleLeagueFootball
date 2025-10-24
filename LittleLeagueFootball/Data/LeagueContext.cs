using LittleLeagueFootball.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace LittleLeagueFootball.Data
{
    // Step 1: Define the LeagueContext class with DbContext base class
    //  Set up DbSet<Player> Players and DbSet<Team> Teams (Later Weeks)
    //  Set up Dependency Injection constructor with DbContextOptions<LeagueContext> (Later Weeks)
    public class LeagueContext : DbContext
    {
        // Step 2: Constructor for Dependency Injection
        public LeagueContext(DbContextOptions<LeagueContext> options) : base(options)
        {
        }

        // Step 3: DbSet for Players and Teams
        //  Miniminum needed for later weeks
        public DbSet<Player> Players => Set<Player>(); // Table for Players
        public DbSet<Team> Teams => Set<Team>();  // Table for Teams

        // Step 4: Seed Data
        //  Use OnModelCreating (built-in function) to seed data for Players and Teams
        //  Reference NFL.com roster for players
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base method
            //  Ensures built-in configurations are applied
            base.OnModelCreating(modelBuilder);

            // Seed Teams
            //  Four teams, Use NFC South names
            modelBuilder.Entity<Team>().HasData(
                new Team { Id = 1, Name = "Buccaneers" },
                new Team { Id = 2, Name = "Falcons" },
                new Team { Id = 3, Name = "Panthers" },
                new Team { Id = 4, Name = "Saints" }
            );

            // Seed Players
            //  Ten players per team, total 40 players
            //  Get FirstName, LastName, TeamId
            //  Use NFL rosters
            modelBuilder.Entity<Player>().HasData(

                // Buccaneers Players
                new Player { Id = 1, FirstName = "Baker", LastName = "Mayfield", TeamId = 1 },
                new Player { Id = 2, FirstName = "Chris", LastName = "Godwin", TeamId = 1 },
                new Player { Id = 3, FirstName = "Mike", LastName = "Evans", TeamId = 1 },
                new Player { Id = 4, FirstName = "Emeka", LastName = "Egbuka", TeamId = 1 },
                new Player { Id = 5, FirstName = "Lavonte", LastName = "David", TeamId = 1 },
                new Player { Id = 6, FirstName = "Rachaad", LastName = "White", TeamId = 1 },
                new Player { Id = 7, FirstName = "Bucky", LastName = "Irving", TeamId = 1 },
                new Player { Id = 8, FirstName = "Yaya", LastName = "Diaby", TeamId = 1 },
                new Player { Id = 9, FirstName = "Sean", LastName = "Tucker", TeamId = 1 },
                new Player { Id = 10, FirstName = "Cade", LastName = "Otton", TeamId = 1 },

                // Falcons Players
                new Player { Id = 11, FirstName = "Tyler", LastName = "Allgeier", TeamId = 2 },
                new Player { Id = 12, FirstName = "Kyle", LastName = "Pitts", TeamId = 2 },
                new Player { Id = 13, FirstName = "Darnell", LastName = "Mooney", TeamId = 2 },
                new Player { Id = 14, FirstName = "Bijan", LastName = "Robinson", TeamId = 2 },
                new Player { Id = 15, FirstName = "Micheal", LastName = "Penix Jr.", TeamId = 2 },
                new Player { Id = 16, FirstName = "AJ", LastName = "Terrell", TeamId = 2 },
                new Player { Id = 17, FirstName = "Josh", LastName = "Woods", TeamId = 2 },
                new Player { Id = 18, FirstName = "Chris", LastName = "Blaire", TeamId = 2 },
                new Player { Id = 19, FirstName = "Khalid", LastName = "Kareem", TeamId = 2 },
                new Player { Id = 20, FirstName = "Keith", LastName = "Taylor", TeamId = 2 },

                // Panthers Players
                new Player { Id = 21, FirstName = "Bryce", LastName = "Young", TeamId = 3 },
                new Player { Id = 22, FirstName = "Trevor", LastName = "Etienne", TeamId = 3 },
                new Player { Id = 23, FirstName = "Rico", LastName = "Dowdle", TeamId = 3 },
                new Player { Id = 24, FirstName = "Ryan", LastName = "Fitzgerald", TeamId = 3 },
                new Player { Id = 25, FirstName = "Chuba", LastName = "Hubbard", TeamId = 3 },
                new Player { Id = 26, FirstName = "Xavier", LastName = "Legette", TeamId = 3 },
                new Player { Id = 27, FirstName = "Tetairoa", LastName = "McMillan", TeamId = 3 },
                new Player { Id = 28, FirstName = "Andy", LastName = "Dalton", TeamId = 3 },
                new Player { Id = 29, FirstName = "Jalen", LastName = "Coker", TeamId = 3 },
                new Player { Id = 30, FirstName = "Austin", LastName = "Corbet", TeamId = 3 },

                // Saints Players
                new Player { Id = 31, FirstName = "Michael", LastName = "Davis", TeamId = 4 },
                new Player { Id = 32, FirstName = "Alvin", LastName = "Kamara", TeamId = 4 },
                new Player { Id = 33, FirstName = "Chris", LastName = "Olave", TeamId = 4 },
                new Player { Id = 34, FirstName = "Terrell", LastName = "Burgess", TeamId = 4 },
                new Player { Id = 35, FirstName = "Kendre", LastName = "Miller", TeamId = 4 },
                new Player { Id = 36, FirstName = "Bub", LastName = "Means", TeamId = 4 },
                new Player { Id = 37, FirstName = "Erik", LastName = "McCoy", TeamId = 4 },
                new Player { Id = 38, FirstName = "Will", LastName = "Sherman", TeamId = 4 },
                new Player { Id = 39, FirstName = "Dante", LastName = "Pettis", TeamId = 4 },
                new Player { Id = 40, FirstName = "Ronnie", LastName = "Bell", TeamId = 4 }
            );
        }
    }
}
