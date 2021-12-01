using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CovAnalytica.Server.Data.Migrations
{
    public partial class addupdatemetadatatable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompleteCovidDataItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    IsoCode = table.Column<string>(type: "TEXT", nullable: false),
                    Continent = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalCases = table.Column<decimal>(type: "TEXT", nullable: true),
                    NewCases = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalDeaths = table.Column<decimal>(type: "TEXT", nullable: true),
                    NewDeaths = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalCasesPerMillion = table.Column<decimal>(type: "TEXT", nullable: true),
                    NewCasesPerMillion = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalDeathsPerMillion = table.Column<decimal>(type: "TEXT", nullable: true),
                    NewDeathsPerMillion = table.Column<decimal>(type: "TEXT", nullable: true),
                    ReproductionRate = table.Column<decimal>(type: "TEXT", nullable: true),
                    IcuPatients = table.Column<decimal>(type: "TEXT", nullable: true),
                    IcuPatientsPerMillion = table.Column<decimal>(type: "TEXT", nullable: true),
                    HospitalPatients = table.Column<decimal>(type: "TEXT", nullable: true),
                    HospitalPatientsPerMillion = table.Column<decimal>(type: "TEXT", nullable: true),
                    WeeklyIcuAdmissions = table.Column<decimal>(type: "TEXT", nullable: true),
                    WeeklyIcuAdmissionsPerMillion = table.Column<decimal>(type: "TEXT", nullable: true),
                    WeeklyHospitalAdmissions = table.Column<decimal>(type: "TEXT", nullable: true),
                    WeeklyHospitalAdmissionsPerMillion = table.Column<decimal>(type: "TEXT", nullable: true),
                    NewTests = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalTests = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalTestsPerThousand = table.Column<decimal>(type: "TEXT", nullable: true),
                    NewTestsPerThousand = table.Column<decimal>(type: "TEXT", nullable: true),
                    PositiveRate = table.Column<decimal>(type: "TEXT", nullable: true),
                    TestsPercase = table.Column<decimal>(type: "TEXT", nullable: true),
                    TestsUnits = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalVaccinations = table.Column<decimal>(type: "TEXT", nullable: true),
                    PeopleVaccinated = table.Column<decimal>(type: "TEXT", nullable: true),
                    PeopleFullyVaccinated = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalBoosters = table.Column<decimal>(type: "TEXT", nullable: true),
                    NewVaccinations = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalVaccinationsPerHundred = table.Column<decimal>(type: "TEXT", nullable: true),
                    PeopleVaccinatedPerHundred = table.Column<decimal>(type: "TEXT", nullable: true),
                    PeopleFullyVaccinatedPerHundred = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalBoostersPerHundred = table.Column<decimal>(type: "TEXT", nullable: true),
                    StringencyIndex = table.Column<decimal>(type: "TEXT", nullable: true),
                    Population = table.Column<decimal>(type: "TEXT", nullable: true),
                    PopulationDensity = table.Column<decimal>(type: "TEXT", nullable: true),
                    MedianAge = table.Column<decimal>(type: "TEXT", nullable: true),
                    Aged65Older = table.Column<decimal>(type: "TEXT", nullable: true),
                    Aged70Older = table.Column<decimal>(type: "TEXT", nullable: true),
                    GdpPerCapita = table.Column<decimal>(type: "TEXT", nullable: true),
                    ExtremePoverty = table.Column<decimal>(type: "TEXT", nullable: true),
                    CardiovascularDeathRate = table.Column<decimal>(type: "TEXT", nullable: true),
                    DiabetesPrevalence = table.Column<decimal>(type: "TEXT", nullable: true),
                    FemaleSmokers = table.Column<decimal>(type: "TEXT", nullable: true),
                    MaleSmokers = table.Column<decimal>(type: "TEXT", nullable: true),
                    HandWashingFacilities = table.Column<decimal>(type: "TEXT", nullable: true),
                    HospitalBedsPerThousand = table.Column<decimal>(type: "TEXT", nullable: true),
                    LifeExpectancy = table.Column<decimal>(type: "TEXT", nullable: true),
                    HumanDevelopmentIndex = table.Column<decimal>(type: "TEXT", nullable: true),
                    ExcessMortalityCumulativeAbsolute = table.Column<decimal>(type: "TEXT", nullable: true),
                    ExcessMortalityCumulative = table.Column<decimal>(type: "TEXT", nullable: true),
                    ExcessMortality = table.Column<decimal>(type: "TEXT", nullable: true),
                    ExcessMortalityCumulativePerMillion = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompleteCovidDataItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UpdateMetadataItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateMetadataItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompleteCovidDataItems");

            migrationBuilder.DropTable(
                name: "UpdateMetadataItems");
        }
    }
}
