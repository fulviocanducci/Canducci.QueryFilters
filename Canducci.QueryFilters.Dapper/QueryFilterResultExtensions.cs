using Canducci.QueryFilters.Models;
using Dapper;
using System;
using System.Data;

namespace Canducci.QueryFilters.Dapper
{
    public static class QueryFilterResultExtensions
    {

        private static DbType? GetDbType(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;

            if (type == typeof(string)) return DbType.String;
            if (type == typeof(int)) return DbType.Int32;
            if (type == typeof(long)) return DbType.Int64;
            if (type == typeof(short)) return DbType.Int16;
            if (type == typeof(byte)) return DbType.Byte;
            if (type == typeof(bool)) return DbType.Boolean;
            if (type == typeof(decimal)) return DbType.Decimal;
            if (type == typeof(double)) return DbType.Double;
            if (type == typeof(float)) return DbType.Single;
            if (type == typeof(DateTime)) return DbType.DateTime;
            if (type == typeof(DateTimeOffset)) return DbType.DateTimeOffset;
            if (type == typeof(Guid)) return DbType.Guid;
            if (type == typeof(byte[])) return DbType.Binary;

            return null;
        }


        public static DynamicParameters ToDynamicParameters(this QueryFilterResult result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            DynamicParameters parameters = new DynamicParameters();
            foreach (var parameter in result.Parameters)
            {
                object value = parameter.Value;
                DbType? dbType = value == null || value == DBNull.Value ? (DbType?)null : GetDbType(value.GetType());
                parameters.Add(parameter.Key, value, dbType);
            }
            return parameters;
        }
    }
}
