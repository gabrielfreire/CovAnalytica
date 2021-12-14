using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Models
{
    /// <summary>
    /// iso_code,continent,location,date,total_cases,
    /// new_cases,new_cases_smoothed,total_deaths,
    /// new_deaths,new_deaths_smoothed,total_cases_per_million,
    /// new_cases_per_million,new_cases_smoothed_per_million,
    /// total_deaths_per_million,new_deaths_per_million,new_deaths_smoothed_per_million,
    /// reproduction_rate,icu_patients,icu_patients_per_million,
    /// hosp_patients,hosp_patients_per_million,weekly_icu_admissions,
    /// weekly_icu_admissions_per_million,weekly_hosp_admissions,weekly_hosp_admissions_per_million,
    /// new_tests,total_tests,total_tests_per_thousand,new_tests_per_thousand,
    /// new_tests_smoothed,new_tests_smoothed_per_thousand,positive_rate,tests_per_case,
    /// tests_units,total_vaccinations,people_vaccinated,people_fully_vaccinated,total_boosters,
    /// new_vaccinations,new_vaccinations_smoothed,total_vaccinations_per_hundred,
    /// people_vaccinated_per_hundred,people_fully_vaccinated_per_hundred,total_boosters_per_hundred,
    /// new_vaccinations_smoothed_per_million,new_people_vaccinated_smoothed,new_people_vaccinated_smoothed_per_hundred,
    /// stringency_index,population,population_density,median_age,aged_65_older,aged_70_older,gdp_per_capita,
    /// extreme_poverty,cardiovasc_death_rate,diabetes_prevalence,female_smokers,male_smokers,handwashing_facilities,
    /// hospital_beds_per_thousand,life_expectancy,human_development_index,excess_mortality_cumulative_absolute,
    /// excess_mortality_cumulative,excess_mortality,excess_mortality_cumulative_per_million
    /// </summary>
    public class CompleteCovidData
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string IsoCode { get; set; }
        public string Continent { get; set; }
        public string Location { get; set; }
        public DateTime Date{ get; set; }
        public decimal? TotalCases { get; set; }
        public decimal? NewCases { get; set; }
        public decimal? TotalDeaths { get; set; }
        public decimal? NewDeaths { get; set; }
        public decimal? TotalCasesPerMillion { get; set; }
        public decimal? NewCasesPerMillion { get; set; }
        public decimal? TotalDeathsPerMillion { get; set; }
        public decimal? NewDeathsPerMillion { get; set; }
        public decimal? ReproductionRate { get; set; }
        public decimal? IcuPatients { get; set; }
        public decimal? IcuPatientsPerMillion { get; set; }
        public decimal? HospitalPatients { get; set; }
        public decimal? HospitalPatientsPerMillion { get; set; }
        public decimal? WeeklyIcuAdmissions { get; set; }
        public decimal? WeeklyIcuAdmissionsPerMillion { get; set; }
        public decimal? WeeklyHospitalAdmissions { get; set; }
        public decimal? WeeklyHospitalAdmissionsPerMillion { get; set; }
        public decimal? NewTests { get; set; }
        public decimal? TotalTests { get; set; }
        public decimal? TotalTestsPerThousand { get; set; }
        public decimal? NewTestsPerThousand { get; set; }
        public decimal? PositiveRate { get; set; }
        public decimal? TestsPercase { get; set; }
        public decimal? TestsUnits { get; set; }
        public decimal? TotalVaccinations { get; set; }
        public decimal? PeopleVaccinated { get; set; }
        public decimal? PeopleFullyVaccinated { get; set; }
        public decimal? TotalBoosters { get; set; }
        public decimal? NewVaccinations { get; set; }
        public decimal? NewVaccinationsSmoothedPerMillion { get; set; }
        public decimal? NewPeopleVaccinatedSmoothed { get; set; }
        public decimal? NewPeopleVaccinatedSmoothedPerHundred { get; set; }
        public decimal? TotalVaccinationsPerHundred { get; set; }
        public decimal? PeopleVaccinatedPerHundred { get; set; }
        public decimal? PeopleFullyVaccinatedPerHundred { get; set; }
        public decimal? TotalBoostersPerHundred { get; set; }
        public decimal? StringencyIndex { get; set; }
        public decimal? Population { get; set; }
        public decimal? PopulationDensity { get; set; }
        public decimal? MedianAge { get; set; }
        public decimal? Aged65Older { get; set; }
        public decimal? Aged70Older { get; set; }
        public decimal? GdpPerCapita { get; set; }
        public decimal? ExtremePoverty { get; set; }
        public decimal? CardiovascularDeathRate { get; set; }
        public decimal? DiabetesPrevalence { get; set; }
        public decimal? FemaleSmokers { get; set; }
        public decimal? MaleSmokers { get; set; }
        public decimal? HandWashingFacilities { get; set; }
        public decimal? HospitalBedsPerThousand { get; set; }
        public decimal? LifeExpectancy { get; set; }
        public decimal? HumanDevelopmentIndex { get; set; }
        public decimal? ExcessMortalityCumulativeAbsolute { get; set; }
        public decimal? ExcessMortalityCumulative { get; set; }
        public decimal? ExcessMortality { get; set; }
        public decimal? ExcessMortalityCumulativePerMillion { get; set; }

    }
}
