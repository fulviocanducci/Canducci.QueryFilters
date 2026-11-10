using Canducci.QueryFilters.Models;
using Canducci.QueryFilters.Operators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Canducci.QueryFilters
{
    public class QueryFilterCompiler
    {
        private readonly HashSet<string> _allowedFields;

        public QueryFilterCompiler()
        {
            _allowedFields = null;
        }

        public QueryFilterCompiler(IEnumerable<string> allowedFields)
        {
            if (allowedFields == null)
            {
                throw new ArgumentNullException(nameof(allowedFields));
            }
            _allowedFields = new HashSet<string>(allowedFields, StringComparer.OrdinalIgnoreCase);
        }

        public QueryFilterResult Compile(QueryFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var parameters = new Dictionary<string, object>();
            var parameterIndex = 0;

            var sql = CompileGroup(filter.Root, parameters, ref parameterIndex);

            return new QueryFilterResult
            {
                Sql = sql,
                Parameters = parameters
            };
        }

        private void ValidateField(string field)
        {
            if (string.IsNullOrWhiteSpace(field))
            {
                throw new ArgumentException("Field cannot be null or empty.", nameof(field));
            }

            if (_allowedFields != null && !_allowedFields.Contains(field))
            {
                throw new ArgumentException($"Field '{field}' is not allowed.", nameof(field));
            }
        }

        private string CompileGroup(FilterGroup group, IDictionary<string, object> parameters, ref int parameterIndex)
        {
            var sql = new StringBuilder();

            for (int i = 0; i < group.Items.Count; i++)
            {
                var item = group.Items[i];

                if (i > 0)
                {
                    sql.Append($" {item.LogicalOperator} ");
                }
                if (item.Condition != null)
                {
                    sql.Append(CompileCondition(item.Condition, parameters, ref parameterIndex));
                }
                else if (item.Group != null)
                {
                    sql.Append("(");
                    sql.Append(CompileGroup(item.Group, parameters, ref parameterIndex));
                    sql.Append(")");
                }
            }

            return sql.ToString();
        }

        private string CompileCondition(FilterCondition condition, IDictionary<string, object> parameters, ref int parameterIndex)
        {
            ValidateField(condition.Field);
            if (condition.Operator == FilterOperator.In || condition.Operator == FilterOperator.NotIn)
            {
                return CompileInCondition(condition, parameters, ref parameterIndex);
            }
            if (condition.Operator == FilterOperator.Between || condition.Operator == FilterOperator.NotBetween)
            {
                return CompileBetweenCondition(condition, parameters, ref parameterIndex);
            }
            if (condition.Operator == FilterOperator.IsNull || condition.Operator == FilterOperator.IsNotNull)
            {
                return $"{condition.Field} {GetSqlOperator(condition.Operator)}";
            }
            var parameterName = $"p{parameterIndex++}";
            parameters.Add(parameterName, GetParameterValue(condition));
            return $"{condition.Field} {GetSqlOperator(condition.Operator)} @{parameterName}";
        }

        private string CompileBetweenCondition(FilterCondition condition, IDictionary<string, object> parameters, ref int parameterIndex)
        {
            if (!(condition.Value is System.Collections.IEnumerable values) || condition.Value is string)
            {
                throw new ArgumentException("Between operator requires an IEnumerable value.", nameof(condition.Value));
            }
            var items = new List<object>();
            foreach (var value in values)
            {
                items.Add(value ?? DBNull.Value);
            }
            if (items.Count != 2)
            {
                throw new ArgumentException("Between operator requires exactly two values.", nameof(condition.Value));
            }
            var firstParameter = $"p{parameterIndex++}";
            var secondParameter = $"p{parameterIndex++}";
            parameters.Add(firstParameter, items[0]);
            parameters.Add(secondParameter, items[1]);
            return $"{condition.Field} {GetSqlOperator(condition.Operator)} @{firstParameter} AND @{secondParameter}";
        }

        private string CompileInCondition(FilterCondition condition, IDictionary<string, object> parameters, ref int parameterIndex)
        {
            if (!(condition.Value is System.Collections.IEnumerable values) || condition.Value is string)
            {
                throw new ArgumentException("IN and NOT IN operators require an IEnumerable value.", nameof(condition.Value));
            }
            var parameterNames = new List<string>();
            foreach (var value in values)
            {
                var parameterName = $"p{parameterIndex++}";
                parameters.Add(parameterName, value ?? DBNull.Value);
                parameterNames.Add($"@{parameterName}");
            }
            if (parameterNames.Count == 0)
            {
                throw new ArgumentException("IN and NOT IN operators require at least one value.", nameof(condition.Value));
            }
            return $"{condition.Field} {GetSqlOperator(condition.Operator)} ({string.Join(", ", parameterNames)})";
        }

        private static string GetSqlOperator(FilterOperator op)
        {
            switch (op)
            {
                case FilterOperator.Equal:
                    {
                        return "=";
                    }
                case FilterOperator.NotEqual:
                    {
                        return "<>";
                    }
                case FilterOperator.Contains:
                case FilterOperator.StartsWith:
                case FilterOperator.EndsWith:
                    {
                        return "LIKE";
                    }
                case FilterOperator.GreaterThan:
                    {
                        return ">";
                    }
                case FilterOperator.GreaterThanOrEqual:
                    {
                        return ">=";
                    }
                case FilterOperator.LessThan:
                    {
                        return "<";
                    }
                case FilterOperator.LessThanOrEqual:
                    {
                        return "<=";
                    }
                case FilterOperator.In:
                    {
                        return "IN";
                    }
                case FilterOperator.NotIn:
                    {
                        return "NOT IN";
                    }
                case FilterOperator.IsNull:
                    {
                        return "IS NULL";
                    }
                case FilterOperator.IsNotNull:
                    {
                        return "IS NOT NULL";
                    }
                case FilterOperator.Between:
                    {
                        return "BETWEEN";
                    }
                case FilterOperator.NotBetween:
                    {
                        return "NOT BETWEEN";
                    }
                case FilterOperator.NotContains:
                case FilterOperator.NotStartsWith:
                case FilterOperator.NotEndsWith:
                    {
                        return "NOT LIKE";
                    }
                default:
                    {
                        throw new NotSupportedException($"Operator '{op}' is not supported.");
                    }
            }
        }

        private static object GetParameterValue(FilterCondition condition)
        {
            switch (condition.Operator)
            {
                case FilterOperator.Contains:
                case FilterOperator.NotContains:
                    {
                        return $"%{condition.Value}%";
                    }
                case FilterOperator.StartsWith:
                case FilterOperator.NotStartsWith:
                    {
                        return $"{condition.Value}%";
                    }
                case FilterOperator.EndsWith:
                case FilterOperator.NotEndsWith:
                    {
                        return $"%{condition.Value}";
                    }
                default:
                    {
                        return condition.Value;
                    }
            }
        }
    }

}
