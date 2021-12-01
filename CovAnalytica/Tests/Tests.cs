
using CovAnalytica.Server.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.IO;

namespace Tests
{
    [SetUpFixture]
    public class Tests
    {
        public static IConfiguration? _configuration;
        public static IServiceCollection? _services;

        public static IServiceScopeFactory? _scopeFactory;

        [OneTimeSetUp]
        public void Setup()
        {
            var confBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = confBuilder.Build();

            _services = new ServiceCollection();
            _services.AddScoped<IConfiguration>(sp => _configuration);
            _services.AddApplicationServices(_configuration);
            _scopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>() ?? null;
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            // do nothing for now
        }
    }
}