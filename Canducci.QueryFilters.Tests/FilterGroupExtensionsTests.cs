using Canducci.QueryFilters.Models;
using Canducci.QueryFilters.Operators;
using System.Collections;

namespace Canducci.QueryFilters.Tests
{
    public class FilterGroupExtensionsTests
    {
        #region Comparisons

        [Fact]
        public void Comparisons_ShouldGenerateCorrectConditions()
        {
            AssertCondition(Create().WhereEquals("Age", 18), "Age", FilterOperator.Equal, "AND", 18);
            AssertCondition(Create().WhereNotEquals("Age", 18), "Age", FilterOperator.NotEqual, "AND", 18);
            AssertCondition(Create().WhereGreaterThan("Age", 18), "Age", FilterOperator.GreaterThan, "AND", 18);
            AssertCondition(Create().WhereGreaterThanOrEqual("Age", 18), "Age", FilterOperator.GreaterThanOrEqual, "AND", 18);
            AssertCondition(Create().WhereLessThan("Age", 60), "Age", FilterOperator.LessThan, "AND", 60);
            AssertCondition(Create().WhereLessThanOrEqual("Age", 60), "Age", FilterOperator.LessThanOrEqual, "AND", 60);

            AssertCondition(Create().AndEquals("Age", 18), "Age", FilterOperator.Equal, "AND", 18);
            AssertCondition(Create().AndNotEquals("Age", 18), "Age", FilterOperator.NotEqual, "AND", 18);
            AssertCondition(Create().AndGreaterThan("Age", 18), "Age", FilterOperator.GreaterThan, "AND", 18);
            AssertCondition(Create().AndGreaterThanOrEqual("Age", 18), "Age", FilterOperator.GreaterThanOrEqual, "AND", 18);
            AssertCondition(Create().AndLessThan("Age", 60), "Age", FilterOperator.LessThan, "AND", 60);
            AssertCondition(Create().AndLessThanOrEqual("Age", 60), "Age", FilterOperator.LessThanOrEqual, "AND", 60);

            AssertCondition(Create().OrEquals("Age", 18), "Age", FilterOperator.Equal, "OR", 18);
            AssertCondition(Create().OrNotEquals("Age", 18), "Age", FilterOperator.NotEqual, "OR", 18);
            AssertCondition(Create().OrGreaterThan("Age", 18), "Age", FilterOperator.GreaterThan, "OR", 18);
            AssertCondition(Create().OrGreaterThanOrEqual("Age", 18), "Age", FilterOperator.GreaterThanOrEqual, "OR", 18);
            AssertCondition(Create().OrLessThan("Age", 60), "Age", FilterOperator.LessThan, "OR", 60);
            AssertCondition(Create().OrLessThanOrEqual("Age", 60), "Age", FilterOperator.LessThanOrEqual, "OR", 60);
        }

        #endregion

        #region Null

        [Fact]
        public void Null_ShouldGenerateCorrectConditions()
        {
            AssertCondition(Create().WhereNull("Name"), "Name", FilterOperator.IsNull, "AND", null);
            AssertCondition(Create().WhereNotNull("Name"), "Name", FilterOperator.IsNotNull, "AND", null);

            AssertCondition(Create().AndNull("Name"), "Name", FilterOperator.IsNull, "AND", null);
            AssertCondition(Create().AndNotNull("Name"), "Name", FilterOperator.IsNotNull, "AND", null);

            AssertCondition(Create().OrNull("Name"), "Name", FilterOperator.IsNull, "OR", null);
            AssertCondition(Create().OrNotNull("Name"), "Name", FilterOperator.IsNotNull, "OR", null);
        }

        #endregion

        #region Between

        [Fact]
        public void Between_ShouldGenerateCorrectConditions()
        {
            AssertCondition(Create().WhereBetween("Age", 10, 20), "Age", FilterOperator.Between, "AND", new object[] { 10, 20 });
            AssertCondition(Create().WhereNotBetween("Age", 10, 20), "Age", FilterOperator.NotBetween, "AND", new object[] { 10, 20 });

            AssertCondition(Create().AndBetween("Age", 10, 20), "Age", FilterOperator.Between, "AND", new object[] { 10, 20 });
            AssertCondition(Create().AndNotBetween("Age", 10, 20), "Age", FilterOperator.NotBetween, "AND", new object[] { 10, 20 });

            AssertCondition(Create().OrBetween("Age", 10, 20), "Age", FilterOperator.Between, "OR", new object[] { 10, 20 });
            AssertCondition(Create().OrNotBetween("Age", 10, 20), "Age", FilterOperator.NotBetween, "OR", new object[] { 10, 20 });
        }

