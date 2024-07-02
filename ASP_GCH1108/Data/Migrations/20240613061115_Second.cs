using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_GCH1108.Data.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobList_AspNetUsers_UserId",
                table: "JobList");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Qualification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "JobList",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "JobList",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobList_AspNetUsers_UserId",
                table: "JobList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobList_AspNetUsers_UserId",
                table: "JobList");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "JobList");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "JobList",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobList_AspNetUsers_UserId",
                table: "JobList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
