using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_GCH1108.Data.Migrations
{
    /// <inheritdoc />
    public partial class Five : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_JobList_JobListId",
                table: "JobApplication");

            migrationBuilder.RenameColumn(
                name: "JobListId",
                table: "JobApplication",
                newName: "JobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplication_JobListId",
                table: "JobApplication",
                newName: "IX_JobApplication_JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_JobList_JobId",
                table: "JobApplication",
                column: "JobId",
                principalTable: "JobList",
                principalColumn: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_JobList_JobId",
                table: "JobApplication");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "JobApplication",
                newName: "JobListId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplication_JobId",
                table: "JobApplication",
                newName: "IX_JobApplication_JobListId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_JobList_JobListId",
                table: "JobApplication",
                column: "JobListId",
                principalTable: "JobList",
                principalColumn: "JobId");
        }
    }
}
