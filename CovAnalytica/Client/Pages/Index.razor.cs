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
        [Inject] public SDKService SDKService { get; set; }

        List<Country> allCountries = new List<Country>();
        List<string> addedCountries = new List<string>();
        string selectedCountry = string.Empty;

        bool _busy = false;

        // charts
        TimeseriesLinechart excessMortalityChart;
        TimeseriesLinechart excessMortalityCulPMChart;
        TimeseriesLinechart excessMortalityCulChart;
        TimeseriesLinechart deathChart;
        TimeseriesLinechart totalCasesChart;
        TimeseriesLinechart newVaccChart;
        TimeseriesLinechart peopleVaccChart;

        Dictionary<string, bool> VisibilityMap = new Dictionary<string, bool>()
        {
            { "DeathsPerMillion", true },
            { "TotalCases", true },
            { "NewPeopleVaccinated", true },
            { "PeopleVaccinated", true },
            { "ExcessMortalityCumulativePerMillion", true },
            { "ExcessMortalityCumulative", true },
            { "ExcessMortality", true }
        };

        protected override async Task OnInitializedAsync()
        {
            _busy = true;
            await LoadCountries();
            await base.OnInitializedAsync();
        }

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
			{
                // default dataset
                AddDatasetsAsync("World");
            }
		}

		void AddDatasetsAsync(string country)
        {
            if (string.IsNullOrWhiteSpace(country)) return;

            _busy = true;
            StateHasChanged();

            _ = Task.Run(async () =>
            {

                var c = await SDKService.ListTimeseriesByCountry(country, "Location,NewDeaths,NewCases,NewPeopleVaccinatedSmoothed,PeopleVaccinated,ExcessMortalityCumulativePerMillion,ExcessMortalityCumulativeAbsolute,ExcessMortality,Date");
                
                if (c == null) return;
                if (c.Count == 0) return;

                var _deathPMDataset = await deathChart?.BuildDataset(c, "NewDeaths");
                var _totalCasesDataset = await totalCasesChart?.BuildDataset(c, "NewCases", _deathPMDataset?.StrokeColor);
                var _newVaccPMDataset = await newVaccChart?.BuildDataset(c, "NewPeopleVaccinatedSmoothed", _deathPMDataset?.StrokeColor);
                var _peopleVaccPHDataset = await peopleVaccChart?.BuildDataset(c, "PeopleVaccinated", _deathPMDataset?.StrokeColor);
                var _excessMortalityPMDataset = await excessMortalityCulPMChart?.BuildDataset(c, "ExcessMortalityCumulativePerMillion", _deathPMDataset?.StrokeColor);
                var _excessMortalityCulDataset = await excessMortalityCulChart?.BuildDataset(c, "ExcessMortalityCumulativeAbsolute", _deathPMDataset?.StrokeColor);
                var _excessMortalityDataset = await excessMortalityChart?.BuildDataset(c, "ExcessMortality", _deathPMDataset?.StrokeColor);

                if (_deathPMDataset.Items.Length > 0)
                    deathChart?.AddDataset(_deathPMDataset);
               
                if (_totalCasesDataset.Items.Length > 0)
                    totalCasesChart?.AddDataset(_totalCasesDataset);
            
                if (_newVaccPMDataset.Items.Length > 0)
                    newVaccChart?.AddDataset(_newVaccPMDataset);

                if (_excessMortalityPMDataset.Items.Length > 0)
                    excessMortalityCulPMChart?.AddDataset(_excessMortalityPMDataset);

                if (_excessMortalityDataset.Items.Length > 0)
                    excessMortalityChart?.AddDataset(_excessMortalityDataset);

                if (_peopleVaccPHDataset.Items.Length > 0)
                    peopleVaccChart?.AddDataset(_peopleVaccPHDataset);

                if (_excessMortalityCulDataset.Items.Length > 0)
                    excessMortalityCulChart?.AddDataset(_excessMortalityCulDataset);
            
                addedCountries.Add(country);
                selectedCountry = string.Empty;
                
                _busy = false;
                await InvokeAsync(StateHasChanged);
            });
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

            _ = InvokeAsync(() =>
            {
                addedCountries.Remove(chip.Text);
                RemoveDatasets(chip.Text);
            });
        }
        void RemoveDatasets(string country)
        {
            _busy = true;
            StateHasChanged();

            _ = InvokeAsync(() =>
            {

                deathChart?.RemoveDataset(country);
                totalCasesChart?.RemoveDataset(country);
                newVaccChart?.RemoveDataset(country);
                excessMortalityCulPMChart?.RemoveDataset(country);
                peopleVaccChart?.RemoveDataset(country);
                excessMortalityCulChart?.RemoveDataset(country);
                excessMortalityChart?.RemoveDataset(country);

                _busy = false;
                StateHasChanged();
            });
        }

    }
}
