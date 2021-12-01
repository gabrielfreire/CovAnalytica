using CovAnalytica.Shared.Extensions;
using CovAnalytica.Shared.Interfaces;
using CovAnalytica.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CovAnalytica.Server.Data
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private ApplicationDbContext _dbContext;
        
        private IMemoryStorage<CompleteCovidData> _timeseriesCovidDataMemoryStorage;
        private IMemoryStorage<SelectionCovidData> _totalsPerCountryCovidDataMemoryStorage;

        private ICollection<CompleteCovidData> _scopedTimeseriesCache;
        private ICollection<SelectionCovidData> _scopedTotalsPerCountryCache;

        public DatabaseRepository(
            ApplicationDbContext dbContext,
            IMemoryStorage<CompleteCovidData> completeCovidDataMemoryStorage, 
            IMemoryStorage<SelectionCovidData> selectionCovidDataMemoryStorage)
        {
            _dbContext = dbContext;
            _timeseriesCovidDataMemoryStorage = completeCovidDataMemoryStorage;
            _totalsPerCountryCovidDataMemoryStorage = selectionCovidDataMemoryStorage;
        }

        public async Task SaveRangeAsync(ICollection<CompleteCovidData> entities)
        {
            await DeleteAllCompleteCovidDataAsync();

            // set new update marker
            await SaveUpdateMarker(new UpdateMetadata() { LastUpdated = DateTime.UtcNow });
            
            _scopedTimeseriesCache = entities;
            _scopedTotalsPerCountryCache = entities.ToSelectionCovidData();
            
            // save all data
            await _dbContext.CompleteCovidDataItems.AddRangeAsync(_scopedTimeseriesCache);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
            await _dbContext.SelectionCovidDataItems.AddRangeAsync(_scopedTotalsPerCountryCache);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAllCompleteCovidDataAsync()
        {
            _dbContext.RemoveRange(_dbContext.CompleteCovidDataItems);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAllSelectionCovidDataAsync()
        {
            _dbContext.RemoveRange(_dbContext.SelectionCovidDataItems);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAllUpdateMarkersAsync()
        {
            _dbContext.RemoveRange(_dbContext.UpdateMetadataItems);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task FillMemory()
        {
            if (_dbContext.CompleteCovidDataItems.Any())
            {
                var _completeData = _scopedTimeseriesCache != null ? _scopedTimeseriesCache : await _dbContext.CompleteCovidDataItems.ToListAsync();
                await _timeseriesCovidDataMemoryStorage.Save(_completeData);

                var _selectionDataQueryable = _scopedTotalsPerCountryCache != null ? _scopedTotalsPerCountryCache : _completeData.ToSelectionCovidData();
                await _totalsPerCountryCovidDataMemoryStorage.Save(_selectionDataQueryable.ToList());
            }
        }

        public async Task<bool> IsDataAvailableInMemory()
        {
            return (await _timeseriesCovidDataMemoryStorage.HasDataBeenLoaded()) && 
                (await _totalsPerCountryCovidDataMemoryStorage.HasDataBeenLoaded());
        }

        public async Task<bool> IsItTimeToUpdate()
        {
            var _lastUpdateMetadata = _dbContext.UpdateMetadataItems.OrderByDescending(um => um.LastUpdated).FirstOrDefault();

            if (_lastUpdateMetadata != null)
            {
                var _elapsed = DateTime.UtcNow - _lastUpdateMetadata.LastUpdated;
                if (_elapsed < TimeSpan.FromHours(24))
                {
                    return false;
                }

            }
            return true;
        }

        public async Task SaveUpdateMarker(UpdateMetadata updateMetadata)
        {
            await _dbContext.UpdateMetadataItems.AddAsync(updateMetadata);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}
