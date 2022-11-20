using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawRoomApi.DataBase.Migrations
{
    public partial class SaveChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "UserCourses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "UserCourses",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
