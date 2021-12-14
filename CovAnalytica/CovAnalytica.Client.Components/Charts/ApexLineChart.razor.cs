using ApexCharts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Client.Components
{
    public partial class ApexLineChart : ComponentBase
    {
        private List<Dataset> _datasets = new List<Dataset>();
        [Parameter]
        public Func<Task<List<Dataset>>> GetDatasets { get; set; }
        [Parameter]
        public string YAxisLabel { get; set; } = "Values";
        [Parameter]
        public string XAxisLabel { get; set; } = "Date";
        [Parameter]
        public string Title { get; set; } = "Title";

        private bool _isVisible = true;
        [Parameter]
        public bool Visible { get => _isVisible; set
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
        public string Subtitle { get; set; } = string.Empty;

        ApexChart<DataItem> _apexChart;
        private ApexChartOptions<DataItem> options = new ApexCharts.ApexChartOptions<DataItem>();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (!string.IsNullOrWhiteSpace(Subtitle))
            {
                options.Subtitle = new Subtitle() { Text = Subtitle };
            }
            options.Xaxis = new XAxis()
            {
                Title = new AxisTitle()
                {
                    Text = XAxisLabel,
                    OffsetY = 5
                }
            };
            //options.Markers = new Markers { Shape = ShapeEnum.Circle, Size = 1 };
            options.Yaxis = new List<YAxis>()
            {
                new YAxis()
                {
                    Title = new AxisTitle() { Text = YAxisLabel }
                }
            };
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            if (firstRender)
            {
                if (GetDatasets != null)
                {
                    _datasets = await GetDatasets.Invoke();
                }

                if (_datasets != null || _datasets.Count > 0)
                {
                    _apexChart?.SetRerenderChart();
                }


                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public void AddDataset(Dataset dataset)
        {
            if (dataset != null && dataset.Items != null && dataset.Items.Length > 0 && !_datasets.Any(d => d.Title == dataset.Title))
            {
                _datasets.Add(dataset);
                _apexChart?.SetRerenderChart();
                StateHasChanged();
            }
        }

        public void RemoveDataset(Dataset dataset)
        {
            if (dataset != null && dataset.Items != null && dataset.Items.Length > 0)
            {
                var _datasetToRemove = _datasets.FirstOrDefault(d => d.Title == dataset.Title);
                if (_datasetToRemove != null)
                {
                    _datasets.Remove(_datasetToRemove);
                    _apexChart?.SetRerenderChart();
                    StateHasChanged();
                }
            }
        }

        public void RemoveDataset(string country)
        {
            var _datasetToRemove = _datasets.FirstOrDefault(d => d.Title == country);
            if (_datasetToRemove != null)
            {
                _datasets.Remove(_datasetToRemove);
                _apexChart?.SetRerenderChart();
                StateHasChanged();
            }
            
        }

        DataItem[] revenue2019 = new DataItem[] {
            new DataItem
            {
                Date = DateTime.Parse("2019-01-01"),
                Value = 234000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-02-01"),
                Value = 269000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-03-01"),
                Value = 233000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-04-01"),
                Value = 244000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-05-01"),
                Value = 214000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-06-01"),
                Value = 253000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-07-01"),
                Value = 274000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-08-01"),
                Value = 284000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-09-01"),
                Value = 273000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-10-01"),
                Value = 282000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-11-01"),
                Value = 289000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-12-01"),
                Value = 294000
            }
        };

        DataItem[] revenue2020 = new DataItem[] {
            new DataItem
            {
                Date = DateTime.Parse("2019-01-01"),
                Value = 334000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-02-01"),
                Value = 369000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-03-01"),
                Value = 333000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-04-01"),
                Value = 344000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-05-01"),
                Value = 314000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-06-01"),
                Value = 353000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-07-01"),
                Value = 374000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-08-01"),
                Value = 384000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-09-01"),
                Value = 373000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-10-01"),
                Value = 382000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-11-01"),
                Value = 389000
            },
            new DataItem
            {
                Date = DateTime.Parse("2019-12-01"),
                Value = 394000
            }
        };
    }
}
