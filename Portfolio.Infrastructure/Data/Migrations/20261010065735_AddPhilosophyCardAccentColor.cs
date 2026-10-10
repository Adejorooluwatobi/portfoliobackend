using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPhilosophyCardAccentColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccentColor",
                table: "philosophy_cards",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "#8b5cf6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccentColor",
                table: "philosophy_cards");
        }
    }
}
