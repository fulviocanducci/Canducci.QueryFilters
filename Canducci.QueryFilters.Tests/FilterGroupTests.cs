using Canducci.QueryFilters.Operators;

namespace Canducci.QueryFilters.Tests
{
    public class FilterGroupTests
    {
        internal QueryFilterCompiler _compiler;
        public FilterGroupTests()
        {
            _compiler = new QueryFilterCompiler();
        }

        [Fact]
        public void Compile_Group_Should_Generate_Parentheses()
        {
            var filter = QueryFilter.Create()
                .Group(g => g
                    .Where("Name", FilterOperator.Contains, "Fulvio")
                    .Or("Name", FilterOperator.Contains, "Silva")
                )
                .And("Active", FilterOperator.Equal, true);

            var result = _compiler.Compile(filter);

            Assert.Equal(
                "(Name LIKE @p0 OR Name LIKE @p1) AND Active = @p2",
                result.Sql);

            Assert.Equal(3, result.Parameters.Count);
        }

        [Fact]
        public void Compile_Nested_Groups_Should_Generate_Parentheses()
        {
            var filter = QueryFilter.Create()
                .Group(g => g
                    .Where("Name", FilterOperator.Equal, "Fulvio")
                    .Group(nested => nested
                        .Where("Age", FilterOperator.GreaterThan, 18)
                        .Or("Age", FilterOperator.LessThan, 60)
                    )
                );

            var result = _compiler.Compile(filter);

            Assert.Equal(
                "(Name = @p0 AND (Age > @p1 OR Age < @p2))",
                result.Sql);

            Assert.Equal(3, result.Parameters.Count);
        }
        [Fact]
        public void Group_Should_Add_Group()
        {
            var filter = QueryFilter.Create()
                .Group(g => g
                    .Where("Name", FilterOperator.Contains, "Fulvio")
                    .Or("Name", FilterOperator.Contains, "Silva")
                );

            Assert.Single(filter.Root.Items);

            var group = filter.Root.Items[0].Group;

            Assert.NotNull(group);
            Assert.Equal(2, group.Items.Count);
        }

        [Fact]
        public void Group_Should_Preserve_Condition_Order()
        {
            var filter = QueryFilter.Create()
                .Where("Age", FilterOperator.GreaterThan, 18)
                .Group(g => g
                    .Where("Name", FilterOperator.Contains, "Fulvio")
                    .Or("Name", FilterOperator.Contains, "Silva")
                )
                .And("Active", FilterOperator.Equal, true);

            Assert.Equal(3, filter.Root.Items.Count);

            Assert.NotNull(filter.Root.Items[0].Condition);
            Assert.NotNull(filter.Root.Items[1].Group);
            Assert.NotNull(filter.Root.Items[2].Condition);
        }

        [Fact]
        public void Group_Should_Support_Nested_Groups()
        {
            var filter = QueryFilter.Create()
                .Group(g => g
                    .Where("Name", FilterOperator.Contains, "Fulvio")
                    .Group(nested => nested
                        .Where("Age", FilterOperator.GreaterThan, 18)
                        .Or("Age", FilterOperator.LessThan, 60)
                    )
                );

            var group = filter.Root.Items[0].Group;

            Assert.Equal(2, group.Items.Count);
            Assert.NotNull(group.Items[1].Group);
            Assert.Equal(2, group.Items[1].Group.Items.Count);
        }
    }
}
