using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharp_diary_rewrite.Migrations
{
    /// <inheritdoc />
    public partial class remove_updated_column_from_entry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryText",
                table: "Entries");

            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "Entries",
                newName: "Text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Entries",
                newName: "Updated");

            migrationBuilder.AddColumn<string>(
                name: "EntryText",
                table: "Entries",
                type: "text",
                nullable: true);
        }
    }
}
