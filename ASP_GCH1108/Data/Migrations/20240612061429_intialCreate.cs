using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_GCH1108.Data.Migrations
{
    /// <inheritdoc />
    public partial class intialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobList",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredQualifications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationDeadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobList", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_JobList_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Qualification",
                columns: table => new
                {
                    QualificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Licensor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DegreeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualification", x => x.QualificationId);
                });

            migrationBuilder.CreateTable(
                name: "JobApplication",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobListId = table.Column<int>(type: "int", nullable: true),
                    QualificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplication", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_JobApplication_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplication_JobList_JobListId",
                        column: x => x.JobListId,
                        principalTable: "JobList",
                        principalColumn: "JobId");
                    table.ForeignKey(
                        name: "FK_JobApplication_Qualification_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Qualification",
                        principalColumn: "QualificationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_JobListId",
                table: "JobApplication",
                column: "JobListId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_QualificationId",
                table: "JobApplication",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_UserId",
                table: "JobApplication",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobList_UserId",
                table: "JobList",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplication");

            migrationBuilder.DropTable(
                name: "JobList");

            migrationBuilder.DropTable(
                name: "Qualification");
        }
    }
}
