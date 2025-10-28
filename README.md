# Little League Football
## ASP.NET Core MVC

## Project Summary
Little League Football League is a simple ASP.NET Core MVC web app that helps a local youth football league manage teams and players.
The app supports creating, editing, and viewing team rosters with seeded data for 4 teams and 10 players. No stats or positions are recorded.
It’s designed purpose is to show ASP.NET concepts such as modeling, dependency injection, CRUD, diagnostics, logging, and stored procedures,
followed by Azure deployment.

## Planning Table
| Week | Concept | Feature | Goal | Acceptance Criteria | Evidence in README.md | Test Plan |
|------|---------|---------|------|---------------------|------------------------|-----------|
| 10 | Modeling | Create `Team`/`Player` entities; configure one-to-many (Team → Players); add `LeagueContext`; seed 4 teams with 10 players | App can store players and assign them to teams | - [ ] Teams table created <br>- [ ] Players table created <br>- [ ] FK `TeamId` works <br>- [ ] Seed inserts 4/10 | Implemented code; migration output; README write-up; screenshots of seeded lists or diagram | Run migration; open Teams/Players pages; confirm counts and relationship |
| 11 | Separation of Concerns / DI | Add `ILeagueService`/`LeagueService` to encapsulate roster logic; controllers use constructor injection | Move logic out of controllers into a testable service | - [ ] Service registered Scoped <br>- [ ] Controllers call service only <br>- [ ] Methods return expected data | Code diff showing thin controllers; README write-up on DI/lifetimes; screenshots of service interface | Unit tests using EF InMemory; hit endpoints and verify data flow |
| 12 | CRUD | Add Create/Edit/Delete for Teams and Players; add Roster view per team; use PRG on POST | Enable full CRUD and printable rosters | - [ ] Forms display <br>- [ ] Validation messages show <br>- [ ] Changes persist <br>- [ ] Roster lists players | CRUD code; README write-up; screenshots of Create/Edit/Delete and roster view | Add then edit a player; confirm DB update; validate error cases; verify roster updates |
| 13 | Diagnostics | Add `/healthz` using Health Checks for EF DB; add minimal status page (env, version) | App reports DB health status | - [ ] `/healthz` returns Healthy when DB up <br>- [ ] Returns Unhealthy when broken | Health check registration; README write-up; screenshots of `curl -i https://localhost:PORT/healthz` | Break connection string to observe Unhealthy; restore and re-verify |
| 14 | Logging | Log structured events for Create/Edit/Delete; add request logging + correlation ID | Produce actionable logs for key ops | - [ ] Info logs include entity id/name <br>- [ ] Errors logged with exception <br>- [ ] Correlation ID appears | Log config; README write-up; screenshots of logs during CRUD | Perform create→edit→delete; verify logs and correlation ID; unit test logger via mock |
| 15 | Stored Procedures | Add SP: GetPlayersByTeam @TeamID | Demonstrate interop with a stored procedure | - [ ] `.sql` file added <br>- [ ] EF call works <br>- [ ] UI path triggers SP <br>- [ ] Fallback LINQ documented | SP script + service call; README write-up; screenshots of UI result | Compare SP output vs LINQ; run in DB and app; verify match |
| 16 | Deployment | Deploy to Azure (Linux, .NET 8) with staging/production slots; configure connection string and `/healthz` | Make the app publicly reachable | - [ ] App Service created <br>- [ ] App builds/runs <br>- [ ] `/healthz` reachable <br>- [ ] One functional path works | Deployed URL; README write-up; screenshots of portal, `/healthz`, and working page | Visit public URL; confirm health endpoint; load Teams & a roster page |

## Week 10 Modeling
This week, I implemented the foundation of my Little League Football application by creating the data model and connecting it to a database
using Entity Framework Core. The two main entities I added were `Team` and `Player`, with the relationships between entities. A team can have
many players, so I used an ICollection<Player> navigation property in the `Team` model to establish a one-to-many relationship. Whereas, a
player can only have one relationship to a single team. Next, I created the `LeagueContext` class, which inherits from DbContext and defines
two DbSet properties, one for `Teams` and `Players`. This context manages communication between the models and the database. I then configured
the database connection string in `appsettings.json` and registered the context in Program.cs for dependency injection. To verify that the data
model worked, I added initial seed data for four teams (Buccaneers, Falcons, Panthers, Saints) and ten players per team. I ran the commands
`dotnet ef migrations add InitialCreate` and `dotnet ef database update` to generate the schema and seed the data automatically. Then,
I created simple pages for /Teams and /Players to confirm that data loaded correctly. Finally, after running the migration, I tested the application
by navigating to the /Teams and /Players pages. Each page loaded all the seeded data correctly, which confirmed that the database connection and
model relationships were working as expected.

## Week 11 Separation of Concerns and Dependency Injection
This week, I worked on adding Separation of Concerns and Dependency Injection for my Little League Football project. The main goal was to take any logic
out of the controllers and move it into a separate service class so the controllers only handle routing and views. I started by creating an interface
called `ILeagueService` and then a class called `LeagueService` that contains all of the logic for getting teams, rosters, and players. I registered the
service inside Program.cs using a Scoped lifetime so that every request has its own instance. Next, I updated both the `TeamController` and `PlayerController`
to use constructor injection so they could get the service through the DI container instead of manually creating it. This made both controllers much cleaner
and easier to follow. All of the logic for ordering players and retrieving rosters now happens inside `LeagueService`, which helps keep the code organized
and separated into its own layers. Once that was set up, I tested everything by running the app and checking `/Team`, `/Team/Roster/{id}`, and `/Player`.
The rosters loaded correctly, showing each player under the right team and sorted by last and first name. This confirmed that Dependency Injection was
working as expected and that the application now follows proper MVC separation.
