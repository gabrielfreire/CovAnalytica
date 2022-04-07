using CovAnalytica.Shared.Extensions;
using CovAnalytica.Shared.Models;
using CovAnalytica.Shared.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Client.Components
{
    public partial class TimeseriesLinechart : ComponentBase
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

        [Parameter]
        public string Title { get; set; } = "Default Title";
        [Parameter]
        public string YAxisLabel { get; set; } = "Default Y Axis label";

        public Task<Dataset> BuildDataset(List<CompleteCovidData> covidDataList, string filterPropertyName, string color = null)
        {
            if (typeof(CompleteCovidData).GetProperty(filterPropertyName) == null)
                throw new InvalidOperationException($"Property {filterPropertyName} in {nameof(CompleteCovidData)} type was not found");
            var _type = typeof(CompleteCovidData);
            var _dataset = new Dataset();
            _dataset.Items = covidDataList.Where(it => 
                !Object.Equals(it.GetType().GetProperty(filterPropertyName).GetValue(it), null) && 
                ((decimal)it.GetType().GetProperty(filterPropertyName).GetValue(it)) > 0)
                .Select(it => new DataItem() { Date = it.Date, Value = decimal.Round((decimal)it.GetType().GetProperty(filterPropertyName).GetValue(it)) }).ToArray();
            _dataset.Title = covidDataList.First().Location;
            _dataset.StrokeWidth = new Random().Next(1, 2);
            _dataset.StrokeColor = color == null ? "".GetRandomHexColor() : color;
            return Task.FromResult(_dataset);
        }
        public void AddDataset(Dataset dataset)
        {
            _ = InvokeAsync(() =>
            {
                addedCountries.Add(dataset.Title);
                chart?.AddDataset(dataset);
                StateHasChanged();
            });
        }
        public void RemoveDataset(string country)
        {
            _ = InvokeAsync(() =>
            {
                chart?.RemoveDataset(country);
                addedCountries.Remove(country);
                StateHasChanged();
            });
        }

    }
}
