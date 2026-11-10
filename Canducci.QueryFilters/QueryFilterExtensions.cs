using Canducci.QueryFilters.Models;
using Canducci.QueryFilters.Operators;

namespace Canducci.QueryFilters
{
    public static class QueryFilterExtensions
    {

        public static QueryFilterResult Compile(this QueryFilter filter)
        {
            return new QueryFilterCompiler().Compile(filter);
        }

        #region Comparisons

        public static QueryFilter WhereEquals<T>(this QueryFilter filter, string field, T value)
        {
            return filter.Where(field, FilterOperator.Equal, value);
        }

        public static QueryFilter WhereNotEquals<T>(this QueryFilter filter, string field, T value)
        {
            return filter.Where(field, FilterOperator.NotEqual, value);
        }

        public static QueryFilter AndEquals<T>(this QueryFilter filter, string field, T value)
        {
            return filter.And(field, FilterOperator.Equal, value);
        }

        public static QueryFilter AndNotEquals<T>(this QueryFilter filter, string field, T value)
        {
            return filter.And(field, FilterOperator.NotEqual, value);
        }

        public static QueryFilter OrEquals<T>(this QueryFilter filter, string field, T value)
        {
            return filter.Or(field, FilterOperator.Equal, value);
        }

        public static QueryFilter OrNotEquals<T>(this QueryFilter filter, string field, T value)
        {
            return filter.Or(field, FilterOperator.NotEqual, value);
        }

        public static QueryFilter WhereGreaterThan<T>(this QueryFilter filter, string field, T value)
        {
            return filter.Where(field, FilterOperator.GreaterThan, value);
        }

        public static QueryFilter WhereGreaterThanOrEqual<T>(this QueryFilter filter, string field, T value)
        {
            return filter.Where(field, FilterOperator.GreaterThanOrEqual, value);
        }

        public static QueryFilter AndGreaterThan<T>(this QueryFilter filter, string field, T value)
        {
            return filter.And(field, FilterOperator.GreaterThan, value);
        }

        public static QueryFilter AndGreaterThanOrEqual<T>(this QueryFilter filter, string field, T value)
        {
            return filter.And(field, FilterOperator.GreaterThanOrEqual, value);
        }

        public static QueryFilter OrGreaterThan<T>(this QueryFilter filter, string field, T value)
        {
            return filter.Or(field, FilterOperator.GreaterThan, value);
        }

        public static QueryFilter OrGreaterThanOrEqual<T>(this QueryFilter filter, string field, T value)
        {
            return filter.Or(field, FilterOperator.GreaterThanOrEqual, value);
        }

        public static QueryFilter WhereLessThan<T>(this QueryFilter filter, string field, T value)
        {
            return filter.Where(field, FilterOperator.LessThan, value);
        }

        public static QueryFilter WhereLessThanOrEqual<T>(this QueryFilter filter, string field, T value)
        {
            return filter.Where(field, FilterOperator.LessThanOrEqual, value);
        }

        public static QueryFilter AndLessThan<T>(this QueryFilter filter, string field, T value)
        {
            return filter.And(field, FilterOperator.LessThan, value);
        }

        public static QueryFilter AndLessThanOrEqual<T>(this QueryFilter filter, string field, T value)
        {
            return filter.And(field, FilterOperator.LessThanOrEqual, value);
        }

        public static QueryFilter OrLessThan<T>(this QueryFilter filter, string field, T value)
        {
            return filter.Or(field, FilterOperator.LessThan, value);
        }

        public static QueryFilter OrLessThanOrEqual<T>(this QueryFilter filter, string field, T value)
        {
            return filter.Or(field, FilterOperator.LessThanOrEqual, value);
        }

        #endregion

        #region Null

        public static QueryFilter WhereNull(this QueryFilter filter, string field)
        {
            return filter.Where(field, FilterOperator.IsNull);
        }

        public static QueryFilter WhereNotNull(this QueryFilter filter, string field)
        {
            return filter.Where(field, FilterOperator.IsNotNull);
        }

        public static QueryFilter AndNull(this QueryFilter filter, string field)
        {
            return filter.And(field, FilterOperator.IsNull);
        }

        public static QueryFilter AndNotNull(this QueryFilter filter, string field)
        {
            return filter.And(field, FilterOperator.IsNotNull);
        }

        public static QueryFilter OrNull(this QueryFilter filter, string field)
        {
            return filter.Or(field, FilterOperator.IsNull);
        }

        public static QueryFilter OrNotNull(this QueryFilter filter, string field)
        {
            return filter.Or(field, FilterOperator.IsNotNull);
        }

        #endregion

        #region Between

        public static QueryFilter WhereBetween<TStart, TEnd>(this QueryFilter filter, string field, TStart start, TEnd end)
        {
            return filter.Where(field, FilterOperator.Between, new object[] { start, end });
        }

        public static QueryFilter WhereNotBetween<TStart, TEnd>(this QueryFilter filter, string field, TStart start, TEnd end)
        {
            return filter.Where(field, FilterOperator.NotBetween, new object[] { start, end });
        }

