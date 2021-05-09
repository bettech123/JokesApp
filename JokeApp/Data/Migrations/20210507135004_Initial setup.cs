using Microsoft.EntityFrameworkCore.Migrations;

namespace JokeApp.Data.Migrations
{
    public partial class Initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JokeModel",
                columns: table => new
                {
                    JokeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JokeQuestion = table.Column<string>(nullable: true),
                    JokeAnswer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JokeModel", x => x.JokeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JokeModel");
        }
    }
}
