using Dapper;
using System;
using System.Data;

namespace Canducci.QueryFilters.Dapper
{
    public static class QueryFilterResultExtensions
    {
        public static DynamicParameters ToDynamicParameters(this Models.QueryFilterResult result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            DynamicParameters parameters = new DynamicParameters();
            foreach (var parameter in result.Parameters)
            {
                object value = parameter.Value;
                DbType? dbType = Util.GetTypeByValue(value);
                parameters.Add(parameter.Key, value, dbType);
            }
            return parameters;
        }

        public static DynamicParameters ToDynamicParameters(this QueryFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }
            return filter.Compile().ToDynamicParameters();
        }

        public static QueryFilterResult Compile(this Models.QueryFilterResult result)
        {
            return new QueryFilterResult(result.Sql, result.ToDynamicParameters());
        }

        public static QueryFilterResult Compile(this QueryFilter filter, CompileType compileType)
        {
            if (!Enum.IsDefined(typeof(CompileType), compileType))
            {
                throw new System.ComponentModel.InvalidEnumArgumentException(nameof(compileType), (int)compileType, typeof(CompileType));
            }

            var result = filter.Compile();
            return new QueryFilterResult(result.Sql, result.ToDynamicParameters());
        }

    }
}
