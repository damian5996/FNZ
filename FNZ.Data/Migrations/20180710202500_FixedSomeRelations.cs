using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FNZ.Data.Migrations
{
    public partial class FixedSomeRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastEditedById",
                table: "Tabs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModeratorId",
                table: "Tabs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tabs_LastEditedById",
                table: "Tabs",
                column: "LastEditedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tabs_ModeratorId",
                table: "Tabs",
                column: "ModeratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tabs_Moderators_LastEditedById",
                table: "Tabs",
                column: "LastEditedById",
                principalTable: "Moderators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tabs_Moderators_ModeratorId",
                table: "Tabs",
                column: "ModeratorId",
                principalTable: "Moderators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_Moderators_LastEditedById",
                table: "Tabs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_Moderators_ModeratorId",
                table: "Tabs");

            migrationBuilder.DropIndex(
                name: "IX_Tabs_LastEditedById",
                table: "Tabs");

            migrationBuilder.DropIndex(
                name: "IX_Tabs_ModeratorId",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "LastEditedById",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "ModeratorId",
                table: "Tabs");
        }
    }
}
