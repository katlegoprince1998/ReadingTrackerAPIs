using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingTrackerAPIs.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YearlyGoal_Users_UserId",
                table: "YearlyGoal");

            migrationBuilder.DropForeignKey(
                name: "FK_YearlyGoal_Users_UserId1",
                table: "YearlyGoal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_YearlyGoal",
                table: "YearlyGoal");

            migrationBuilder.RenameTable(
                name: "YearlyGoal",
                newName: "YearlyGoals");

            migrationBuilder.RenameIndex(
                name: "IX_YearlyGoal_UserId1",
                table: "YearlyGoals",
                newName: "IX_YearlyGoals_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_YearlyGoal_UserId",
                table: "YearlyGoals",
                newName: "IX_YearlyGoals_UserId");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_YearlyGoals",
                table: "YearlyGoals",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_YearlyGoals_Users_UserId",
                table: "YearlyGoals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_YearlyGoals_Users_UserId1",
                table: "YearlyGoals",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyNotes_Books_BookId1",
                table: "DailyNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyNotes_Users_UserId1",
                table: "DailyNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_YearlyGoals_Users_UserId",
                table: "YearlyGoals");

            migrationBuilder.DropForeignKey(
                name: "FK_YearlyGoals_Users_UserId1",
                table: "YearlyGoals");

            migrationBuilder.DropIndex(
                name: "IX_DailyNotes_BookId1",
                table: "DailyNotes");

            migrationBuilder.DropIndex(
                name: "IX_DailyNotes_UserId1",
                table: "DailyNotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_YearlyGoals",
                table: "YearlyGoals");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "DailyNotes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "DailyNotes");

            migrationBuilder.RenameTable(
                name: "YearlyGoals",
                newName: "YearlyGoal");

            migrationBuilder.RenameIndex(
                name: "IX_YearlyGoals_UserId1",
                table: "YearlyGoal",
                newName: "IX_YearlyGoal_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_YearlyGoals_UserId",
                table: "YearlyGoal",
                newName: "IX_YearlyGoal_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_YearlyGoal",
                table: "YearlyGoal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_YearlyGoal_Users_UserId",
                table: "YearlyGoal",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_YearlyGoal_Users_UserId1",
                table: "YearlyGoal",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
