using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Models
{
    public class UpdateMetadata
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime LastUpdated { get; set; }
    }
}
