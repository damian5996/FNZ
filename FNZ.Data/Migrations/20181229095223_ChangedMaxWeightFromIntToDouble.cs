using Microsoft.EntityFrameworkCore.Migrations;

namespace FNZ.Data.Migrations
{
    public partial class ChangedMaxWeightFromIntToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "MaxWeight",
                table: "Animals",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaxWeight",
                table: "Animals",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
