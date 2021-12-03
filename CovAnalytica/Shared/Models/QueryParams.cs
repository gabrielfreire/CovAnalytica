using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Models
{
    public class QueryParams
    {
        public int Skip { get; set; } = 0;
        public int Count { get; set; } = 0;
        public Dictionary<string, object> PropertyMap { get; set; } = new Dictionary<string, object>();
        public List<string> SelectList { get; set; } = new List<string>();
        
        public static QueryParams FromQueries(IReadOnlyList<Tuple<string, string>> queries)
        {
            var _queryParams = new QueryParams();
            foreach (var query in queries)
            {
                var prop = query.Item1.ToLower();
                switch(prop)
                {
                    case "skip":
                        _queryParams.Skip = int.Parse(query.Item2);
                        break;
                    case "count":
                        _queryParams.Count = int.Parse(query.Item2);
                        break;
                    case "selects":
                        if (!string.IsNullOrWhiteSpace(query.Item2))
                        {
                            var _columns = query.Item2.Split(',');
                            if (_columns.Length > 0)
                            {
                                _queryParams.SelectList.AddRange(_columns.ToList());
                            }
                        }
                        break;
                    default:
                        _queryParams.PropertyMap.Add(query.Item1, query.Item2);
                        break;
                }
            }

            return _queryParams;
        }

        public List<PropertyInformation> GetPropertiesInformation()
        {
            var _propsInfo = new List<PropertyInformation>();
            foreach(var property in PropertyMap)
            {
                _propsInfo.Add(new PropertyInformation(property.Key, property.Value));
            }
            return _propsInfo;
        }
    }
}
