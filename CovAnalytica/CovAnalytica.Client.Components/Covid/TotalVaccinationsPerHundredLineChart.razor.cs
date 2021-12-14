using CovAnalytica.Shared.Extensions;
using CovAnalytica.Shared.Models;
using CovAnalytica.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Client.Components
{
	public partial class TotalVaccinationsPerHundredLineChart : ComponentBase
	{
        [Inject]
        public SDKService SDKService { get; set; }
        List<Country> allCountries = new List<Country>();
        List<string> addedCountries = new List<string>();
        string selectedCountry = string.Empty;
        ApexLineChart chart;

        private bool _isVisible = true;
        [Parameter]
        public bool Visible
        {
            get => _isVisible; set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    VisibleChanged.InvokeAsync(_isVisible);
                }
            }
        }
        [Parameter]
        public EventCallback<bool> VisibleChanged { get; set; }

        public Dataset BuildDataset(List<CompleteCovidData> covidDataList, string color = null)
        {
            var _dataset = new Dataset();
            _dataset.Items = covidDataList.Where(it => it.NewVaccinationsSmoothedPerMillion != null && it.NewVaccinationsSmoothedPerMillion > 0)
                .Select(it => new DataItem() { Date = it.Date, Value = decimal.Round(it.NewVaccinationsSmoothedPerMillion ?? 0) }).ToArray();
            _dataset.Title = covidDataList.First().Location;
            _dataset.StrokeWidth = new Random().Next(1, 2);
            _dataset.StrokeColor = color == null ? "".GetRandomHexColor() : color;
            return _dataset;
        }
        public void AddDataset(Dataset dataset)
        {
            addedCountries.Add(dataset.Title);
            chart?.AddDataset(dataset);
            StateHasChanged();
        }
        public void RemoveDataset(string country)
        {
            chart?.RemoveDataset(country);
            addedCountries.Remove(country);
            StateHasChanged();
        }


    }
}
