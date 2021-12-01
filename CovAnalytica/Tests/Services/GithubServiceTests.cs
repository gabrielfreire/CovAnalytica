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
    public class GithubServiceTests
    {
        private IGithubService? _githubService;
        [OneTimeSetUp]
        public void Setup()
        {
            _githubService = _scopeFactory?.CreateScope().ServiceProvider.GetService<IGithubService>();
        }

        [Test]
        public async Task Should_FetchFileFromGithubAsString_FullUrl()
        {
            if (_githubService == null) return;
            var _fileAsString = await _githubService.GetFileAsStringAsync(Constants.VACCINATION_LOCATIONS_URL);

            _fileAsString.Should().NotBeEmpty();
            _fileAsString.Should().Contain("location,iso_code,vaccines");
        }
        [Test]
        public async Task Should_FetchFileFromGithubAsString_Path()
        {
            if (_githubService == null) return;
            var _fileAsString = await _githubService.GetFileAsStringAsync("owid/covid-19-data/master/public/data/vaccinations/locations.csv");

            _fileAsString.Should().NotBeEmpty();
            _fileAsString.Should().Contain("location,iso_code,vaccines");
        }
        [Test]
        public void Should_FetchFileFromGithubAsString_EmptyUrl()
        {
            if (_githubService == null) return;
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _githubService.GetFileAsStringAsync(" "))?
                .Message.Should().Be("Github URL cannot be empty");
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _githubService.GetFileAsStringAsync(string.Empty))?
                .Message.Should().Be("Github URL cannot be empty");

        }
    }
}
