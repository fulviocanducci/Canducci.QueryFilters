using Canducci.QueryFilters.Operators;
using System.Collections;

namespace Canducci.QueryFilters.Tests
{
    public class QueryFilterExtensionsTests
    {
        #region Comparisons

        [Fact]
        public void Comparisons_ShouldGenerateCorrectConditions()
        {
            AssertCondition(QueryFilter.Create().WhereEquals("Age", 18), "Age", FilterOperator.Equal, "AND", 18);
            AssertCondition(QueryFilter.Create().WhereNotEquals("Age", 18), "Age", FilterOperator.NotEqual, "AND", 18);
            AssertCondition(QueryFilter.Create().WhereGreaterThan("Age", 18), "Age", FilterOperator.GreaterThan, "AND", 18);
            AssertCondition(QueryFilter.Create().WhereGreaterThanOrEqual("Age", 18), "Age", FilterOperator.GreaterThanOrEqual, "AND", 18);
            AssertCondition(QueryFilter.Create().WhereLessThan("Age", 60), "Age", FilterOperator.LessThan, "AND", 60);
            AssertCondition(QueryFilter.Create().WhereLessThanOrEqual("Age", 60), "Age", FilterOperator.LessThanOrEqual, "AND", 60);

            AssertCondition(QueryFilter.Create().AndEquals("Age", 18), "Age", FilterOperator.Equal, "AND", 18);
            AssertCondition(QueryFilter.Create().AndNotEquals("Age", 18), "Age", FilterOperator.NotEqual, "AND", 18);
            AssertCondition(QueryFilter.Create().AndGreaterThan("Age", 18), "Age", FilterOperator.GreaterThan, "AND", 18);
            AssertCondition(QueryFilter.Create().AndGreaterThanOrEqual("Age", 18), "Age", FilterOperator.GreaterThanOrEqual, "AND", 18);
            AssertCondition(QueryFilter.Create().AndLessThan("Age", 60), "Age", FilterOperator.LessThan, "AND", 60);
            AssertCondition(QueryFilter.Create().AndLessThanOrEqual("Age", 60), "Age", FilterOperator.LessThanOrEqual, "AND", 60);

            AssertCondition(QueryFilter.Create().OrEquals("Age", 18), "Age", FilterOperator.Equal, "OR", 18);
            AssertCondition(QueryFilter.Create().OrNotEquals("Age", 18), "Age", FilterOperator.NotEqual, "OR", 18);
            AssertCondition(QueryFilter.Create().OrGreaterThan("Age", 18), "Age", FilterOperator.GreaterThan, "OR", 18);
            AssertCondition(QueryFilter.Create().OrGreaterThanOrEqual("Age", 18), "Age", FilterOperator.GreaterThanOrEqual, "OR", 18);
            AssertCondition(QueryFilter.Create().OrLessThan("Age", 60), "Age", FilterOperator.LessThan, "OR", 60);
            AssertCondition(QueryFilter.Create().OrLessThanOrEqual("Age", 60), "Age", FilterOperator.LessThanOrEqual, "OR", 60);
        }

        #endregion

        #region Null

        [Fact]
        public void Null_ShouldGenerateCorrectConditions()
        {
            AssertCondition(QueryFilter.Create().WhereNull("Name"), "Name", FilterOperator.IsNull, "AND", null);
            AssertCondition(QueryFilter.Create().WhereNotNull("Name"), "Name", FilterOperator.IsNotNull, "AND", null);

            AssertCondition(QueryFilter.Create().AndNull("Name"), "Name", FilterOperator.IsNull, "AND", null);
            AssertCondition(QueryFilter.Create().AndNotNull("Name"), "Name", FilterOperator.IsNotNull, "AND", null);

            AssertCondition(QueryFilter.Create().OrNull("Name"), "Name", FilterOperator.IsNull, "OR", null);
            AssertCondition(QueryFilter.Create().OrNotNull("Name"), "Name", FilterOperator.IsNotNull, "OR", null);
        }

        #endregion

        #region Between

        [Fact]
        public void Between_ShouldGenerateCorrectConditions()
        {
            AssertCondition(QueryFilter.Create().WhereBetween("Age", 10, 20), "Age", FilterOperator.Between, "AND", new object[] { 10, 20 });
            AssertCondition(QueryFilter.Create().WhereNotBetween("Age", 10, 20), "Age", FilterOperator.NotBetween, "AND", new object[] { 10, 20 });

            AssertCondition(QueryFilter.Create().AndBetween("Age", 10, 20), "Age", FilterOperator.Between, "AND", new object[] { 10, 20 });
            AssertCondition(QueryFilter.Create().AndNotBetween("Age", 10, 20), "Age", FilterOperator.NotBetween, "AND", new object[] { 10, 20 });

            AssertCondition(QueryFilter.Create().OrBetween("Age", 10, 20), "Age", FilterOperator.Between, "OR", new object[] { 10, 20 });
            AssertCondition(QueryFilter.Create().OrNotBetween("Age", 10, 20), "Age", FilterOperator.NotBetween, "OR", new object[] { 10, 20 });
        }

        [Fact]
        public void Between_ShouldAcceptDifferentTypes()
        {
            var filter = QueryFilter.Create()
                .WhereBetween("Price", 10, 20.50m);

            AssertCondition(filter, "Price", FilterOperator.Between, "AND", new object[] { 10, 20.50m });
        }

        #endregion

        #region In

