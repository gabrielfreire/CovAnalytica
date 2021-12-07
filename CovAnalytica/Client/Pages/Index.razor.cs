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
        DeathPerMillionTimeseriesLineChart deathPMChart;
        TotalCasesTimeseriesLineChart totalCasesChart;
        TotalVaccinationsPerHundredLineChart totalVaccChart;

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
            var c = await SDKService.ListTimeseriesByCountry(country, "Location,NewDeathsPerMillion,NewCasesPerMillion,TotalVaccinationsPerHundred,Date");
            if (c == null) return;
            if (c.Count == 0) return;
            var _deathPMDataset = deathPMChart?.BuildDataset(c);
            var _totalCasesDataset = totalCasesChart?.BuildDataset(c);
            var _totalVaccDataset = totalVaccChart?.BuildDataset(c);

            if (_deathPMDataset.Items.Length > 0)
                deathPMChart?.AddDataset(_deathPMDataset);
               
            if (_totalCasesDataset.Items.Length > 0)
                totalCasesChart?.AddDataset(_totalCasesDataset);
            
            if (_totalVaccDataset.Items.Length > 0)
                totalVaccChart?.AddDataset(_totalVaccDataset);
            
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
        }

    }
}
