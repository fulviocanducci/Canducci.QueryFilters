using Canducci.QueryFilters.Operators;

namespace Canducci.QueryFilters.Tests
{
    public class QueryFilterTests
    {
        [Fact]
        public void Where_Should_Add_Condition()
        {
            var filter = QueryFilter.Create()
                .Where("Name", FilterOperator.Contains, "Fulvio");

            Assert.Single(filter.Conditions);

            var condition = filter.Conditions[0];

            Assert.Equal("Name", condition.Field);
            Assert.Equal(FilterOperator.Contains, condition.Operator);
            Assert.Equal("Fulvio", condition.Value);
            Assert.Equal("AND", condition.LogicalOperator);
        }

        [Fact]
        public void And_Should_Add_Condition()
        {
            var filter = QueryFilter.Create()
                .Where("Name", FilterOperator.Equal, "Fulvio")
                .And("Active", FilterOperator.Equal, true);

            Assert.Equal(2, filter.Conditions.Count);

            var condition = filter.Conditions[1];

            Assert.Equal("Active", condition.Field);
            Assert.Equal(FilterOperator.Equal, condition.Operator);
            Assert.Equal(true, condition.Value);
            Assert.Equal("AND", condition.LogicalOperator);
        }

        [Fact]
        public void Or_Should_Add_Condition()
        {
            var filter = QueryFilter.Create()
                .Where("Name", FilterOperator.Equal, "Fulvio")
                .Or("Age", FilterOperator.GreaterThan, 18);

            Assert.Equal(2, filter.Conditions.Count);

            var condition = filter.Conditions[1];

            Assert.Equal("Age", condition.Field);
            Assert.Equal(FilterOperator.GreaterThan, condition.Operator);
            Assert.Equal(18, condition.Value);
            Assert.Equal("OR", condition.LogicalOperator);
        }

        [Fact]
        public void Where_And_Or_Should_Preserve_Order()
        {
            var filter = QueryFilter.Create()
                .Where("Name", FilterOperator.Equal, "Fulvio")
                .And("Active", FilterOperator.Equal, true)
                .Or("Age", FilterOperator.GreaterThan, 18);

            Assert.Equal(3, filter.Conditions.Count);

            Assert.Equal("Name", filter.Conditions[0].Field);
            Assert.Equal("Active", filter.Conditions[1].Field);
            Assert.Equal("Age", filter.Conditions[2].Field);

            Assert.Equal("AND", filter.Conditions[0].LogicalOperator);
            Assert.Equal("AND", filter.Conditions[1].LogicalOperator);
            Assert.Equal("OR", filter.Conditions[2].LogicalOperator);
        }
    }
}
