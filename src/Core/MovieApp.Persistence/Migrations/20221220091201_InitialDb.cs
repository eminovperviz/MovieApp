using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.Persistence.Migrations;

/// <inheritdoc />
public partial class InitialDb : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "MovieEntity",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                ReleaseYear = table.Column<int>(type: "int", nullable: false),
                Rating = table.Column<int>(type: "int", nullable: false),
                Synopsis = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MovieEntity", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "MovieEntity");
    }
}
