using CovAnalytica.Client.Components;
using CovAnalytica.Shared.Extensions;
using CovAnalytica.Shared.Models;
using CovAnalytica.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CovAnalytica.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public SDKService SDKService { get; set; }
        List<Country> allCountries = new List<Country>();
        List<string> addedCountries = new List<string>();
        string selectedCountry = string.Empty;

        // charts
        TimeseriesLinechart excessDeathChart;
        TimeseriesLinechart excessDeathPMChart;
        TimeseriesLinechart deathPMChart;
        TimeseriesLinechart totalCasesChart;
        TimeseriesLinechart totalVaccChart;

        Dictionary<string, bool> VisibilityMap = new Dictionary<string, bool>()
        {
            { "DeathPerMillion", true },
            { "TotalCases", true },
            { "TotalVaccinationsPerHundred", true },
            { "ExcessDeathsPerMillion", true },
            { "ExcessMortality", true }
        };

        protected override async Task OnInitializedAsync()
        {
            await LoadCountries();
            await base.OnInitializedAsync();
        }

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
			{
                // default dataset
                await AddDatasetsAsync("World");
			}
		}

		async Task AddDatasetsAsync(string country)
        {
            if (string.IsNullOrWhiteSpace(country)) return;
            var c = await SDKService.ListTimeseriesByCountry(country, "Location,NewDeathsPerMillion,NewCasesPerMillion,NewVaccinationsSmoothedPerMillion,ExcessMortalityCumulativePerMillion,ExcessMortality,Date");
            if (c == null) return;
            if (c.Count == 0) return;
            var _deathPMDataset = deathPMChart?.BuildDataset(c, "NewDeathsPerMillion");
            var _totalCasesDataset = totalCasesChart?.BuildDataset(c, "NewCasesPerMillion", _deathPMDataset?.StrokeColor);
            var _totalVaccDataset = totalVaccChart?.BuildDataset(c, "NewVaccinationsSmoothedPerMillion", _deathPMDataset?.StrokeColor);
            var _excessDeathPMDataset = excessDeathPMChart?.BuildDataset(c, "ExcessMortalityCumulativePerMillion", _deathPMDataset?.StrokeColor);
            var _excessDeathDataset = excessDeathChart?.BuildDataset(c, "ExcessMortality", _deathPMDataset?.StrokeColor);

            if (_deathPMDataset.Items.Length > 0)
                deathPMChart?.AddDataset(_deathPMDataset);
               
            if (_totalCasesDataset.Items.Length > 0)
                totalCasesChart?.AddDataset(_totalCasesDataset);
            
            if (_totalVaccDataset.Items.Length > 0)
                totalVaccChart?.AddDataset(_totalVaccDataset);

            if (_excessDeathPMDataset.Items.Length > 0)
                excessDeathPMChart?.AddDataset(_excessDeathPMDataset);

            if (_excessDeathDataset.Items.Length > 0)
                excessDeathChart?.AddDataset(_excessDeathDataset);
            
            addedCountries.Add(country);
            selectedCountry = string.Empty;
            StateHasChanged();
        }

        async Task LoadCountries()
        {
            allCountries.AddRange((await SDKService.ListCountries()).OrderBy(c => c.Name));
        }
        private async Task<IEnumerable<string>> SearchAsync(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
            {
                return allCountries.Select(al => al.Name);
            }

            return allCountries
                .Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Name);
        }
        public void OnChipClosed(MudChip chip)
        {
            addedCountries.Remove(chip.Text);
            StateHasChanged();

            _ = InvokeAsync(() =>
            {
                RemoveDatasets(chip.Text);
                StateHasChanged();
            });
        }
        void RemoveDatasets(string country)
        {
            deathPMChart?.RemoveDataset(country);
            totalCasesChart?.RemoveDataset(country);
            totalVaccChart?.RemoveDataset(country);
            excessDeathPMChart?.RemoveDataset(country);
        }

    }
}
