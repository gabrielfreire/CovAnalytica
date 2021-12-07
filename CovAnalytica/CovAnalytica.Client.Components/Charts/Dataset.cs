using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Client.Components
{
    public class Dataset
    {
        public DataItem[] Items { get; set; }
        public string Title { get; set; }
        public string StrokeColor { get; set; }
        public int StrokeWidth { get; set; }
    }
}
