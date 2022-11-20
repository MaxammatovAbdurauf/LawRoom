using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawRoomApi.DataBase.Migrations
{
    public partial class addTaskwirhIsAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "UserCourses",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "UserCourses");
        }
    }
}
