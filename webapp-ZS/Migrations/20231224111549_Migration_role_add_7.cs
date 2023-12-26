using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapp_ZS.Migrations
{
    public partial class Migration_role_add_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userIds",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userIds",
                table: "Carts");
        }
    }
}
