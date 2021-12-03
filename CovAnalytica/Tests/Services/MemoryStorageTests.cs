using CovAnalytica.Shared;
using CovAnalytica.Shared.Interfaces;
using CovAnalytica.Shared.Models;
using CovAnalytica.Shared.Extensions;
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
    public class MemoryStorageTests
    {
        private IMemoryStorage<CompleteCovidData>? _timeseriesCovidDataMemoryStorage;
        private IMemoryStorage<SelectionCovidData>? _totalsPerCountryCovidDataMemoryStorage;
        private IGithubService? _githubService;
        private ICSVService? _csvService;

        ICollection<CompleteCovidData>? _timeseriesCovidData;
        ICollection<SelectionCovidData>? _totalsPerCountryCovidData;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _githubService = _scopeFactory?.CreateScope().ServiceProvider.GetService<IGithubService>();
            _csvService = _scopeFactory?.CreateScope().ServiceProvider.GetService<ICSVService>();
            _timeseriesCovidDataMemoryStorage = _scopeFactory?.CreateScope().ServiceProvider.GetService<IMemoryStorage<CompleteCovidData>>();
            _totalsPerCountryCovidDataMemoryStorage = _scopeFactory?.CreateScope().ServiceProvider.GetService<IMemoryStorage<SelectionCovidData>>();
        }

        [Test, Order(1)]
        public async Task Should_Save_TimeseriesData_InMemory()
        {
            if (_timeseriesCovidDataMemoryStorage == null) return; 
            if (_githubService == null) return;
            if (_csvService == null) return;

            var _completeDataListAsString = await _githubService.GetFileAsStringAsync(Constants.COMPLETE_COVID_DATA_URL_PATH);
            _timeseriesCovidData = await _csvService.CompleteCovidDataFromStringAsync(_completeDataListAsString);

            await _timeseriesCovidDataMemoryStorage.Save(_timeseriesCovidData);
            (await _timeseriesCovidDataMemoryStorage.HasDataBeenLoaded()).Should().BeTrue();
        }
        [Test, Order(2)]
        public async Task Should_Save_TotalsPerCountryData_InMemory()
        {
            if (_totalsPerCountryCovidDataMemoryStorage == null) return; 
            if (_timeseriesCovidData == null) return; 
            
            _totalsPerCountryCovidData = _timeseriesCovidData.ToSelectionCovidData();
            await _totalsPerCountryCovidDataMemoryStorage.Save(_totalsPerCountryCovidData);
            (await _totalsPerCountryCovidDataMemoryStorage.HasDataBeenLoaded()).Should().BeTrue();
        }
        [Test, Order(3)]
        public async Task Should_Get_TimeseriesData_FromMemory()
        {
            if (_timeseriesCovidDataMemoryStorage == null) return; 
            if (_timeseriesCovidData == null) return; 
            
            (await _timeseriesCovidDataMemoryStorage.GetAll()).Count.Should().Be(_timeseriesCovidData.Count);
        }
        [Test, Order(3)]
        public async Task Should_Get_TotalsPerCountryData_FromMemory()
        {
            if (_totalsPerCountryCovidDataMemoryStorage == null) return; 
            if (_totalsPerCountryCovidData == null) return; 
            
            (await _totalsPerCountryCovidDataMemoryStorage.GetAll()).Count.Should().Be(_totalsPerCountryCovidData.Count);
        }
    }
}
