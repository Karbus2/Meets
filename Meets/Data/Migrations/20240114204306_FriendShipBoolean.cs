using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meets.Migrations
{
    /// <inheritdoc />
    public partial class FriendShipBoolean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "Friendships",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "Friendships");
        }
    }
}
