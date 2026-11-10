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

            Assert.Single(filter.Root.Items);

            var condition = filter.Root.Items[0];

            Assert.Equal("Name", condition.Condition.Field);
            Assert.Equal(FilterOperator.Contains, condition.Condition.Operator);
            Assert.Equal("Fulvio", condition.Condition.Value);
            Assert.Equal("AND", condition.LogicalOperator);
        }

        [Fact]
        public void And_Should_Add_Condition()
        {
            var filter = QueryFilter.Create()
                .Where("Name", FilterOperator.Equal, "Fulvio")
                .And("Active", FilterOperator.Equal, true);

            Assert.Equal(2, filter.Root.Items.Count);

            var condition = filter.Root.Items[1];

            Assert.Equal("Active", condition.Condition.Field);
            Assert.Equal(FilterOperator.Equal, condition.Condition.Operator);
            Assert.Equal(true, condition.Condition.Value);
            Assert.Equal("AND", condition.LogicalOperator);
        }

        [Fact]
        public void Or_Should_Add_Condition()
        {
            var filter = QueryFilter.Create()
                .Where("Name", FilterOperator.Equal, "Fulvio")
                .Or("Age", FilterOperator.GreaterThan, 18);

            Assert.Equal(2, filter.Root.Items.Count);

            var condition = filter.Root.Items[1];

            Assert.Equal("Age", condition.Condition.Field);
            Assert.Equal(FilterOperator.GreaterThan, condition.Condition.Operator);
            Assert.Equal(18, condition.Condition.Value);
            Assert.Equal("OR", condition.LogicalOperator);
        }

        [Fact]
        public void Where_And_Or_Should_Preserve_Order()
        {
            var filter = QueryFilter.Create()
                .Where("Name", FilterOperator.Equal, "Fulvio")
                .And("Active", FilterOperator.Equal, true)
                .Or("Age", FilterOperator.GreaterThan, 18);

            Assert.Equal(3, filter.Root.Items.Count);

            Assert.Equal("Name", filter.Root.Items[0].Condition.Field);
            Assert.Equal("Active", filter.Root.Items[1].Condition.Field);
            Assert.Equal("Age", filter.Root.Items[2].Condition.Field);

            Assert.Equal("AND", filter.Root.Items[0].LogicalOperator);
            Assert.Equal("AND", filter.Root.Items[1].LogicalOperator);
            Assert.Equal("OR", filter.Root.Items[2].LogicalOperator);
        }

        [Fact]
        public void Compile_Should_Not_Reuse_Parameters_Between_Executions()
        {
            var compiler = new QueryFilterCompiler();

            var first = compiler.Compile(
                QueryFilter.Create()
                    .Where("Name", FilterOperator.Equal, "Fulvio"));

            var second = compiler.Compile(
                QueryFilter.Create()
                    .Where("Age", FilterOperator.GreaterThan, 18));

            Assert.Equal("Name = @p0", first.Sql);
            Assert.Equal("Age > @p0", second.Sql);

            Assert.Single(first.Parameters);
            Assert.Single(second.Parameters);
        }

    }
}
