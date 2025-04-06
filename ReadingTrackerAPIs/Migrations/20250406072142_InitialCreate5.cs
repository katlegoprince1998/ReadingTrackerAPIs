using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingTrackerAPIs.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyNotes_Books_BookId",
                table: "DailyNotes");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyNotes_Books_BookId",
                table: "DailyNotes",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyNotes_Books_BookId",
                table: "DailyNotes");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyNotes_Books_BookId",
                table: "DailyNotes",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
