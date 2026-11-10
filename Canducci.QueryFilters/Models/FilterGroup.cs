using Canducci.QueryFilters.Operators;
using System;
using System.Collections.Generic;

namespace Canducci.QueryFilters.Models
{
    public class FilterGroup
    {
        private readonly List<FilterGroupItem> _items;

        public IReadOnlyList<FilterGroupItem> Items => _items;

        public FilterGroup()
        {
            _items = new List<FilterGroupItem>();
        }

        internal FilterGroup Where(string field, FilterOperator op)
        {
            return Where(field, op, null);
        }

        internal FilterGroup And(string field, FilterOperator op)
        {
            return And(field, op, null);
        }

        internal FilterGroup Or(string field, FilterOperator op)
        {
            return Or(field, op, null);
        }

        internal FilterGroup Where(string field, FilterOperator op, object value)
        {
            return AddCondition(field, op, value, "AND");
        }

        internal FilterGroup And(string field, FilterOperator op, object value)
        {
            return AddCondition(field, op, value, "AND");
        }

        internal FilterGroup Or(string field, FilterOperator op, object value)
        {
            return AddCondition(field, op, value, "OR");
        }

        public FilterGroup Group(Action<FilterGroup> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var group = new FilterGroup();

            configure(group);

            _items.Add(new FilterGroupItem
            {
                Group = group,
                LogicalOperator = "AND"
            });

            return this;
        }

        private FilterGroup AddCondition(string field, FilterOperator op, object value, string logicalOperator)
        {
            var condition = new FilterCondition
            {
                Field = field,
                Operator = op,
                Value = value,
                LogicalOperator = logicalOperator
            };

            _items.Add(new FilterGroupItem
            {
                Condition = condition,
                LogicalOperator = logicalOperator
            });

            return this;
        }
    }
}