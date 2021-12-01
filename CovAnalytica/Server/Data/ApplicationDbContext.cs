using CovAnalytica.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CovAnalytica.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Timeseries data
        public DbSet<CompleteCovidData> CompleteCovidDataItems { get; set; }
        // Totals per country data
        public DbSet<SelectionCovidData> SelectionCovidDataItems { get; set; }
        public DbSet<UpdateMetadata> UpdateMetadataItems { get; set; }
    }
}
