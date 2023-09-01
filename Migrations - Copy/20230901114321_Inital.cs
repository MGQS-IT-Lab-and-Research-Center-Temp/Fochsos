using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Fochso.Migrations
{
    /// <inheritdoc />
    public partial class Inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Newses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    ImagePath = table.Column<string>(type: "longtext", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newses", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    TeachingSubject = table.Column<string>(type: "longtext", maxLength: 100000, nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Class = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    ClassId = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "longtext", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    English = table.Column<int>(type: "int", nullable: false),
                    Mathematics = table.Column<int>(type: "int", nullable: false),
                    Civic = table.Column<int>(type: "int", nullable: false),
                    Physics = table.Column<int>(type: "int", nullable: false),
                    Chemistry = table.Column<int>(type: "int", nullable: false),
                    Biology = table.Column<int>(type: "int", nullable: false),
                    FurtherMathematics = table.Column<int>(type: "int", nullable: false),
                    Agric = table.Column<int>(type: "int", nullable: false),
                    IRS = table.Column<int>(type: "int", nullable: false),
                    ICT = table.Column<int>(type: "int", nullable: false),
                    DataProcessing = table.Column<int>(type: "int", nullable: false),
                    Geography = table.Column<int>(type: "int", nullable: false),
                    Literature = table.Column<int>(type: "int", nullable: false),
                    Government = table.Column<int>(type: "int", nullable: false),
                    TD = table.Column<int>(type: "int", nullable: false),
                    Commerce = table.Column<int>(type: "int", nullable: false),
                    Accounting = table.Column<int>(type: "int", nullable: false),
                    Yoruba = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Class = table.Column<string>(type: "longtext", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Results_StudentId",
                table: "Results",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleName",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_ClassId",
                table: "Student",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Newses");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Class");
        }
    }
}
