using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Interfaces
{
    public interface IGithubService
    {
        /// <summary>
        /// Get file from a github file URL or only the github path
        /// Example urls:
        /// - https://raw.githubusercontent.com/owid/covid-19-data/master/public/data/vaccinations/locations.csv
        /// - owid/covid-19-data/master/public/data/vaccinations/locations.csv
        /// </summary>
        /// <param name="githubFileUrl"></param>
        /// <returns></returns>
        Task<string> GetFileAsStringAsync(string githubFileUrl);
    }
}
