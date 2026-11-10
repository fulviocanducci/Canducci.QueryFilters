using Canducci.QueryFilters.Operators;

namespace Canducci.QueryFilters.Models
{
    public class FilterCondition
    {
        public string Field { get; set; }
        public FilterOperator Operator { get; set; }
        public object Value { get; set; }
        public string LogicalOperator { get; set; }
    }
}