using CovAnalytica.Shared;
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
        private IMemoryStorage<VaersVaxAdverseEvent> _vaersVaxAdverseEventsMemoryStorage;

        private ICollection<CompleteCovidData> _scopedTimeseriesCache;
        private ICollection<SelectionCovidData> _scopedTotalsPerCountryCache;
        private ICollection<VaersVaxAdverseEvent> _scopedVaersVaxAdverseEventsCache;

        public DatabaseRepository(
            ApplicationDbContext dbContext,
            IMemoryStorage<CompleteCovidData> completeCovidDataMemoryStorage,
            IMemoryStorage<SelectionCovidData> selectionCovidDataMemoryStorage, 
            IMemoryStorage<VaersVaxAdverseEvent> vaersVaxAdverseEventsMemoryStorage)
        {
            _dbContext = dbContext;
            _timeseriesCovidDataMemoryStorage = completeCovidDataMemoryStorage;
            _totalsPerCountryCovidDataMemoryStorage = selectionCovidDataMemoryStorage;
            _vaersVaxAdverseEventsMemoryStorage = vaersVaxAdverseEventsMemoryStorage;
        }

        public async Task SaveRangeAsync(
            ICollection<CompleteCovidData> completeCovidDataEntities, 
            ICollection<VaersVaxAdverseEvent> vaersVaxAdverseEventEntities)
        {
            DeleteAllCompleteCovidDataAsync();
            DeleteAllSelectionCovidDataAsync();
            DeleteVaersVaxAEDataAsync();

            // set new update marker
            await SaveUpdateMarker(new UpdateMetadata() { LastUpdated = DateTime.UtcNow });

            await SaveChangesAsync();

            _scopedTimeseriesCache = completeCovidDataEntities;
            _scopedTotalsPerCountryCache = completeCovidDataEntities.ToSelectionCovidData();
            _scopedVaersVaxAdverseEventsCache = vaersVaxAdverseEventEntities;

            // save all data
            await _saveTimeseriesData(_scopedTimeseriesCache);
            await _saveTotalsPerCountryData(_scopedTotalsPerCountryCache);
            await _saveVaersVaxAEData(_scopedVaersVaxAdverseEventsCache);

            await SaveChangesAsync();
        }

        public async Task SaveUpdateMarker(UpdateMetadata updateMetadata) => await _dbContext.UpdateMetadataItems.AddAsync(updateMetadata);

        private Task _saveTimeseriesData(ICollection<CompleteCovidData> entities) => _dbContext.CompleteCovidDataItems.AddRangeAsync(entities);
        private Task _saveTotalsPerCountryData(ICollection<SelectionCovidData> entities) => _dbContext.SelectionCovidDataItems.AddRangeAsync(entities);
        private Task _saveVaersVaxAEData(ICollection<VaersVaxAdverseEvent> entities) => _dbContext.VaersVaxAdverseEventItems.AddRangeAsync(entities);


        public void DeleteAllCompleteCovidDataAsync() => _dbContext.RemoveRange(_dbContext.CompleteCovidDataItems);
        public void DeleteAllSelectionCovidDataAsync() => _dbContext.RemoveRange(_dbContext.SelectionCovidDataItems);
        public void DeleteVaersVaxAEDataAsync() => _dbContext.RemoveRange(_dbContext.VaersVaxAdverseEventItems);
        public void DeleteAllUpdateMarkersAsync() => _dbContext.RemoveRange(_dbContext.UpdateMetadataItems);
        public Task SaveChangesAsync() => _dbContext.SaveChangesAsync(CancellationToken.None);
        

        public async Task FillMemory()
        {
            if (_dbContext.CompleteCovidDataItems.Any())
            {
                var _completeData = _scopedTimeseriesCache != null ? _scopedTimeseriesCache : await _dbContext.CompleteCovidDataItems.ToListAsync();
                await _timeseriesCovidDataMemoryStorage.Save(_completeData);

                var _selectionDataQueryable = _scopedTotalsPerCountryCache != null ? _scopedTotalsPerCountryCache : _completeData.ToSelectionCovidData();
                await _totalsPerCountryCovidDataMemoryStorage.Save(_selectionDataQueryable.ToList());
            }

            if (_dbContext.VaersVaxAdverseEventItems.Any())
            {
                var _vaersVaxAeData = _scopedVaersVaxAdverseEventsCache != null ? _scopedVaersVaxAdverseEventsCache : await _dbContext.VaersVaxAdverseEventItems.ToListAsync();
                await _vaersVaxAdverseEventsMemoryStorage.Save(_vaersVaxAeData);
            }
        }

        public async Task<bool> IsDataAvailableInMemory() => 
                (await _timeseriesCovidDataMemoryStorage.HasDataBeenLoaded()) && 
                (await _totalsPerCountryCovidDataMemoryStorage.HasDataBeenLoaded()) &&
                (await _vaersVaxAdverseEventsMemoryStorage.HasDataBeenLoaded());
        

        public async Task<bool> IsItTimeToUpdate()
        {
            var _lastUpdateMetadata = _dbContext.UpdateMetadataItems.OrderByDescending(um => um.LastUpdated).FirstOrDefault();

            if (_lastUpdateMetadata != null)
            {

                var _elapsed = DateTime.UtcNow - _lastUpdateMetadata.LastUpdated;
                
                Util.LogInformation($"Last updated on {_lastUpdateMetadata.LastUpdated.ToString("dd/MM/yyyy H:mm:ss")} - {_elapsed.Days} days and {_elapsed.Hours} hours ago.");

                if (_elapsed < TimeSpan.FromHours(24))
                {
                    return false;
                }

            }
            return true;
        }

            
    }
}
