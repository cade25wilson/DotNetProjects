using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssuePriority",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Issue_Priority_Type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuePriority", x => x.Id);
                    table.UniqueConstraint("AK_IssuePriority_Issue_Priority_Type", x => x.Issue_Priority_Type);
                });

            migrationBuilder.CreateTable(
                name: "IssueStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Issue_Status_Type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueStatus", x => x.Id);
                    table.UniqueConstraint("AK_IssueStatus_Issue_Status_Type", x => x.Issue_Status_Type);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.UniqueConstraint("AK_Projects_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    EmailAddress = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    AllowEmailNotification = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__1788CC4C7C9531A8", x => x.UserId);
                    table.UniqueConstraint("AK_UserProfile_DisplayName", x => x.DisplayName);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Issue_Title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Issue_Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Issue_Type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Issue_Priority = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Issue_CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Issue_CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Issue_ClosedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Issue_ClosedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Issue_ResolutionSummary = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Project = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_IssuePriority",
                        column: x => x.Issue_Priority,
                        principalTable: "IssuePriority",
                        principalColumn: "Issue_Priority_Type");
                    table.ForeignKey(
                        name: "FK_Issues_IssueStatus",
                        column: x => x.Issue_Type,
                        principalTable: "IssueStatus",
                        principalColumn: "Issue_Status_Type");
                    table.ForeignKey(
                        name: "FK_Issues_Projects",
                        column: x => x.Project,
                        principalTable: "Projects",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_Issues_UserProfile",
                        column: x => x.Issue_CreatedBy,
                        principalTable: "UserProfile",
                        principalColumn: "DisplayName");
                    table.ForeignKey(
                        name: "FK_Issues_UserProfile2",
                        column: x => x.Issue_ClosedBy,
                        principalTable: "UserProfile",
                        principalColumn: "DisplayName");
                });

            migrationBuilder.CreateTable(
                name: "ProjectAccess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    User = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    AccessType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectAccess_Projects",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectAccess_UserProfile",
                        column: x => x.User,
                        principalTable: "UserProfile",
                        principalColumn: "DisplayName");
                });

            migrationBuilder.CreateIndex(
                name: "AK_IssuePriority_Issue_Priority_Type",
                table: "IssuePriority",
                column: "Issue_Priority_Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Issue_ClosedBy",
                table: "Issues",
                column: "Issue_ClosedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Issue_CreatedBy",
                table: "Issues",
                column: "Issue_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Issue_Priority",
                table: "Issues",
                column: "Issue_Priority");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Issue_Type",
                table: "Issues",
                column: "Issue_Type");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Project",
                table: "Issues",
                column: "Project");

            migrationBuilder.CreateIndex(
                name: "AK_IssueStatus_Issue_Status_Type",
                table: "IssueStatus",
                column: "Issue_Status_Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAccess_ProjectId",
                table: "ProjectAccess",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAccess_User",
                table: "ProjectAccess",
                column: "User");

            migrationBuilder.CreateIndex(
                name: "AK_Projects_Name",
                table: "Projects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_UserProfile_DisplayName",
                table: "UserProfile",
                column: "DisplayName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "ProjectAccess");

            migrationBuilder.DropTable(
                name: "IssuePriority");

            migrationBuilder.DropTable(
                name: "IssueStatus");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "UserProfile");
        }
    }
}