        [Fact]
        public void In_ShouldGenerateCorrectConditions()
        {
            AssertCondition(QueryFilter.Create().WhereIn("Status", 1, 2, 3), "Status", FilterOperator.In, "AND", new object[] { 1, 2, 3 });
            AssertCondition(QueryFilter.Create().WhereNotIn("Status", 1, 2, 3), "Status", FilterOperator.NotIn, "AND", new object[] { 1, 2, 3 });

            AssertCondition(QueryFilter.Create().AndIn("Status", 1, 2, 3), "Status", FilterOperator.In, "AND", new object[] { 1, 2, 3 });
            AssertCondition(QueryFilter.Create().AndNotIn("Status", 1, 2, 3), "Status", FilterOperator.NotIn, "AND", new object[] { 1, 2, 3 });

            AssertCondition(QueryFilter.Create().OrIn("Status", 1, 2, 3), "Status", FilterOperator.In, "OR", new object[] { 1, 2, 3 });
            AssertCondition(QueryFilter.Create().OrNotIn("Status", 1, 2, 3), "Status", FilterOperator.NotIn, "OR", new object[] { 1, 2, 3 });
        }

        #endregion

        #region Contains

        [Fact]
        public void Contains_ShouldGenerateCorrectConditions()
        {
            AssertCondition(QueryFilter.Create().WhereContains("Name", "Fulvio"), "Name", FilterOperator.Contains, "AND", "Fulvio");
            AssertCondition(QueryFilter.Create().WhereNotContains("Name", "Fulvio"), "Name", FilterOperator.NotContains, "AND", "Fulvio");

            AssertCondition(QueryFilter.Create().AndContains("Name", "Fulvio"), "Name", FilterOperator.Contains, "AND", "Fulvio");
            AssertCondition(QueryFilter.Create().AndNotContains("Name", "Fulvio"), "Name", FilterOperator.NotContains, "AND", "Fulvio");

            AssertCondition(QueryFilter.Create().OrContains("Name", "Fulvio"), "Name", FilterOperator.Contains, "OR", "Fulvio");
            AssertCondition(QueryFilter.Create().OrNotContains("Name", "Fulvio"), "Name", FilterOperator.NotContains, "OR", "Fulvio");
        }

        #endregion

        #region StartsWith

        [Fact]
        public void StartsWith_ShouldGenerateCorrectConditions()
        {
            AssertCondition(QueryFilter.Create().WhereStartsWith("Name", "Ful"), "Name", FilterOperator.StartsWith, "AND", "Ful");
            AssertCondition(QueryFilter.Create().WhereNotStartsWith("Name", "Ful"), "Name", FilterOperator.NotStartsWith, "AND", "Ful");

            AssertCondition(QueryFilter.Create().AndStartsWith("Name", "Ful"), "Name", FilterOperator.StartsWith, "AND", "Ful");
            AssertCondition(QueryFilter.Create().AndNotStartsWith("Name", "Ful"), "Name", FilterOperator.NotStartsWith, "AND", "Ful");

            AssertCondition(QueryFilter.Create().OrStartsWith("Name", "Ful"), "Name", FilterOperator.StartsWith, "OR", "Ful");
            AssertCondition(QueryFilter.Create().OrNotStartsWith("Name", "Ful"), "Name", FilterOperator.NotStartsWith, "OR", "Ful");
        }

        #endregion

        #region EndsWith

        [Fact]
        public void EndsWith_ShouldGenerateCorrectConditions()
        {
            AssertCondition(QueryFilter.Create().WhereEndsWith("Name", "vio"), "Name", FilterOperator.EndsWith, "AND", "vio");
            AssertCondition(QueryFilter.Create().WhereNotEndsWith("Name", "vio"), "Name", FilterOperator.NotEndsWith, "AND", "vio");

            AssertCondition(QueryFilter.Create().AndEndsWith("Name", "vio"), "Name", FilterOperator.EndsWith, "AND", "vio");
            AssertCondition(QueryFilter.Create().AndNotEndsWith("Name", "vio"), "Name", FilterOperator.NotEndsWith, "AND", "vio");

            AssertCondition(QueryFilter.Create().OrEndsWith("Name", "vio"), "Name", FilterOperator.EndsWith, "OR", "vio");
            AssertCondition(QueryFilter.Create().OrNotEndsWith("Name", "vio"), "Name", FilterOperator.NotEndsWith, "OR", "vio");
        }

        #endregion

        #region Helpers

        private static void AssertCondition(
            QueryFilter filter,
            string expectedField,
            FilterOperator expectedOperator,
            string expectedLogicalOperator,
            object? expectedValue)
        {
            Assert.NotNull(filter);
            Assert.Single(filter.Root.Items);

            var item = filter.Root.Items[0];

            Assert.NotNull(item.Condition);
            Assert.Equal(expectedField, item.Condition.Field);
            Assert.Equal(expectedOperator, item.Condition.Operator);
            Assert.Equal(expectedLogicalOperator, item.LogicalOperator);
            Assert.Equal(expectedLogicalOperator, item.Condition.LogicalOperator);

            if (expectedValue is IEnumerable expectedEnumerable && !(expectedValue is string))
            {
                var actualEnumerable = Assert.IsAssignableFrom<IEnumerable>(item.Condition.Value);

                Assert.Equal(
                    expectedEnumerable.Cast<object>(),
                    actualEnumerable.Cast<object>());
            }
            else
            {
                Assert.Equal(expectedValue, item.Condition.Value);
            }
        }

        #endregion
    }
}
