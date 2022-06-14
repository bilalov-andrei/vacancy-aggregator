using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VacancyAggregator.Data.PostgreSql.Migrations
{
    public partial class DatabaseCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "VacancyAggregator_Schema");

            migrationBuilder.CreateTable(
                name: "DataSources",
                schema: "VacancyAggregator_Schema",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    ShortName = table.Column<string>(maxLength: 10, nullable: false),
                    AssemblyPath = table.Column<string>(nullable: true),
                    ConnectionString = table.Column<string>(nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacancyFilters",
                schema: "VacancyAggregator_Schema",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Text = table.Column<string>(maxLength: 100, nullable: false),
                    Salary_From = table.Column<int>(nullable: true),
                    Salary_To = table.Column<int>(nullable: true),
                    Salary_Currency = table.Column<int>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    Experience = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancyFilters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vacancies",
                schema: "VacancyAggregator_Schema",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DataSourceId = table.Column<int>(nullable: false),
                    ExternalId = table.Column<string>(nullable: false),
                    VacancyFilterId = table.Column<int>(nullable: false),
                    ExternalUrl = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 3000, nullable: false),
                    KeySkills = table.Column<string[]>(nullable: true),
                    PublishedAt = table.Column<DateTime>(nullable: true),
                    Salary_From = table.Column<int>(nullable: true),
                    Salary_To = table.Column<int>(nullable: true),
                    Salary_Currency = table.Column<int>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    Experience = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Schedules = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacancies_DataSources_DataSourceId",
                        column: x => x.DataSourceId,
                        principalSchema: "VacancyAggregator_Schema",
                        principalTable: "DataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacancies_VacancyFilters_VacancyFilterId",
                        column: x => x.VacancyFilterId,
                        principalSchema: "VacancyAggregator_Schema",
                        principalTable: "VacancyFilters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "VacancyAggregator_Schema",
                table: "DataSources",
                columns: new[] { "Id", "AssemblyPath", "ConnectionString", "IsEnabled", "Name", "ShortName" },
                values: new object[] { 1, null, null, true, "Internal", "Inter" });

            migrationBuilder.InsertData(
                schema: "VacancyAggregator_Schema",
                table: "VacancyFilters",
                columns: new[] { "Id", "Area", "Experience", "Text", "Salary_Currency", "Salary_From", "Salary_To" },
                values: new object[] { 1, null, 2, "C#", 0, null, null });

            migrationBuilder.InsertData(
                schema: "VacancyAggregator_Schema",
                table: "Vacancies",
                columns: new[] { "Id", "Area", "DataSourceId", "Description", "Experience", "ExternalId", "ExternalUrl", "KeySkills", "Name", "PublishedAt", "Schedules", "Status", "VacancyFilterId", "Salary_Currency", "Salary_From", "Salary_To" },
                values: new object[] { 1, "moscow", 1, "first description", 0, "12", "https:\\site.ru", new[] { "c#", "js" }, "first test vacancy", new DateTime(2022, 6, 10, 8, 49, 55, 203, DateTimeKind.Utc), "[]", 0, 1, 0, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_VacancyFilterId",
                schema: "VacancyAggregator_Schema",
                table: "Vacancies",
                column: "VacancyFilterId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_DataSourceId_ExternalId_VacancyFilterId",
                schema: "VacancyAggregator_Schema",
                table: "Vacancies",
                columns: new[] { "DataSourceId", "ExternalId", "VacancyFilterId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vacancies",
                schema: "VacancyAggregator_Schema");

            migrationBuilder.DropTable(
                name: "DataSources",
                schema: "VacancyAggregator_Schema");

            migrationBuilder.DropTable(
                name: "VacancyFilters",
                schema: "VacancyAggregator_Schema");
        }
    }
}
