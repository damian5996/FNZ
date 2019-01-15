using Microsoft.EntityFrameworkCore.Migrations;

namespace FNZ.Data.Migrations
{
    public partial class renameColumnInApplications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Applications",
                newName: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Applications",
                newName: "Adress");
        }
    }
}
