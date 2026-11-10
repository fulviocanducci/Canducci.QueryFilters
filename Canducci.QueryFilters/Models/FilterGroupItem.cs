namespace Canducci.QueryFilters.Models
{
    public class FilterGroupItem
    {
        public FilterCondition Condition { get; set; }

        public FilterGroup Group { get; set; }

        public string LogicalOperator { get; set; }
    }
}