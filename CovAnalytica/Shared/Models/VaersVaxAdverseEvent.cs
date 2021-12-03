using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Models
{
    public class VaersVaxAdverseEvent
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? VaccineType { get; set; }
        public string? VaccineTypeCode { get; set; }
        public string? VaccineDose { get; set; }
        public string? VaccineDoseCode { get; set; }
        public string? VaccineManufacturer { get; set; }
        public string? VaccineManufacturerCode { get; set; }
        public string? EventCategory { get; set; }
        public string? EventCategoryCode { get; set; }
        public string? Age { get; set; }
        public string? AgeCode { get; set; }
        public decimal? EventsReported { get; set; }
        public string? Percent { get; set; }
    }
}
