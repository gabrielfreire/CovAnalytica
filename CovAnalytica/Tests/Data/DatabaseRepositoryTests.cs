using CovAnalytica.Shared;
using CovAnalytica.Shared.Interfaces;
using CovAnalytica.Shared.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Data
{
    using static Tests;
    public class DatabaseRepositoryTests
    {
        private IDatabaseRepository? _dataRepository;
        private IGithubService? _githubService;
        private ICSVService? _csvService;

        [OneTimeSetUp]
        public void Setup()
        {
            _githubService = _scopeFactory?.CreateScope().ServiceProvider.GetService<IGithubService>();
            _csvService = _scopeFactory?.CreateScope().ServiceProvider.GetService<ICSVService>();
            _dataRepository = _scopeFactory?.CreateScope().ServiceProvider.GetService<IDatabaseRepository>();
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            //reset database
            await _dataRepository.DeleteAllCompleteCovidDataAsync();
            await _dataRepository.DeleteAllSelectionCovidDataAsync();
            await _dataRepository.DeleteAllUpdateMarkersAsync();
        }

        [Test, Order(1)]
        public async Task Should_AttemptToSave_Data()
        {
            if (_dataRepository == null) return;
            if (_githubService == null) return;
            if (_csvService == null) return;

            var _completeDataListAsString = await _githubService.GetFileAsStringAsync(Constants.COMPLETE_COVID_DATA_URL_PATH);
            var _vaersVaxAEAsString = await _githubService.GetFileAsStringAsync(Constants.VAERS_VAX_AE_WITH_AGE_URL_PATH);

            var _completeDataList = await _csvService.CompleteCovidDataFromStringAsync(_completeDataListAsString);
            var _vaersVaxAEList = await _csvService.VaersVaxAdverseEventsDataFromStringAsync(_vaersVaxAEAsString);
            
            await _dataRepository.SaveRangeAsync(
                _completeDataList.Where(c => c.PeopleFullyVaccinatedPerHundred != null).Take(5).ToList(), 
                _vaersVaxAEList.Take(5).ToList());

            (await _dataRepository.IsItTimeToUpdate()).Should().BeFalse();

            (await _dataRepository.IsDataAvailableInMemory()).Should().BeFalse();
        }

        [Test, Order(2)]
        public async Task Should_FillData_ToMemory()
        {
            if (_dataRepository == null) return;

            await _dataRepository.FillMemory();

            (await _dataRepository.IsDataAvailableInMemory()).Should().BeTrue();
        }
    }
}
