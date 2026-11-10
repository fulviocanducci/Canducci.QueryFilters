using Dapper;

namespace Canducci.QueryFilters.Dapper
{
    public class QueryFilterResult
    {
        public QueryFilterResult(string sql, DynamicParameters parameters)
        {
            Sql = sql;
            Parameters = parameters;
        }

        public string Sql { get; set; }

        public DynamicParameters Parameters { get; set; }

        public static QueryFilterResult Create(string sql, DynamicParameters parameters)
        {
            return new QueryFilterResult(sql, parameters);
        }
    }
}