        [Fact]
        public void Between_ShouldAcceptDifferentTypes()
        {
            var group = Create().WhereBetween("Price", 10, 20.50m);

            AssertCondition(group, "Price", FilterOperator.Between, "AND", new object[] { 10, 20.50m });
        }

        #endregion

        #region In

        [Fact]
        public void In_ShouldGenerateCorrectConditions()
        {
            AssertCondition(Create().WhereIn("Status", 1, 2, 3), "Status", FilterOperator.In, "AND", new object[] { 1, 2, 3 });
            AssertCondition(Create().WhereNotIn("Status", 1, 2, 3), "Status", FilterOperator.NotIn, "AND", new object[] { 1, 2, 3 });

            AssertCondition(Create().AndIn("Status", 1, 2, 3), "Status", FilterOperator.In, "AND", new object[] { 1, 2, 3 });
            AssertCondition(Create().AndNotIn("Status", 1, 2, 3), "Status", FilterOperator.NotIn, "AND", new object[] { 1, 2, 3 });

            AssertCondition(Create().OrIn("Status", 1, 2, 3), "Status", FilterOperator.In, "OR", new object[] { 1, 2, 3 });
            AssertCondition(Create().OrNotIn("Status", 1, 2, 3), "Status", FilterOperator.NotIn, "OR", new object[] { 1, 2, 3 });
        }

        #endregion

        #region Contains

        [Fact]
        public void Contains_ShouldGenerateCorrectConditions()
        {
            AssertCondition(Create().WhereContains("Name", "Fulvio"), "Name", FilterOperator.Contains, "AND", "Fulvio");
            AssertCondition(Create().WhereNotContains("Name", "Fulvio"), "Name", FilterOperator.NotContains, "AND", "Fulvio");

            AssertCondition(Create().AndContains("Name", "Fulvio"), "Name", FilterOperator.Contains, "AND", "Fulvio");
            AssertCondition(Create().AndNotContains("Name", "Fulvio"), "Name", FilterOperator.NotContains, "AND", "Fulvio");

            AssertCondition(Create().OrContains("Name", "Fulvio"), "Name", FilterOperator.Contains, "OR", "Fulvio");
            AssertCondition(Create().OrNotContains("Name", "Fulvio"), "Name", FilterOperator.NotContains, "OR", "Fulvio");
        }

        #endregion

        #region StartsWith

        [Fact]
        public void StartsWith_ShouldGenerateCorrectConditions()
        {
            AssertCondition(Create().WhereStartsWith("Name", "Ful"), "Name", FilterOperator.StartsWith, "AND", "Ful");
            AssertCondition(Create().WhereNotStartsWith("Name", "Ful"), "Name", FilterOperator.NotStartsWith, "AND", "Ful");

            AssertCondition(Create().AndStartsWith("Name", "Ful"), "Name", FilterOperator.StartsWith, "AND", "Ful");
            AssertCondition(Create().AndNotStartsWith("Name", "Ful"), "Name", FilterOperator.NotStartsWith, "AND", "Ful");

            AssertCondition(Create().OrStartsWith("Name", "Ful"), "Name", FilterOperator.StartsWith, "OR", "Ful");
            AssertCondition(Create().OrNotStartsWith("Name", "Ful"), "Name", FilterOperator.NotStartsWith, "OR", "Ful");
        }

        #endregion

        #region EndsWith

        [Fact]
        public void EndsWith_ShouldGenerateCorrectConditions()
        {
            AssertCondition(Create().WhereEndsWith("Name", "vio"), "Name", FilterOperator.EndsWith, "AND", "vio");
            AssertCondition(Create().WhereNotEndsWith("Name", "vio"), "Name", FilterOperator.NotEndsWith, "AND", "vio");

            AssertCondition(Create().AndEndsWith("Name", "vio"), "Name", FilterOperator.EndsWith, "AND", "vio");
            AssertCondition(Create().AndNotEndsWith("Name", "vio"), "Name", FilterOperator.NotEndsWith, "AND", "vio");

            AssertCondition(Create().OrEndsWith("Name", "vio"), "Name", FilterOperator.EndsWith, "OR", "vio");
            AssertCondition(Create().OrNotEndsWith("Name", "vio"), "Name", FilterOperator.NotEndsWith, "OR", "vio");
        }

        #endregion

        #region Helpers

        private static FilterGroup Create()
        {
            return new FilterGroup();
        }

        private static void AssertCondition(
            FilterGroup filter,
            string expectedField,
            FilterOperator expectedOperator,
            string expectedLogicalOperator,
            object? expectedValue)
        {
            Assert.NotNull(filter);
            Assert.Single(filter.Items);

            var item = filter.Items[0];

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
