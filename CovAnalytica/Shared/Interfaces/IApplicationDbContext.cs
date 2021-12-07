using CovAnalytica.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Interfaces
{
	public interface IApplicationDbContext
	{
        public DbSet<CompleteCovidData> CompleteCovidDataItems { get; set; }
        // Totals per country data
        public DbSet<SelectionCovidData> SelectionCovidDataItems { get; set; }
        public DbSet<VaersVaxAdverseEvent> VaersVaxAdverseEventItems { get; set; }
        public DbSet<UpdateMetadata> UpdateMetadataItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
