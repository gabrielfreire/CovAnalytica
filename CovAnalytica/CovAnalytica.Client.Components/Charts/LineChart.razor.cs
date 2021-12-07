using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Client.Components
{
    public partial class LineChart : ComponentBase
    {
        private List<Dataset> _datasets = new List<Dataset>();
        [Parameter]
        public Func<Task<List<Dataset>>> GetDatasets { get; set; }
        [Parameter]
        public Func<object, string> ValueFormatter { get; set; }
        [Parameter]
        public string YAxisLabel { get; set; } = "Values";
        [Parameter]
        public string CategoryAxisFormatString { get; set; } = "{0:MMM}-{0:yyyy}";
        [Parameter]
        public int CategoryAxisPadding { get; set; } = 20;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (GetDatasets != null)
                {
                    _datasets = await GetDatasets.Invoke();
                }

                if (_datasets == null || _datasets.Count == 0)
                {
                    // add default datasets for dev
                    _datasets.Add(new Dataset() { Items = revenue2019, Title = "2019" });
                    _datasets.Add(new Dataset() { Items = revenue2020, Title = "2020" });
                }

                StateHasChanged();
            }
            await base.OnAfterRenderAsync(firstRender);
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
