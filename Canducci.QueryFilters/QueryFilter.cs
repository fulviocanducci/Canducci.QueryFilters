using Canducci.QueryFilters.Models;
using Canducci.QueryFilters.Operators;
using System;

namespace Canducci.QueryFilters
{
    public class QueryFilter
    {
        private readonly FilterGroup _root;

        public FilterGroup Root => _root;

        private QueryFilter()
        {
            _root = new FilterGroup();
        }

        public static QueryFilter Create()
        {
            return new QueryFilter();
        }

        public static QueryFilter Create(Action<FilterGroup> configure)
        {
            return new QueryFilter().Group(configure);
        }

        internal QueryFilter Where(string field, FilterOperator op, object value)
        {
            _root.Where(field, op, value);
            return this;
        }

        internal QueryFilter Where(string field, FilterOperator op)
        {
            return Where(field, op, null);
        }

        internal QueryFilter And(string field, FilterOperator op)
        {
            return And(field, op, null);
        }

        internal QueryFilter Or(string field, FilterOperator op)
        {
            return Or(field, op, null);
        }

        internal QueryFilter And(string field, FilterOperator op, object value)
        {
            _root.And(field, op, value);
            return this;
        }

        internal QueryFilter Or(string field, FilterOperator op, object value)
        {
            _root.Or(field, op, value);
            return this;
        }

        public QueryFilter Group(Action<FilterGroup> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            _root.Group(configure);
            return this;
        }
    }

}
