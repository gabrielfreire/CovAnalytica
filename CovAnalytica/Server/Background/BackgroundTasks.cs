using CovAnalytica.Server.Data;
using CovAnalytica.Shared;
using CovAnalytica.Shared.Interfaces;

namespace CovAnalytica.Server.Background
{
    public class BackgroundTasks : IHostedService
    {
        private Timer? _timer;
        private TimeSpan _interval = TimeSpan.FromHours(24);

        private IGithubService _githubService;
        private ICSVService _covidDataService;
        private IServiceProvider _serviceProvider;

        public BackgroundTasks(
            IGithubService githubService,
            ICSVService covidDataService,
            IServiceProvider serviceProvider)
        {
            _githubService = githubService;
            _covidDataService = covidDataService;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Refresh, null, TimeSpan.Zero, _interval);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            _timer?.DisposeAsync();
            return Task.CompletedTask;
        }

        private void Refresh(object? state)
        {
            _ = Task.Run(async () =>
            {
                await AttemptToRefreshCovidData();
            });
        }

        private async Task AttemptToRefreshCovidData()
        {
            using var _scope = _serviceProvider.CreateScope();
            var _dbRepository = _scope.ServiceProvider.GetRequiredService<IDatabaseRepository>();

            // -----------------------------------------------------------------
            // TODO: extract below lines to service in order to properly test it
            // -----------------------------------------------------------------
            try
            {
                if (await _dbRepository.IsItTimeToUpdate())
                {
                    Util.LogInformation("It is time for an update");

                    // get csv string
                    var _completeCovidDataCsvString = await _githubService.GetFileAsStringAsync(Constants.COMPLETE_COVID_DATA_URL_PATH);
                    var _vaersVaxAeCsvString = await _githubService.GetFileAsStringAsync(Constants.VAERS_VAX_AE_WITH_AGE_URL_PATH);
                    Util.LogInformation("Got data from github");

                    // create csv objects
                    var _completeCovidDataCsvObject = await _covidDataService.CompleteCovidDataFromStringAsync(_completeCovidDataCsvString);
                    var _vaersVaxAeCsvObject = await _covidDataService.VaersVaxAdverseEventsDataFromStringAsync(_vaersVaxAeCsvString);
                    Util.LogInformation("Parsed CSV");
                    Util.LogInformation("Starting to save to database");

                    // save to DB
                    await _dbRepository.SaveRangeAsync(_completeCovidDataCsvObject, _vaersVaxAeCsvObject);
                    Console.WriteLine($"{DateTime.UtcNow.ToString("dd/MM/yyyy H:mm:ss")} - [Information][Message 'Data was saved to database']");
                } else Util.LogInformation("It is not time for an update");


                await _dbRepository.FillMemory();

                Util.LogInformation("Memory filled with covid data");
            }
            catch (Exception ex)
            {
                Util.LogException(ex);
            }
        }
    }
}
