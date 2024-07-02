using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_GCH1108.Data.Migrations
{
    /// <inheritdoc />
    public partial class ASP_GCH1108 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequiredQualifications",
                table: "JobList",
                newName: "RequiredProfiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequiredProfiles",
                table: "JobList",
                newName: "RequiredQualifications");
        }
    }
}
