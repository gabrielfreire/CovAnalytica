using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Client.Components
{
    public partial class DashboardGrid : ComponentBase
    {
        [Parameter]
        public RenderFragment CellContent { get; set; }
        [Parameter]
        public string Style { get; set; } = "";
    }
}
