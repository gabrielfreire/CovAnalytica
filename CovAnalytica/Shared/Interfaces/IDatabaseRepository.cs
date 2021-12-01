using CovAnalytica.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Interfaces
{
    public interface IDatabaseRepository
    {
        /// <summary>
        /// Saves the covid data
        /// </summary>
        /// <param name="entities">Collection of complete covid data</param>
        /// <returns></returns>
        Task SaveRangeAsync(ICollection<CompleteCovidData> entities);
        Task DeleteAllSelectionCovidDataAsync();
        Task DeleteAllCompleteCovidDataAsync();
        Task DeleteAllUpdateMarkersAsync();
        Task SaveUpdateMarker(UpdateMetadata updateMetadata);
        Task<bool> IsItTimeToUpdate();
        Task<bool> IsDataAvailableInMemory();
        Task FillMemory();
    }
}
