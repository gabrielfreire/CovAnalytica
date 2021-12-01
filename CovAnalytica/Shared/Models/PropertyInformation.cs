using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Models
{
    public class PropertyInformation
    {
        public string Name { get; private set; }
        public object Value { get; private set; }

        public PropertyInformation(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
