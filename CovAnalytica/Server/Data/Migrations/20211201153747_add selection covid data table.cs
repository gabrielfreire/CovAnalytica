using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CovAnalytica.Server.Data.Migrations
{
    public partial class addselectioncoviddatatable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SelectionCovidDataItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Continent = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalCases = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalDeaths = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalCasesPerMillion = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalDeathsPerMillion = table.Column<decimal>(type: "TEXT", nullable: true),
                    IcuPatients = table.Column<decimal>(type: "TEXT", nullable: true),
                    IcuPatientsPerMillion = table.Column<decimal>(type: "TEXT", nullable: true),
                    PeopleVaccinated = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalBoosters = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalBoostersPerHundred = table.Column<decimal>(type: "TEXT", nullable: true),
                    PeopleVaccinatedPerHundred = table.Column<decimal>(type: "TEXT", nullable: true),
                    PeopleFullyVaccinated = table.Column<decimal>(type: "TEXT", nullable: true),
                    PeopleFullyVaccinatedPerHundred = table.Column<decimal>(type: "TEXT", nullable: true),
                    Population = table.Column<decimal>(type: "TEXT", nullable: true),
                    MedianAge = table.Column<decimal>(type: "TEXT", nullable: true),
                    Aged65Older = table.Column<decimal>(type: "TEXT", nullable: true),
                    Aged70Older = table.Column<decimal>(type: "TEXT", nullable: true),
                    CardiovascularDeathRate = table.Column<decimal>(type: "TEXT", nullable: true),
                    DiabetesPrevalence = table.Column<decimal>(type: "TEXT", nullable: true),
                    FemaleSmokers = table.Column<decimal>(type: "TEXT", nullable: true),
                    MaleSmokers = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectionCovidDataItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectionCovidDataItems");
        }
    }
}
