using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawRoomApi.DataBase.Migrations
{
    public partial class updataagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Tasks",
                newName: "TaskDescription");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskDescription",
                table: "Tasks",
                newName: "Description");
        }
    }
}
