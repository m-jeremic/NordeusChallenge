using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendAndGameDesign.Migrations
{
    public partial class IsDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "isDefault",
                table: "Clubs",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDefault",
                table: "Clubs");
        }
    }
}
