using System;
using System.Collections.Generic;
using System.Data;

namespace Canducci.QueryFilters.ADO
{

    public static class QueryFilterAdoExtensions
    {
        public static QueryFilterResult Compile(this QueryFilter filter, IDbCommand command)
        {
            return Compile(filter, CompileType.ADO, command);
        }

        public static QueryFilterResult Compile(this QueryFilter filter, CompileType compileType, IDbCommand command)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (!Enum.IsDefined(typeof(CompileType), compileType))
            {
                throw new System.ComponentModel.InvalidEnumArgumentException(nameof(compileType), (int)compileType, typeof(CompileType));
            }

            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var result = filter.Compile();

            List<IDbDataParameter> parameters = new List<IDbDataParameter>();

            foreach (var item in result.Parameters)
            {
                IDbDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = item.Key;
                parameter.Value = item.Value ?? DBNull.Value;
                parameters.Add(parameter);
            }

            return QueryFilterResult.Create(result.Sql, parameters);
        }
    }
}
