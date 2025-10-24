using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LittleLeagueFootball.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Buccaneers" },
                    { 2, "Falcons" },
                    { 3, "Panthers" },
                    { 4, "Saints" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "FirstName", "LastName", "TeamId" },
                values: new object[,]
                {
                    { 1, "Baker", "Mayfield", 1 },
                    { 2, "Chris", "Godwin", 1 },
                    { 3, "Mike", "Evans", 1 },
                    { 4, "Emeka", "Egbuka", 1 },
                    { 5, "Lavonte", "David", 1 },
                    { 6, "Rachaad", "White", 1 },
                    { 7, "Bucky", "Irving", 1 },
                    { 8, "Yaya", "Diaby", 1 },
                    { 9, "Sean", "Tucker", 1 },
                    { 10, "Cade", "Otton", 1 },
                    { 11, "Tyler", "Allgeier", 2 },
                    { 12, "Kyle", "Pitts", 2 },
                    { 13, "Darnell", "Mooney", 2 },
                    { 14, "Bijan", "Robinson", 2 },
                    { 15, "Micheal", "Penix Jr.", 2 },
                    { 16, "AJ", "Terrell", 2 },
                    { 17, "Josh", "Woods", 2 },
                    { 18, "Chris", "Blaire", 2 },
                    { 19, "Khalid", "Kareem", 2 },
                    { 20, "Keith", "Taylor", 2 },
                    { 21, "Bryce", "Young", 3 },
                    { 22, "Trevor", "Etienne", 3 },
                    { 23, "Rico", "Dowdle", 3 },
                    { 24, "Ryan", "Fitzgerald", 3 },
                    { 25, "Chuba", "Hubbard", 3 },
                    { 26, "Xavier", "Legette", 3 },
                    { 27, "Tetairoa", "McMillan", 3 },
                    { 28, "Andy", "Dalton", 3 },
                    { 29, "Jalen", "Coker", 3 },
                    { 30, "Austin", "Corbet", 3 },
                    { 31, "Michael", "Davis", 4 },
                    { 32, "Alvin", "Kamara", 4 },
                    { 33, "Chris", "Olave", 4 },
                    { 34, "Terrell", "Burgess", 4 },
                    { 35, "Kendre", "Miller", 4 },
                    { 36, "Bub", "Means", 4 },
                    { 37, "Erik", "McCoy", 4 },
                    { 38, "Will", "Sherman", 4 },
                    { 39, "Dante", "Pettis", 4 },
                    { 40, "Ronnie", "Bell", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
