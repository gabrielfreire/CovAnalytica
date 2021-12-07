using CovAnalytica.Shared.Interfaces;
using CovAnalytica.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CovAnalytica.Server.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Timeseries data
        public DbSet<CompleteCovidData> CompleteCovidDataItems { get; set; }
        // Totals per country data
        public DbSet<SelectionCovidData> SelectionCovidDataItems { get; set; }
        public DbSet<VaersVaxAdverseEvent> VaersVaxAdverseEventItems { get; set; }
        public DbSet<UpdateMetadata> UpdateMetadataItems { get; set; }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
