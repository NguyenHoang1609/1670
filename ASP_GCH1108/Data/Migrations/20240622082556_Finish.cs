using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_GCH1108.Data.Migrations
{
    /// <inheritdoc />
    public partial class Finish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_Qualification_QualificationId",
                table: "JobApplication");

            migrationBuilder.DropTable(
                name: "Qualification");

            migrationBuilder.RenameColumn(
                name: "QualificationId",
                table: "JobApplication",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplication_QualificationId",
                table: "JobApplication",
                newName: "IX_JobApplication_ProfileId");

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.ProfileId);
                    table.ForeignKey(
                        name: "FK_Profile_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profile_UserId",
                table: "Profile",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_Profile_ProfileId",
                table: "JobApplication",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "ProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_Profile_ProfileId",
                table: "JobApplication");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "JobApplication",
                newName: "QualificationId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplication_ProfileId",
                table: "JobApplication",
                newName: "IX_JobApplication_QualificationId");

            migrationBuilder.CreateTable(
                name: "Qualification",
                columns: table => new
                {
                    QualificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DegreeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Licensor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualification", x => x.QualificationId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_Qualification_QualificationId",
                table: "JobApplication",
                column: "QualificationId",
                principalTable: "Qualification",
                principalColumn: "QualificationId");
        }
    }
}
