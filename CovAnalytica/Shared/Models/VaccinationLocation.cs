using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Models
{
    public class VaccinationLocation
    {
        public string? Location { get; set; }
        public string? IsoCode { get; set; }
        public string? Vaccines { get; set; }
        public DateTime LastObservationDate { get; set; }
        public string? SourceName { get; set; }
        public string? SourceWebsite { get; set; }
    }
}
