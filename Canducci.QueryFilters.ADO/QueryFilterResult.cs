using System.Collections.Generic;
using System.Data;

namespace Canducci.QueryFilters.ADO
{
    public class QueryFilterResult
    {
        public QueryFilterResult(string sql, List<IDbDataParameter> parameters)
        {
            Sql = sql;
            Parameters = parameters;
        }

        public string Sql { get; set; }

        public List<IDbDataParameter> Parameters { get; set; }

        public static QueryFilterResult Create(string sql, List<IDbDataParameter> parameters)
        {
            return new QueryFilterResult(sql, parameters);
        }
    }
}
