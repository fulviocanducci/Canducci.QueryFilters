using System.Collections.Generic;

namespace Canducci.QueryFilters.Models
{
    public class QueryFilterResult
    {
        public string Sql { get; set; }

        public IDictionary<string, object> Parameters { get; set; }
    }
}