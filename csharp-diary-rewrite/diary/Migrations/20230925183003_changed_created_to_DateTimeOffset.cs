using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharp_diary_rewrite.Migrations
{
    /// <inheritdoc />
    public partial class changed_created_to_DateTimeOffset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Entries\" ALTER COLUMN \"Created\" TYPE timestamptz USING \"Created\"::timestamptz");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Created",
                table: "Entries",
                type: "text",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");
        }
    }
}
