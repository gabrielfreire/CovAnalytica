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

        public Dataset BuildDataset(List<CompleteCovidData> covidDataList)
        {
            var _dataset = new Dataset();
            _dataset.Items = covidDataList.Where(it => it.TotalVaccinationsPerHundred != null && it.TotalVaccinationsPerHundred > 0)
                .Select(it => new DataItem() { Date = it.Date, Value = decimal.Round(it.TotalVaccinationsPerHundred ?? 0) }).ToArray();
            _dataset.Title = covidDataList.First().Location;
            _dataset.StrokeWidth = new Random().Next(1, 2);
            _dataset.StrokeColor = "".GetRandomHexColor();
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
