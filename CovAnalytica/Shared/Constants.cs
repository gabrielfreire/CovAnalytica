using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared
{
    public class Constants
    {
        // GITHUB PATHS
        public static string VACCINATION_LOCATIONS_URL = "https://raw.githubusercontent.com/owid/covid-19-data/master/public/data/vaccinations/locations.csv";
        public static string VACCINATION_LOCATIONS_URL_PATH = "owid/covid-19-data/master/public/data/vaccinations/locations.csv";
        public static string VACCINATIONS_URL_PATH = "owid/covid-19-data/master/public/data/vaccinations/vaccinations.csv";
        public static string VACCINATIONS_BY_AGE_GROUP_URL_PATH = "owid/covid-19-data/master/public/data/vaccinations/vaccinations-by-age-group.csv";
        public static string VACCINATIONS_BY_MANUFACTURER_URL_PATH = "owid/covid-19-data/master/public/data/vaccinations/vaccinations-by-manufacturer.csv";
        public static string COMPLETE_COVID_DATA_URL_PATH = "owid/covid-19-data/master/public/data/owid-covid-data.csv";
    
        // FILE PATHS
        public static string VACCINATION_LOCATIONS_FILE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "vaccination_locations.csv");
        public static string VACCINATIONS_FILE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "vaccinations.csv");
        public static string COMPLETE_COVID_DATA_FILE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "vaccinations.csv");
    }
}
