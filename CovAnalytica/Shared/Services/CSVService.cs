using CovAnalytica.Shared.Interfaces;
using CovAnalytica.Shared.Models;
using Sylvan.Data.Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Services
{
    /// <summary>
    /// 
    /// using var csv = CsvDataReader.Create("demo.csv");

    /// var idIndex = csv.GetOrdinal("Id");
    /// var nameIndex = csv.GetOrdinal("Name");
    /// var dateIndex = csv.GetOrdinal("Date");
    /// 
    /// while(await csv.ReadAsync()) 
    /// {
    ///     var id = csv.GetString(idIndex);
    ///     var name = csv.GetString(nameIndex);
    ///     var date = csv.GetDateTime(dateIndex);
    /// }

    /// </summary>
    public class CSVService : ICSVService
    {
        public async Task<ICollection<CompleteCovidData>> CompleteCovidDataFromStringAsync(string csvAsString)
        {
            var _completeCovidDataList = new List<CompleteCovidData>();
            await File.WriteAllTextAsync(Constants.COMPLETE_COVID_DATA_FILE_PATH, csvAsString);

            using (var textReader = new StreamReader(Constants.COMPLETE_COVID_DATA_FILE_PATH))
            {

                using var csv = await CsvDataReader.CreateAsync(textReader);
                var isoCodeIdx = csv.GetOrdinal("iso_code");
                var continentIdx = csv.GetOrdinal("continent");
                var locationIdx = csv.GetOrdinal("location");
                var dateIdx = csv.GetOrdinal("date");
                var totalCasesIdx = csv.GetOrdinal("total_cases");
                var newCasesIdx = csv.GetOrdinal("new_cases");
                var totalDeathsIdx = csv.GetOrdinal("total_deaths");
                var newDeathsIdx = csv.GetOrdinal("new_deaths");
                var totalCasesPerMillionIdx = csv.GetOrdinal("total_cases_per_million");
                var newCasesPerMillionIdx = csv.GetOrdinal("new_cases_per_million");
                var totalDeathsPerMillionIdx = csv.GetOrdinal("total_deaths_per_million");
                var newDeathsPerMillionIdx = csv.GetOrdinal("new_deaths_per_million");
                var reproductionRateIdx = csv.GetOrdinal("reproduction_rate");
                var icuPatientsIdx = csv.GetOrdinal("icu_patients");
                var icuPatientsPerMillionIdx = csv.GetOrdinal("icu_patients_per_million");
                var hospPatientsIdx = csv.GetOrdinal("hosp_patients");
                var hospPatientsPerMillionIdx = csv.GetOrdinal("hosp_patients_per_million");
                var weeklyIcuAdmissionsIdx = csv.GetOrdinal("weekly_icu_admissions");
                var weeklyIcuAdmissionsPerMillionIdx = csv.GetOrdinal("weekly_icu_admissions_per_million");
                var weeklyHospAdmissionsIdx = csv.GetOrdinal("weekly_hosp_admissions");
                var weeklyHospAdmissionsPerMillionIdx = csv.GetOrdinal("weekly_hosp_admissions_per_million");
                var newTestsIdx = csv.GetOrdinal("new_tests");
                var totalTestsIdx = csv.GetOrdinal("total_tests");
                var totalTestsPerThousandIdx = csv.GetOrdinal("total_tests_per_thousand");
                var newTestsPerThousandIdx = csv.GetOrdinal("new_tests_per_thousand");
                var positiveRateIdx = csv.GetOrdinal("positive_rate");
                var testsPerCaseIdx = csv.GetOrdinal("tests_per_case");
                var testsUnitsIdx = csv.GetOrdinal("tests_units");
                var totalVaccinationsIdx = csv.GetOrdinal("total_vaccinations");
                var peopleVaccinatedIdx = csv.GetOrdinal("people_vaccinated");
                var peopleFullyVaccinatedIdx = csv.GetOrdinal("people_fully_vaccinated");
                var totalBoostersIdx = csv.GetOrdinal("total_boosters");
                var newVaccinationsIdx = csv.GetOrdinal("new_vaccinations");
                var totalVaccinationsPerHundredIdx = csv.GetOrdinal("total_vaccinations_per_hundred");
                var peopleVaccinatedPerHundredIdx = csv.GetOrdinal("people_vaccinated_per_hundred");
                var peopleFullyVaccinatedPerHundredIdx = csv.GetOrdinal("people_fully_vaccinated_per_hundred");
                var totalBoostersPerHundredIdx = csv.GetOrdinal("total_boosters_per_hundred");
                var stringencyIndexIdx = csv.GetOrdinal("stringency_index");
                var populationIdx = csv.GetOrdinal("population");
                var populationDensityIdx = csv.GetOrdinal("population_density");
                var medianAgeIdx = csv.GetOrdinal("median_age");
                var aged65OlderIdx = csv.GetOrdinal("aged_65_older");
                var aged70OlderIdx = csv.GetOrdinal("aged_70_older");
                var gdpPerCapitaIdx = csv.GetOrdinal("gdp_per_capita");
                var extremePovertyIdx = csv.GetOrdinal("extreme_poverty");
                var cardiovascDeathRateIdx = csv.GetOrdinal("cardiovasc_death_rate");
                var diabetesPrevalenceIdx = csv.GetOrdinal("diabetes_prevalence");
                var femaleSmokersIdx = csv.GetOrdinal("female_smokers");
                var maleSmokersIdx = csv.GetOrdinal("male_smokers");
                var handwashingFacilitiesIdx = csv.GetOrdinal("handwashing_facilities");
                var hospitalBedsPerThousandIdx = csv.GetOrdinal("hospital_beds_per_thousand");
                var lifeExpectancyIdx = csv.GetOrdinal("life_expectancy");
                var humanDevelopmentIndexIdx = csv.GetOrdinal("human_development_index");
                var excessMortalityCumulativeAbsoluteIdx = csv.GetOrdinal("excess_mortality_cumulative_absolute");
                var excessMortalityCumulativeIdx = csv.GetOrdinal("excess_mortality_cumulative");
                var excessMortalityIdx = csv.GetOrdinal("excess_mortality");
                var excessMortalityCumulativePerMillionIdx = csv.GetOrdinal("excess_mortality_cumulative_per_million");

                while (await csv.ReadAsync())
                {
                    var isoCodeValue = csv.GetString(isoCodeIdx);
                    var continentValue = csv.GetString(continentIdx);
                    var locationValue = csv.GetString(locationIdx);
                    var dateValue = csv.GetDateTime(dateIdx);
                    var totalCasesValue = csv.GetString(totalCasesIdx);
                    var newCasesValue = csv.GetString(newCasesIdx);
                    var totalDeathsValue = csv.GetString(totalDeathsIdx);
                    var newDeathsValue = csv.GetString(newDeathsIdx);
                    var totalCasesPerMillionValue = csv.GetString(totalCasesPerMillionIdx);
                    var newCasesPerMillionValue = csv.GetString(newCasesPerMillionIdx);
                    var totalDeathsPerMillionValue = csv.GetString(totalDeathsPerMillionIdx);
                    var newDeathsPerMillionValue = csv.GetString(newDeathsPerMillionIdx);
                    var reproductionRateValue = csv.GetString(reproductionRateIdx);
                    var icuPatientsValue = csv.GetString(icuPatientsIdx);
                    var icuPatientsPerMillionValue = csv.GetString(icuPatientsPerMillionIdx);
                    var hospPatientsValue = csv.GetString(hospPatientsIdx);
                    var hospPatientsPerMillionValue = csv.GetString(hospPatientsPerMillionIdx);
                    var weeklyIcuAdmissionsValue = csv.GetString(weeklyIcuAdmissionsIdx);
                    var weeklyIcuAdmissionsPerMillionValue = csv.GetString(weeklyIcuAdmissionsPerMillionIdx);
                    var weeklyHospAdmissionsValue = csv.GetString(weeklyHospAdmissionsIdx);
                    var weeklyHospAdmissionsPerMillionValue = csv.GetString(weeklyHospAdmissionsPerMillionIdx);
                    var newTestsValue = csv.GetString(newTestsIdx);
                    var totalTestsValue = csv.GetString(totalTestsIdx);
                    var totalTestsPerThousandValue = csv.GetString(totalTestsPerThousandIdx);
                    var newTestsPerThousandValue = csv.GetString(newTestsPerThousandIdx);
                    var positiveRateValue = csv.GetString(positiveRateIdx);
                    var testsPerCaseValue = csv.GetString(testsPerCaseIdx);
                    var testsUnitsValue = csv.GetString(testsUnitsIdx);
                    var totalVaccinationsValue = csv.GetString(totalVaccinationsIdx);
                    var peopleVaccinatedValue = csv.GetString(peopleVaccinatedIdx);
                    var peopleFullyVaccinatedValue = csv.GetString(peopleFullyVaccinatedIdx);
                    var totalBoostersValue = csv.GetString(totalBoostersIdx);
                    var newVaccinationsValue = csv.GetString(newVaccinationsIdx);
                    var totalVaccinationsPerHundredValue = csv.GetString(totalVaccinationsPerHundredIdx);
                    var peopleVaccinatedPerHundredValue = csv.GetString(peopleVaccinatedPerHundredIdx);
                    var peopleFullyVaccinatedPerHundredValue = csv.GetString(peopleFullyVaccinatedPerHundredIdx);
                    var totalBoostersPerHundredValue = csv.GetString(totalBoostersPerHundredIdx);
                    var stringencyIndexValue = csv.GetString(stringencyIndexIdx);
                    var populationValue = csv.GetString(populationIdx);
                    var populationDensityValue = csv.GetString(populationDensityIdx);
                    var medianAgeValue = csv.GetString(medianAgeIdx);
                    var aged65OlderValue = csv.GetString(aged65OlderIdx);
                    var aged70OlderValue = csv.GetString(aged70OlderIdx);
                    var gdpPerCapitaValue = csv.GetString(gdpPerCapitaIdx);
                    var extremePovertyValue = csv.GetString(extremePovertyIdx);
                    var cardiovascDeathRateValue = csv.GetString(cardiovascDeathRateIdx);
                    var diabetesPrevalenceValue = csv.GetString(diabetesPrevalenceIdx);
                    var femaleSmokersValue = csv.GetString(femaleSmokersIdx);
                    var maleSmokersValue = csv.GetString(maleSmokersIdx);
                    var handwashingFacilitiesValue = csv.GetString(handwashingFacilitiesIdx);
                    var hospitalBedsPerThousandValue = csv.GetString(hospitalBedsPerThousandIdx);
                    var lifeExpectancyValue = csv.GetString(lifeExpectancyIdx);
                    var humanDevelopmentIndexValue = csv.GetString(humanDevelopmentIndexIdx);
                    var excessMortalityCumulativeAbsoluteValue = csv.GetString(excessMortalityCumulativeAbsoluteIdx);
                    var excessMortalityCumulativeValue = csv.GetString(excessMortalityCumulativeIdx);
                    var excessMortalityValue = csv.GetString(excessMortalityIdx);
                    var excessMortalityCumulativePerMillionValue = csv.GetString(excessMortalityCumulativePerMillionIdx);
                    

                    _completeCovidDataList.Add(new CompleteCovidData()
                    {
                        IsoCode = string.IsNullOrWhiteSpace(isoCodeValue) ? string.Empty : isoCodeValue,
                        Continent = string.IsNullOrWhiteSpace(continentValue) ? string.Empty : continentValue,
                        Location = string.IsNullOrWhiteSpace(locationValue) ? string.Empty : locationValue,
                        Date = dateValue,
                        TotalCases = string.IsNullOrWhiteSpace(totalCasesValue) ? null : decimal.Parse(totalCasesValue),
                        NewCases = string.IsNullOrWhiteSpace(newCasesValue) ? null : decimal.Parse(newCasesValue),
                        TotalDeaths = string.IsNullOrWhiteSpace(totalDeathsValue) ? null : decimal.Parse(totalDeathsValue),
                        NewDeaths = string.IsNullOrWhiteSpace(newDeathsValue) ? null : decimal.Parse(newDeathsValue),
                        TotalCasesPerMillion = string.IsNullOrWhiteSpace(totalCasesPerMillionValue) ? null : decimal.Parse(totalCasesPerMillionValue),
                        NewCasesPerMillion = string.IsNullOrWhiteSpace(newCasesPerMillionValue) ? null : decimal.Parse(newCasesPerMillionValue),
                        TotalDeathsPerMillion= string.IsNullOrWhiteSpace(totalDeathsPerMillionValue) ? null : decimal.Parse(totalDeathsPerMillionValue),
                        NewDeathsPerMillion = string.IsNullOrWhiteSpace(newDeathsPerMillionValue) ? null : decimal.Parse(newDeathsPerMillionValue),
                        ReproductionRate = string.IsNullOrWhiteSpace(reproductionRateValue) ? null : decimal.Parse(reproductionRateValue),
                        IcuPatients = string.IsNullOrWhiteSpace(icuPatientsValue) ? null : decimal.Parse(icuPatientsValue),
                        IcuPatientsPerMillion = string.IsNullOrWhiteSpace(icuPatientsPerMillionValue) ? null : decimal.Parse(icuPatientsPerMillionValue),
                        HospitalPatients = string.IsNullOrWhiteSpace(hospPatientsValue) ? null : decimal.Parse(hospPatientsValue),
                        HospitalPatientsPerMillion = string.IsNullOrWhiteSpace(hospPatientsPerMillionValue) ? null : decimal.Parse(hospPatientsPerMillionValue),
                        WeeklyIcuAdmissions = string.IsNullOrWhiteSpace(weeklyIcuAdmissionsValue) ? null : decimal.Parse(weeklyIcuAdmissionsValue),
                        WeeklyHospitalAdmissionsPerMillion = string.IsNullOrWhiteSpace(weeklyIcuAdmissionsPerMillionValue) ? null : decimal.Parse(weeklyIcuAdmissionsPerMillionValue),
                        WeeklyHospitalAdmissions = string.IsNullOrWhiteSpace(weeklyHospAdmissionsValue) ? null : decimal.Parse(weeklyHospAdmissionsValue),
                        WeeklyIcuAdmissionsPerMillion = string.IsNullOrWhiteSpace(weeklyHospAdmissionsPerMillionValue) ? null : decimal.Parse(weeklyHospAdmissionsPerMillionValue),
                        NewTests = string.IsNullOrWhiteSpace(newTestsValue) ? null : decimal.Parse(newTestsValue),
                        TotalTests = string.IsNullOrWhiteSpace(totalTestsValue) ? null : decimal.Parse(totalTestsValue),
                        TotalTestsPerThousand = string.IsNullOrWhiteSpace(totalTestsPerThousandValue) ? null : decimal.Parse(totalTestsPerThousandValue),
                        NewTestsPerThousand = string.IsNullOrWhiteSpace(newTestsPerThousandValue) ? null : decimal.Parse(newTestsPerThousandValue),
                        PositiveRate = string.IsNullOrWhiteSpace(positiveRateValue) ? null : decimal.Parse(positiveRateValue),
                        TestsPercase= string.IsNullOrWhiteSpace(testsPerCaseValue) ? null : decimal.Parse(testsPerCaseValue),
                        TestsUnits = string.IsNullOrWhiteSpace(testsUnitsValue) ? null : decimal.TryParse(testsUnitsValue, out var res) ? res : null,
                        TotalVaccinations = string.IsNullOrWhiteSpace(totalVaccinationsValue) ? null : decimal.Parse(totalVaccinationsValue),
                        PeopleVaccinated = string.IsNullOrWhiteSpace(peopleVaccinatedValue) ? null : decimal.Parse(peopleVaccinatedValue),
                        PeopleFullyVaccinated = string.IsNullOrWhiteSpace(peopleFullyVaccinatedValue) ? null : decimal.Parse(peopleFullyVaccinatedValue),
                        TotalBoosters = string.IsNullOrWhiteSpace(totalBoostersValue) ? null : decimal.Parse(totalBoostersValue),
                        NewVaccinations = string.IsNullOrWhiteSpace(newVaccinationsValue) ? null : decimal.Parse(newVaccinationsValue),
                        TotalVaccinationsPerHundred = string.IsNullOrWhiteSpace(totalVaccinationsPerHundredValue) ? null : decimal.Parse(totalVaccinationsPerHundredValue),
                        PeopleVaccinatedPerHundred = string.IsNullOrWhiteSpace(peopleVaccinatedPerHundredValue) ? null : decimal.Parse(peopleVaccinatedPerHundredValue),
                        PeopleFullyVaccinatedPerHundred = string.IsNullOrWhiteSpace(peopleFullyVaccinatedPerHundredValue) ? null : decimal.Parse(peopleFullyVaccinatedPerHundredValue),
                        TotalBoostersPerHundred = string.IsNullOrWhiteSpace(totalBoostersPerHundredValue) ? null : decimal.Parse(totalBoostersPerHundredValue),
                        StringencyIndex = string.IsNullOrWhiteSpace(stringencyIndexValue) ? null : decimal.Parse(stringencyIndexValue),
                        Population= string.IsNullOrWhiteSpace(populationValue) ? null : decimal.Parse(populationValue),
                        PopulationDensity = string.IsNullOrWhiteSpace(populationDensityValue) ? null : decimal.Parse(populationDensityValue),
                        MedianAge = string.IsNullOrWhiteSpace(medianAgeValue) ? null : decimal.Parse(medianAgeValue),
                        Aged65Older = string.IsNullOrWhiteSpace(aged65OlderValue) ? null : decimal.Parse(aged65OlderValue),
                        Aged70Older = string.IsNullOrWhiteSpace(aged70OlderValue) ? null : decimal.Parse(aged70OlderValue),
                        GdpPerCapita = string.IsNullOrWhiteSpace(gdpPerCapitaValue) ? null : decimal.Parse(gdpPerCapitaValue),
                        ExtremePoverty = string.IsNullOrWhiteSpace(extremePovertyValue) ? null : decimal.Parse(extremePovertyValue),
                        CardiovascularDeathRate = string.IsNullOrWhiteSpace(cardiovascDeathRateValue) ? null : decimal.Parse(cardiovascDeathRateValue),
                        DiabetesPrevalence = string.IsNullOrWhiteSpace(diabetesPrevalenceValue) ? null : decimal.Parse(diabetesPrevalenceValue),
                        FemaleSmokers = string.IsNullOrWhiteSpace(femaleSmokersValue) ? null : decimal.Parse(femaleSmokersValue),
                        MaleSmokers = string.IsNullOrWhiteSpace(maleSmokersValue) ? null : decimal.Parse(maleSmokersValue),
                        HandWashingFacilities = string.IsNullOrWhiteSpace(handwashingFacilitiesValue) ? null : decimal.Parse(handwashingFacilitiesValue),
                        HospitalBedsPerThousand = string.IsNullOrWhiteSpace(hospitalBedsPerThousandValue) ? null : decimal.Parse(hospitalBedsPerThousandValue),
                        LifeExpectancy = string.IsNullOrWhiteSpace(lifeExpectancyValue) ? null : decimal.Parse(lifeExpectancyValue),
                        HumanDevelopmentIndex = string.IsNullOrWhiteSpace(humanDevelopmentIndexValue) ? null : decimal.Parse(humanDevelopmentIndexValue),
                        ExcessMortalityCumulativeAbsolute = string.IsNullOrWhiteSpace(excessMortalityCumulativeAbsoluteValue) ? null : decimal.Parse(excessMortalityCumulativeAbsoluteValue),
                        ExcessMortalityCumulative = string.IsNullOrWhiteSpace(excessMortalityCumulativeValue) ? null : decimal.Parse(excessMortalityCumulativeValue),
                        ExcessMortality = string.IsNullOrWhiteSpace(excessMortalityValue) ? null : decimal.Parse(excessMortalityValue),
                        ExcessMortalityCumulativePerMillion = string.IsNullOrWhiteSpace(excessMortalityCumulativePerMillionValue) ? null : decimal.Parse(excessMortalityCumulativePerMillionValue)
                    });
                }
            }

            File.Delete(Constants.COMPLETE_COVID_DATA_FILE_PATH);

            return _completeCovidDataList;
        }

        public async Task<ICollection<VaccinationLocation>> LocationsFromStringAsync(string csvAsString)
        {
            var _vaccinationLocations = new List<VaccinationLocation>();

            await File.WriteAllTextAsync(Constants.VACCINATION_LOCATIONS_FILE_PATH, csvAsString);

            using (var textReader = new StreamReader(Constants.VACCINATION_LOCATIONS_FILE_PATH))
            {

                using var csv = await CsvDataReader.CreateAsync(textReader);

                var locationIdx = csv.GetOrdinal("location");
                var isoCodeIdx = csv.GetOrdinal("iso_code");
                var vaccinesIdx = csv.GetOrdinal("vaccines");
                var lastObsDateIdx = csv.GetOrdinal("last_observation_date");
                var sourceNameIdx = csv.GetOrdinal("source_name");
                var sourceWebsiteIdx = csv.GetOrdinal("source_website");
                while (await csv.ReadAsync()) 
                {
                    _vaccinationLocations.Add(new VaccinationLocation()
                    {
                        Location = csv.GetString(locationIdx),
                        IsoCode = csv.GetString(isoCodeIdx),
                        LastObservationDate = csv.GetDateTime(lastObsDateIdx),
                        SourceName = csv.GetString(sourceNameIdx),
                        SourceWebsite = csv.GetString(sourceWebsiteIdx),
                        Vaccines = csv.GetString(vaccinesIdx)
                    });
                }
            }

            File.Delete(Constants.VACCINATION_LOCATIONS_FILE_PATH);

            return _vaccinationLocations;
        }

        public async Task<ICollection<Vaccination>> VaccinationsFromStringAsync(string csvAsString)
        {
            var _vaccinations = new List<Vaccination>();

            await File.WriteAllTextAsync(Constants.VACCINATIONS_FILE_PATH, csvAsString);

            using (var textReader = new StreamReader(Constants.VACCINATIONS_FILE_PATH))
            {

                using var csv = await CsvDataReader.CreateAsync(textReader);

                var locationIdx = csv.GetOrdinal("location");
                var isoCodeIdx = csv.GetOrdinal("iso_code");
                var dateIdx = csv.GetOrdinal("date");
                var totalVaccinationsIdx = csv.GetOrdinal("total_vaccinations");
                var peopleVaccinatedIdx = csv.GetOrdinal("people_vaccinated");
                var peopleFullyVaccinatedIdx = csv.GetOrdinal("people_fully_vaccinated");
                var totalBoostersIdx = csv.GetOrdinal("total_boosters");
                var dailyVaccinationsRawIdx = csv.GetOrdinal("daily_vaccinations_raw");
                var dailyVaccinationsIdx = csv.GetOrdinal("daily_vaccinations");
                var totalVaccinationsPerHundredIdx = csv.GetOrdinal("total_vaccinations_per_hundred");
                var peopleVaccinatedPerHundredIdx = csv.GetOrdinal("people_vaccinated_per_hundred");
                var peopleFullyVaccinatedPerHundredIdx = csv.GetOrdinal("people_fully_vaccinated_per_hundred");
                var totalBoostersPerHundredIdx = csv.GetOrdinal("total_boosters_per_hundred");
                var dailyVaccinationsPerMillionIdx = csv.GetOrdinal("daily_vaccinations_per_million");
                var dailyPeopleVaccinatedIdx = csv.GetOrdinal("daily_people_vaccinated");
                var dailyPeopleVaccinatedPerHundredIdx = csv.GetOrdinal("daily_people_vaccinated_per_hundred");
                while (await csv.ReadAsync())
                {
                    var locationValue = csv.GetString(locationIdx);
                    var isoCodeValue = csv.GetString(isoCodeIdx);
                    var dateValue = csv.GetDateTime(dateIdx);
                    var totalVaccinationsValue = csv.GetString(totalVaccinationsIdx);
                    var peopleVaccinatedValue = csv.GetString(peopleVaccinatedIdx);
                    var peopleFullyVaccinatedValue = csv.GetString(peopleFullyVaccinatedIdx);
                    var totalBoostersValue = csv.GetString(totalBoostersIdx);
                    var dailyVaccinationsRawValue = csv.GetString(dailyVaccinationsRawIdx);
                    var dailyVaccinationsValue = csv.GetString(dailyVaccinationsIdx);
                    var totalVaccinationsPerHundredValue = csv.GetString(totalVaccinationsPerHundredIdx);
                    var peopleVaccinatedPerHundredValue = csv.GetString(peopleVaccinatedPerHundredIdx);
                    var peopleFullyVaccinatedPerHundredValue = csv.GetString(peopleFullyVaccinatedPerHundredIdx);
                    var totalBoostersPerHundredValue = csv.GetString(totalBoostersPerHundredIdx);
                    var dailyVaccinationsPerMillionValue = csv.GetString(dailyVaccinationsPerMillionIdx);
                    var dailyPeopleVaccinatedValue = csv.GetString(dailyPeopleVaccinatedIdx);
                    var dailyPeopleVaccinatedPerHundredValue = csv.GetString(dailyPeopleVaccinatedPerHundredIdx);

                    _vaccinations.Add(new Vaccination()
                    {
                        Location = locationValue,
                        IsoCode = isoCodeValue,
                        Date = dateValue,
                        TotalVaccinations = string.IsNullOrEmpty(totalVaccinationsValue) ? null : decimal.Parse(totalVaccinationsValue),
                        PeopleVaccinated = string.IsNullOrEmpty(peopleVaccinatedValue) ? null : decimal.Parse(peopleVaccinatedValue),
                        PeopleFullyVaccinated = string.IsNullOrEmpty(peopleFullyVaccinatedValue) ? null : decimal.Parse(peopleFullyVaccinatedValue),
                        TotalBoosters = string.IsNullOrEmpty(totalBoostersValue) ? null : decimal.Parse(totalBoostersValue),
                        DailyVaccinationsRaw = string.IsNullOrEmpty(dailyVaccinationsRawValue) ? null : decimal.Parse(dailyVaccinationsRawValue),
                        DailyVaccinations = string.IsNullOrEmpty(dailyVaccinationsValue) ? null : decimal.Parse(dailyVaccinationsValue),
                        TotalVaccinationsPerHundred = string.IsNullOrEmpty(totalVaccinationsPerHundredValue) ? null : decimal.Parse(totalVaccinationsPerHundredValue),
                        PeopleVaccinatedPerHundrer = string.IsNullOrEmpty(peopleVaccinatedPerHundredValue) ? null : decimal.Parse(peopleVaccinatedPerHundredValue),
                        PeopleFullyVaccinatedPerHundrer = string.IsNullOrEmpty(peopleFullyVaccinatedPerHundredValue) ? null : decimal.Parse(peopleFullyVaccinatedPerHundredValue),
                        TotalBoostersPerHundred = string.IsNullOrEmpty(totalBoostersPerHundredValue) ? null : decimal.Parse(totalBoostersPerHundredValue),
                        DailyVaccinationsPerMillion = string.IsNullOrEmpty(dailyVaccinationsPerMillionValue) ? null : decimal.Parse(dailyVaccinationsPerMillionValue),
                        DailyPeopleVaccinated = string.IsNullOrEmpty(dailyPeopleVaccinatedValue) ? null : decimal.Parse(dailyPeopleVaccinatedValue),
                        DailyPeopleVaccinatedPerHundred = string.IsNullOrEmpty(dailyPeopleVaccinatedPerHundredValue) ? null : decimal.Parse(dailyPeopleVaccinatedPerHundredValue)
                    });
                }
            }

            File.Delete(Constants.VACCINATIONS_FILE_PATH);

            return _vaccinations;
        }

        public async Task<ICollection<VaersVaxAdverseEvent>> VaersVaxAdverseEventsDataFromStringAsync(string csvAsString)
        {
            var _vaersData = new List<VaersVaxAdverseEvent>();

            await File.WriteAllTextAsync(Constants.VAERS_VAX_AE_WITH_AGE_FILE_PATH, csvAsString);

            using (var textReader = new StreamReader(Constants.VAERS_VAX_AE_WITH_AGE_FILE_PATH))
            {
                using var csv = await CsvDataReader.CreateAsync(textReader);
                var vaccineTypeIdx = csv.GetOrdinal("Vaccine Type");
                var vaccineTypeCodeIdx = csv.GetOrdinal("Vaccine Type Code");
                var vaccineDoseIdx = csv.GetOrdinal("Vaccine Dose");
                var vaccineDoseCodeIdx = csv.GetOrdinal("Vaccine Dose Code");
                var vaccineManufacturerIdx = csv.GetOrdinal("Vaccine Manufacturer");
                var vaccineManufacturerCodeIdx = csv.GetOrdinal("Vaccine Manufacturer Code");
                var eventCategoryIdx = csv.GetOrdinal("Event Category");
                var eventCategoryCodeIdx = csv.GetOrdinal("Event Category Code");
                var ageIdx = csv.GetOrdinal("Age");
                var ageCodeIdx = csv.GetOrdinal("Age Code");
                var eventsReportedIdx = csv.GetOrdinal("Events Reported");
                var percentIdx = csv.GetOrdinal("Percent");

                while (await csv.ReadAsync())
                {
                    var vaccineTypeValue = csv.GetString(vaccineTypeIdx);
                    var vaccineTypeCodeValue = csv.GetString(vaccineTypeCodeIdx);
                    var vaccineDoseValue = csv.GetString(vaccineDoseIdx);
                    var vaccineDoseCodeValue = csv.GetString(vaccineDoseCodeIdx);
                    var vaccineManufacturerValue = csv.GetString(vaccineManufacturerIdx);
                    var vaccineManufacturerCodeValue = csv.GetString(vaccineManufacturerCodeIdx);
                    var eventCategoryValue = csv.GetString(eventCategoryIdx);
                    var eventCategoryCodeValue = csv.GetString(eventCategoryCodeIdx);
                    var ageValue = csv.GetString(ageIdx);
                    var ageCodeValue = csv.GetString(ageCodeIdx);
                    var eventsReportedValue = csv.GetString(eventsReportedIdx);
                    var percentValue = csv.GetString(percentIdx);

                    _vaersData.Add(new VaersVaxAdverseEvent()
                    {
                        Age = string.IsNullOrWhiteSpace(ageValue) ? null : ageValue,
                        AgeCode = string.IsNullOrWhiteSpace(ageCodeValue) ? null : ageCodeValue,
                        EventCategory = string.IsNullOrWhiteSpace(eventCategoryValue) ? null : eventCategoryValue,
                        EventCategoryCode = string.IsNullOrWhiteSpace(eventCategoryCodeValue) ? null : eventCategoryCodeValue,
                        EventsReported = string.IsNullOrWhiteSpace(eventsReportedValue) ? null : decimal.Parse(eventsReportedValue),
                        VaccineDose = string.IsNullOrWhiteSpace(vaccineDoseValue) ? null : vaccineDoseValue,
                        VaccineDoseCode = string.IsNullOrWhiteSpace(vaccineDoseCodeValue) ? null : vaccineDoseCodeValue,
                        VaccineManufacturer = string.IsNullOrWhiteSpace(vaccineManufacturerValue) ? null : vaccineManufacturerValue,
                        VaccineManufacturerCode = string.IsNullOrWhiteSpace(vaccineManufacturerCodeValue) ? null : vaccineManufacturerCodeValue,
                        Percent = string.IsNullOrWhiteSpace(percentValue) ? null : percentValue,
                        VaccineType = string.IsNullOrWhiteSpace(vaccineTypeValue) ? null : vaccineTypeValue,
                        VaccineTypeCode = string.IsNullOrWhiteSpace(vaccineTypeCodeValue) ? null : vaccineTypeCodeValue
                    });
                }
            }

            File.Delete(Constants.VAERS_VAX_AE_WITH_AGE_FILE_PATH);
            
            return _vaersData;
        }
    }
}
