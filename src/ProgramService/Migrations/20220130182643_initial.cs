using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgramService.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Program",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Homepage = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    TestPk1 = table.Column<string>(type: "TEXT", nullable: false),
                    TestPk2 = table.Column<string>(type: "TEXT", nullable: false),
                    Field = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => new { x.TestPk1, x.TestPk2 });
                });

            migrationBuilder.CreateTable(
                name: "Version",
                columns: table => new
                {
                    VersionTag = table.Column<string>(type: "TEXT", nullable: false),
                    ProgramName = table.Column<string>(type: "TEXT", nullable: true),
                    Uri = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version", x => x.VersionTag);
                    table.ForeignKey(
                        name: "FK_Version_Program_ProgramName",
                        column: x => x.ProgramName,
                        principalTable: "Program",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Version_ProgramName",
                table: "Version",
                column: "ProgramName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "Version");

            migrationBuilder.DropTable(
                name: "Program");
        }
    }
}
