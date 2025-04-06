using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingTrackerAPIs.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyNotes_Books_BookId1",
                table: "DailyNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyNotes_Users_UserId1",
                table: "DailyNotes");

            migrationBuilder.DropIndex(
                name: "IX_DailyNotes_BookId1",
                table: "DailyNotes");

            migrationBuilder.DropIndex(
                name: "IX_DailyNotes_UserId1",
                table: "DailyNotes");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "DailyNotes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "DailyNotes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BookId1",
                table: "DailyNotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "DailyNotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyNotes_BookId1",
                table: "DailyNotes",
                column: "BookId1");

            migrationBuilder.CreateIndex(
                name: "IX_DailyNotes_UserId1",
                table: "DailyNotes",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyNotes_Books_BookId1",
                table: "DailyNotes",
                column: "BookId1",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyNotes_Users_UserId1",
                table: "DailyNotes",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
