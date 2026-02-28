using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_MZ.Migrations
{
    /// <inheritdoc />
    public partial class AddLinkedInAndGitHubToCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GitHub",
                table: "Creators",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "Creators",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GitHub",
                table: "Creators");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "Creators");
        }
    }
}
