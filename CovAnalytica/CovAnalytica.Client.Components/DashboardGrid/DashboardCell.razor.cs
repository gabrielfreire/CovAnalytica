using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Client.Components
{
    public partial class DashboardCell : ComponentBase
    {
        private CellType _cellType = CellType.OneThirdScreen;
        [Parameter]
        public CellType CellType 
        { 
            get => _cellType; 
            set
            {
                if (value != _cellType)
                {
                    _cellType = value;
                    switch (_cellType)
                    {
                        case CellType.FullScreen:
                            _xs = 12;
                            _sm = 12;
                            _md = 12;
                            break;
                        case CellType.HalfScreen:
                            _xs = 12;
                            _sm = 12;
                            _md = 6;
                            break;
                        case CellType.OneThirdScreen:
                            _xs = 12;
                            _sm = 6;
                            _md = 4;
                            break;
                    }
                }
            } 
        }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public int Elevation { get; set; } = 1;
        [Parameter]
        public string Class { get; set; } = "pa-4";
        [Parameter]
        public string Style { get; set; } = "";

        private int _xs = 12;
        private int _sm = 6;
        private int _md = 4;
    }
}