        public static QueryFilter AndBetween<TStart, TEnd>(this QueryFilter filter, string field, TStart start, TEnd end)
        {
            return filter.And(field, FilterOperator.Between, new object[] { start, end });
        }

        public static QueryFilter AndNotBetween<TStart, TEnd>(this QueryFilter filter, string field, TStart start, TEnd end)
        {
            return filter.And(field, FilterOperator.NotBetween, new object[] { start, end });
        }

        public static QueryFilter OrBetween<TStart, TEnd>(this QueryFilter filter, string field, TStart start, TEnd end)
        {
            return filter.Or(field, FilterOperator.Between, new object[] { start, end });
        }

        public static QueryFilter OrNotBetween<TStart, TEnd>(this QueryFilter filter, string field, TStart start, TEnd end)
        {
            return filter.Or(field, FilterOperator.NotBetween, new object[] { start, end });
        }

        #endregion

        #region In

        public static QueryFilter WhereIn<T>(this QueryFilter filter, string field, params T[] values)
        {
            return filter.Where(field, FilterOperator.In, values);
        }

        public static QueryFilter WhereNotIn<T>(this QueryFilter filter, string field, params T[] values)
        {
            return filter.Where(field, FilterOperator.NotIn, values);
        }

        public static QueryFilter AndIn<T>(this QueryFilter filter, string field, params T[] values)
        {
            return filter.And(field, FilterOperator.In, values);
        }

        public static QueryFilter AndNotIn<T>(this QueryFilter filter, string field, params T[] values)
        {
            return filter.And(field, FilterOperator.NotIn, values);
        }

        public static QueryFilter OrIn<T>(this QueryFilter filter, string field, params T[] values)
        {
            return filter.Or(field, FilterOperator.In, values);
        }

        public static QueryFilter OrNotIn<T>(this QueryFilter filter, string field, params T[] values)
        {
            return filter.Or(field, FilterOperator.NotIn, values);
        }

        #endregion

        #region Contains

        public static QueryFilter WhereContains(this QueryFilter filter, string field, string value)
        {
            return filter.Where(field, FilterOperator.Contains, value);
        }

        public static QueryFilter WhereNotContains(this QueryFilter filter, string field, string value)
        {
            return filter.Where(field, FilterOperator.NotContains, value);
        }

        public static QueryFilter AndContains(this QueryFilter filter, string field, string value)
        {
            return filter.And(field, FilterOperator.Contains, value);
        }

        public static QueryFilter AndNotContains(this QueryFilter filter, string field, string value)
        {
            return filter.And(field, FilterOperator.NotContains, value);
        }

        public static QueryFilter OrContains(this QueryFilter filter, string field, string value)
        {
            return filter.Or(field, FilterOperator.Contains, value);
        }

        public static QueryFilter OrNotContains(this QueryFilter filter, string field, string value)
        {
            return filter.Or(field, FilterOperator.NotContains, value);
        }

        #endregion

        #region StartsWith

        public static QueryFilter WhereStartsWith(this QueryFilter filter, string field, string value)
        {
            return filter.Where(field, FilterOperator.StartsWith, value);
        }

        public static QueryFilter WhereNotStartsWith(this QueryFilter filter, string field, string value)
        {
            return filter.Where(field, FilterOperator.NotStartsWith, value);
        }

        public static QueryFilter AndStartsWith(this QueryFilter filter, string field, string value)
        {
            return filter.And(field, FilterOperator.StartsWith, value);
        }

        public static QueryFilter AndNotStartsWith(this QueryFilter filter, string field, string value)
        {
            return filter.And(field, FilterOperator.NotStartsWith, value);
        }

        public static QueryFilter OrStartsWith(this QueryFilter filter, string field, string value)
        {
            return filter.Or(field, FilterOperator.StartsWith, value);
        }

        public static QueryFilter OrNotStartsWith(this QueryFilter filter, string field, string value)
        {
            return filter.Or(field, FilterOperator.NotStartsWith, value);
        }

        #endregion

        #region EndsWith

        public static QueryFilter WhereEndsWith(this QueryFilter filter, string field, string value)
        {
            return filter.Where(field, FilterOperator.EndsWith, value);
        }

        public static QueryFilter WhereNotEndsWith(this QueryFilter filter, string field, string value)
        {
            return filter.Where(field, FilterOperator.NotEndsWith, value);
        }

        public static QueryFilter AndEndsWith(this QueryFilter filter, string field, string value)
        {
            return filter.And(field, FilterOperator.EndsWith, value);
        }

        public static QueryFilter AndNotEndsWith(this QueryFilter filter, string field, string value)
        {
            return filter.And(field, FilterOperator.NotEndsWith, value);
        }

        public static QueryFilter OrEndsWith(this QueryFilter filter, string field, string value)
        {
            return filter.Or(field, FilterOperator.EndsWith, value);
        }

        public static QueryFilter OrNotEndsWith(this QueryFilter filter, string field, string value)
        {
            return filter.Or(field, FilterOperator.NotEndsWith, value);
        }

        #endregion
    }
}
