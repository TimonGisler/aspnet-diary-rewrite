using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharp_diary_rewrite.Migrations
{
    /// <inheritdoc />
    public partial class added_creator_data_to_entry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Entries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_CreatorId",
                table: "Entries",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_AspNetUsers_CreatorId",
                table: "Entries",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_AspNetUsers_CreatorId",
                table: "Entries");

            migrationBuilder.DropIndex(
                name: "IX_Entries_CreatorId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Entries");
        }
    }
}
