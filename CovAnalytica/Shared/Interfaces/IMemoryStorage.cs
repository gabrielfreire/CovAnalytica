using CovAnalytica.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Interfaces
{
    public interface IMemoryStorage<T>
    {
        Task Save(ICollection<T> entities);
        Task<List<T>> GetAll();
        Task<bool> HasDataBeenLoaded();
    }
}
