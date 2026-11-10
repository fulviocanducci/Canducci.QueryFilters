using Canducci.QueryFilters.Operators;

namespace Canducci.QueryFilters.Tests
{
    public class QueryFilterCompilerTests
    {
        private readonly QueryFilterCompiler _compiler;

        public QueryFilterCompilerTests()
        {
            _compiler = new QueryFilterCompiler();
        }


        [Fact]
        public void Compile_NotStartsWith_Should_Generate_NotLike()
        {
            var filter = QueryFilter.Create()
                .Where("Name", FilterOperator.NotStartsWith, "Fulvio");

            var result = _compiler.Compile(filter);

            Assert.Equal("Name NOT LIKE @p0", result.Sql);
            Assert.Equal("Fulvio%", result.Parameters["p0"]);
        }

        [Fact]
        public void Compile_NotEndsWith_Should_Generate_NotLike()
        {
            var filter = QueryFilter.Create()
                .Where("Name", FilterOperator.NotEndsWith, "Fulvio");

            var result = _compiler.Compile(filter);

            Assert.Equal("Name NOT LIKE @p0", result.Sql);
            Assert.Equal("%Fulvio", result.Parameters["p0"]);
        }

        [Fact]
        public void Compile_In_Should_Generate_Parameters()
        {
            var filter = QueryFilter.Create()
                .Where("Status", FilterOperator.In, new[] { 1, 2, 3 });

            var result = _compiler.Compile(filter);

            Assert.Equal("Status IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(3, result.Parameters.Count);
            Assert.Equal(1, result.Parameters["p0"]);
            Assert.Equal(2, result.Parameters["p1"]);
            Assert.Equal(3, result.Parameters["p2"]);
        }

        [Fact]
        public void Compile_NotContains_Should_Generate_NotLike()
        {
            var filter = QueryFilter.Create()
                .Where("Name", FilterOperator.NotContains, "Fulvio");

            var result = _compiler.Compile(filter);

            Assert.Equal("Name NOT LIKE @p0", result.Sql);
            Assert.Equal("%Fulvio%", result.Parameters["p0"]);
        }

        [Fact]
        public void Compile_NotBetween_Should_Generate_Parameters()
        {
            var filter = QueryFilter.Create()
                .Where("Age", FilterOperator.NotBetween, new[] { 18, 60 });

            var result = _compiler.Compile(filter);

            Assert.Equal("Age NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(2, result.Parameters.Count);
            Assert.Equal(18, result.Parameters["p0"]);
            Assert.Equal(60, result.Parameters["p1"]);
        }

        [Fact]
        public void Compile_NotIn_Should_Generate_Parameters()
        {
            var filter = QueryFilter.Create()
                .Where("Status", FilterOperator.NotIn, new List<int> { 1, 2 });

            var result = _compiler.Compile(filter);

            Assert.Equal("Status NOT IN (@p0, @p1)", result.Sql);
            Assert.Equal(2, result.Parameters.Count);
        }

        [Fact]
        public void Compile_In_With_Empty_Collection_Should_Throw_Exception()
        {
            var filter = QueryFilter.Create()
                .Where("Status", FilterOperator.In, new int[0]);

            Assert.Throws<ArgumentException>(() =>
            {
                _compiler.Compile(filter);
            });
        }

        [Fact]
        public void Compile_In_With_Invalid_Value_Should_Throw_Exception()
        {
            var filter = QueryFilter.Create()
                .Where("Status", FilterOperator.In, "123");

            Assert.Throws<ArgumentException>(() =>
            {
                _compiler.Compile(filter);
            });
        }

        [Fact]
        public void Compile_Between_Should_Generate_Parameters()
        {
            var filter = QueryFilter.Create()
                .Where("Age", FilterOperator.Between, new[] { 18, 60 });

            var result = _compiler.Compile(filter);

            Assert.Equal("Age BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(2, result.Parameters.Count);
            Assert.Equal(18, result.Parameters["p0"]);
            Assert.Equal(60, result.Parameters["p1"]);
        }

        [Fact]
        public void Compile_Between_With_Dates_Should_Generate_Parameters()
        {
            var start = new DateTime(2026, 1, 1);
            var end = new DateTime(2026, 12, 31);

            var filter = QueryFilter.Create()
                .Where("CreatedAt", FilterOperator.Between, new[] { start, end });

            var result = _compiler.Compile(filter);

            Assert.Equal("CreatedAt BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(start, result.Parameters["p0"]);
            Assert.Equal(end, result.Parameters["p1"]);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void Compile_Between_With_Invalid_Quantity_Should_Throw_Exception(int quantity)
        {
            var values = Enumerable.Range(1, quantity).ToArray();

            var filter = QueryFilter.Create()
                .Where("Age", FilterOperator.Between, values);

            Assert.Throws<ArgumentException>(() =>
            {
                _compiler.Compile(filter);
            });
        }

        [Fact]
        public void Compile_Field_Should_Be_Case_Insensitive()
        {
            var compiler = new QueryFilterCompiler(new[]
            {
                "Name"
            });

            var filter = QueryFilter.Create()
                .Where("name", FilterOperator.Equal, "Fulvio");

            var result = compiler.Compile(filter);

            Assert.Equal("name = @p0", result.Sql);
        }


        [Fact]
        public void Compile_Contains_Should_Generate_Like()
        {
            var filter = QueryFilter.Create()
                .Where("Name", FilterOperator.Contains, "Fulvio");

            var result = _compiler.Compile(filter);

            Assert.Equal("Name LIKE @p0", result.Sql);
            Assert.Equal("%Fulvio%", result.Parameters["p0"]);
        }

        [Fact]
        public void Compile_StartsWith_Should_Generate_Like()
        {
            var filter = QueryFilter.Create()
                .Where("Name", FilterOperator.StartsWith, "Fulvio");

            var result = _compiler.Compile(filter);

            Assert.Equal("Name LIKE @p0", result.Sql);
            Assert.Equal("Fulvio%", result.Parameters["p0"]);
        }

        [Fact]
        public void Compile_EndsWith_Should_Generate_Like()
        {
            var filter = QueryFilter.Create()
                .Where("Name", FilterOperator.EndsWith, "Fulvio");

            var result = _compiler.Compile(filter);

            Assert.Equal("Name LIKE @p0", result.Sql);
            Assert.Equal("%Fulvio", result.Parameters["p0"]);
        }

        [Fact]
        public void Compile_Equal_Should_Generate_Parameter()
        {
            var filter = QueryFilter.Create()
                .Where("Active", FilterOperator.Equal, true);

            var result = _compiler.Compile(filter);

            Assert.Equal("Active = @p0", result.Sql);
            Assert.Equal(true, result.Parameters["p0"]);
        }

        [Fact]
        public void Compile_And_Or_Should_Preserve_Order()
        {
            var filter = QueryFilter.Create()
                .Where("Name", FilterOperator.Contains, "Fulvio")
                .And("Active", FilterOperator.Equal, true)
                .Or("Age", FilterOperator.GreaterThan, 18);

            var result = _compiler.Compile(filter);

            Assert.Equal(
                "Name LIKE @p0 AND Active = @p1 OR Age > @p2",
                result.Sql);

            Assert.Equal(3, result.Parameters.Count);
        }

        [Fact]
        public void Compile_Allowed_Field_Should_Succeed()
        {
            var compiler = new QueryFilterCompiler(new[]
            {
                "Name",
                "Age",
                "Active"
            });

            var filter = QueryFilter.Create()
                .Where("Name", FilterOperator.Equal, "Fulvio");

            var result = compiler.Compile(filter);

            Assert.Equal("Name = @p0", result.Sql);
        }

        [Fact]
        public void Compile_IsNull_Should_Generate_IsNull()
        {
            var filter = QueryFilter.Create()
                .Where("Email", FilterOperator.IsNull);

            var result = _compiler.Compile(filter);

            Assert.Equal("Email IS NULL", result.Sql);
            Assert.Empty(result.Parameters);
        }

        [Fact]
        public void Compile_IsNotNull_Should_Generate_IsNotNull()
        {
            var filter = QueryFilter.Create()
                .Where("Email", FilterOperator.IsNotNull);

            var result = _compiler.Compile(filter);

            Assert.Equal("Email IS NOT NULL", result.Sql);
            Assert.Empty(result.Parameters);
        }

        [Fact]
        public void Compile_IsNull_Should_Not_Generate_Parameters()
        {
            var filter = QueryFilter.Create()
                .Where("Email", FilterOperator.IsNull);

            var result = _compiler.Compile(filter);

            Assert.Empty(result.Parameters);
        }

        [Fact]
        public void Compile_IsNotNull_Should_Not_Generate_Parameters()
        {
            var filter = QueryFilter.Create()
                .Where("Email", FilterOperator.IsNotNull);

            var result = _compiler.Compile(filter);

            Assert.Empty(result.Parameters);
        }
    }
}
