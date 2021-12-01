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
        private ICovidDataCSVService _covidDataService;
        private IServiceProvider _serviceProvider;

        public BackgroundTasks(
            IGithubService githubService,
            ICovidDataCSVService covidDataService,
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
                    // get csv string
                    var _csvString = await _githubService.GetFileAsStringAsync(Constants.COMPLETE_COVID_DATA_URL_PATH);

                    // create csv object
                    var _csvObject = await _covidDataService.CompleteCovidDataFromStringAsync(_csvString);

                    // save to DB
                    await _dbRepository.SaveRangeAsync(_csvObject);
                }

                await _dbRepository.FillMemory();

                Console.WriteLine("Memory filled with covid data");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
