using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_GCH1108.Data.Migrations
{
    /// <inheritdoc />
    public partial class lastEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_JobList_JobId",
                table: "JobApplication");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "JobApplication",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_JobList_JobId",
                table: "JobApplication",
                column: "JobId",
                principalTable: "JobList",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_JobList_JobId",
                table: "JobApplication");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "JobApplication",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_JobList_JobId",
                table: "JobApplication",
                column: "JobId",
                principalTable: "JobList",
                principalColumn: "JobId");
        }
    }
}
