using CovAnalytica.Shared;
using CovAnalytica.Shared.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
    using static Tests;
    public class CSVServiceTests
    {
        private IGithubService? _githubService;
        private ICSVService? _csvService;

        [OneTimeSetUp]
        public void Setup()
        {
            _githubService = _scopeFactory?.CreateScope().ServiceProvider.GetService<IGithubService>();
            _csvService = _scopeFactory?.CreateScope().ServiceProvider.GetService<ICSVService>();
        }

        [Test]
        public async Task Should_Create_VaccinationLocationCollection_FromCsvString()
        {
            if (_githubService == null) return;
            if (_csvService == null) return;
            var _vaccLocationAsString = await _githubService.GetFileAsStringAsync(Constants.VACCINATION_LOCATIONS_URL_PATH);

            var _vaccinationLocations = await _csvService.LocationsFromStringAsync(_vaccLocationAsString);
            _vaccinationLocations.Should().NotBeEmpty();
        }
        [Test]
        public async Task Should_Create_VaccinationsCollection_FromCsvString()
        {
            if (_githubService == null) return;
            if (_csvService == null) return;
            var _vaccsAsString = await _githubService.GetFileAsStringAsync(Constants.VACCINATIONS_URL_PATH);

            var _vaccinations = await _csvService.VaccinationsFromStringAsync(_vaccsAsString);
            _vaccinations.Should().NotBeEmpty();
        }
        [Test]
        public async Task Should_Create_CompleteCovidDataCollection_FromCsvString()
        {
            if (_githubService == null) return;
            if (_csvService == null) return;
            var _completeDataListAsString = await _githubService.GetFileAsStringAsync(Constants.COMPLETE_COVID_DATA_URL_PATH);

            var _completeDataList = await _csvService.CompleteCovidDataFromStringAsync(_completeDataListAsString);
            _completeDataList.Should().NotBeEmpty();
        }
        [Test]
        public async Task Should_Create_VaersVaxAEDataCollection_FromCsvString()
        {
            if (_githubService == null) return;
            if (_csvService == null) return;
            var _vaersVaxAeString = await _githubService.GetFileAsStringAsync(Constants.VAERS_VAX_AE_WITH_AGE_URL_PATH);

            var _vaersVaxAE = await _csvService.VaersVaxAdverseEventsDataFromStringAsync(_vaersVaxAeString);
            _vaersVaxAE.Should().NotBeEmpty();
        }
    }
}
