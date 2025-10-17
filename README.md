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
