using Canducci.QueryFilters.Models;
using Canducci.QueryFilters.Operators;
using System.Collections.Generic;

namespace Canducci.QueryFilters
{
    public class QueryFilter
    {
        private readonly List<FilterCondition> _conditions;

        public IReadOnlyList<FilterCondition> Conditions => _conditions;

        private QueryFilter()
        {
            _conditions = new List<FilterCondition>();
        }

        public static QueryFilter Create()
        {
            return new QueryFilter();
        }

        public QueryFilter Where(string field, FilterOperator op, object value)
        {
            _conditions.Add(new FilterCondition
            {
                Field = field,
                Operator = op,
                Value = value,
                LogicalOperator = "AND"
            });

            return this;
        }

        public QueryFilter And(string field, FilterOperator op, object value)
        {
            return Where(field, op, value);
        }

        public QueryFilter Or(string field, FilterOperator op, object value)
        {
            _conditions.Add(new FilterCondition
            {
                Field = field,
                Operator = op,
                Value = value,
                LogicalOperator = "OR"
            });

            return this;
        }
    }


}
