
using Canducci.QueryFilters.Models;
using Canducci.QueryFilters.Operators;

namespace Canducci.QueryFilters
{
    public static class FilterGroupExtensions
    {
        #region Comparisons

        public static FilterGroup WhereEquals<T>(this FilterGroup filter, string field, T value)
        {
            return filter.Where(field, FilterOperator.Equal, value);
        }

        public static FilterGroup WhereNotEquals<T>(this FilterGroup filter, string field, T value)
        {
            return filter.Where(field, FilterOperator.NotEqual, value);
        }

        public static FilterGroup AndEquals<T>(this FilterGroup filter, string field, T value)
        {
            return filter.And(field, FilterOperator.Equal, value);
        }

        public static FilterGroup AndNotEquals<T>(this FilterGroup filter, string field, T value)
        {
            return filter.And(field, FilterOperator.NotEqual, value);
        }

        public static FilterGroup OrEquals<T>(this FilterGroup filter, string field, T value)
        {
            return filter.Or(field, FilterOperator.Equal, value);
        }

        public static FilterGroup OrNotEquals<T>(this FilterGroup filter, string field, T value)
        {
            return filter.Or(field, FilterOperator.NotEqual, value);
        }

        public static FilterGroup WhereGreaterThan<T>(this FilterGroup filter, string field, T value)
        {
            return filter.Where(field, FilterOperator.GreaterThan, value);
        }

        public static FilterGroup WhereGreaterThanOrEqual<T>(this FilterGroup filter, string field, T value)
        {
            return filter.Where(field, FilterOperator.GreaterThanOrEqual, value);
        }

        public static FilterGroup AndGreaterThan<T>(this FilterGroup filter, string field, T value)
        {
            return filter.And(field, FilterOperator.GreaterThan, value);
        }

        public static FilterGroup AndGreaterThanOrEqual<T>(this FilterGroup filter, string field, T value)
        {
            return filter.And(field, FilterOperator.GreaterThanOrEqual, value);
        }

        public static FilterGroup OrGreaterThan<T>(this FilterGroup filter, string field, T value)
        {
            return filter.Or(field, FilterOperator.GreaterThan, value);
        }

        public static FilterGroup OrGreaterThanOrEqual<T>(this FilterGroup filter, string field, T value)
        {
            return filter.Or(field, FilterOperator.GreaterThanOrEqual, value);
        }

        public static FilterGroup WhereLessThan<T>(this FilterGroup filter, string field, T value)
        {
            return filter.Where(field, FilterOperator.LessThan, value);
        }

        public static FilterGroup WhereLessThanOrEqual<T>(this FilterGroup filter, string field, T value)
        {
            return filter.Where(field, FilterOperator.LessThanOrEqual, value);
        }

        public static FilterGroup AndLessThan<T>(this FilterGroup filter, string field, T value)
        {
            return filter.And(field, FilterOperator.LessThan, value);
        }

        public static FilterGroup AndLessThanOrEqual<T>(this FilterGroup filter, string field, T value)
        {
            return filter.And(field, FilterOperator.LessThanOrEqual, value);
        }

        public static FilterGroup OrLessThan<T>(this FilterGroup filter, string field, T value)
        {
            return filter.Or(field, FilterOperator.LessThan, value);
        }

        public static FilterGroup OrLessThanOrEqual<T>(this FilterGroup filter, string field, T value)
        {
            return filter.Or(field, FilterOperator.LessThanOrEqual, value);
        }

        #endregion

        #region Null

        public static FilterGroup WhereNull(this FilterGroup filter, string field)
        {
            return filter.Where(field, FilterOperator.IsNull);
        }

        public static FilterGroup WhereNotNull(this FilterGroup filter, string field)
        {
            return filter.Where(field, FilterOperator.IsNotNull);
        }

        public static FilterGroup AndNull(this FilterGroup filter, string field)
        {
            return filter.And(field, FilterOperator.IsNull);
        }

        public static FilterGroup AndNotNull(this FilterGroup filter, string field)
        {
            return filter.And(field, FilterOperator.IsNotNull);
        }

        public static FilterGroup OrNull(this FilterGroup filter, string field)
        {
            return filter.Or(field, FilterOperator.IsNull);
        }

        public static FilterGroup OrNotNull(this FilterGroup filter, string field)
        {
            return filter.Or(field, FilterOperator.IsNotNull);
        }

        #endregion

        #region Between

        public static FilterGroup WhereBetween<TStart, TEnd>(this FilterGroup filter, string field, TStart start, TEnd end)
        {
            return filter.Where(field, FilterOperator.Between, new object[] { start, end });
        }

        public static FilterGroup WhereNotBetween<TStart, TEnd>(this FilterGroup filter, string field, TStart start, TEnd end)
        {
            return filter.Where(field, FilterOperator.NotBetween, new object[] { start, end });
        }

        public static FilterGroup AndBetween<TStart, TEnd>(this FilterGroup filter, string field, TStart start, TEnd end)
        {
            return filter.And(field, FilterOperator.Between, new object[] { start, end });
        }

        public static FilterGroup AndNotBetween<TStart, TEnd>(this FilterGroup filter, string field, TStart start, TEnd end)
        {
            return filter.And(field, FilterOperator.NotBetween, new object[] { start, end });
        }

        public static FilterGroup OrBetween<TStart, TEnd>(this FilterGroup filter, string field, TStart start, TEnd end)
        {
            return filter.Or(field, FilterOperator.Between, new object[] { start, end });
        }

        public static FilterGroup OrNotBetween<TStart, TEnd>(this FilterGroup filter, string field, TStart start, TEnd end)
        {
            return filter.Or(field, FilterOperator.NotBetween, new object[] { start, end });
        }

        #endregion

        #region In

        public static FilterGroup WhereIn<T>(this FilterGroup filter, string field, params T[] values)
        {
            return filter.Where(field, FilterOperator.In, values);
        }

        public static FilterGroup WhereNotIn<T>(this FilterGroup filter, string field, params T[] values)
        {
            return filter.Where(field, FilterOperator.NotIn, values);
        }

        public static FilterGroup AndIn<T>(this FilterGroup filter, string field, params T[] values)
        {
            return filter.And(field, FilterOperator.In, values);
        }

        public static FilterGroup AndNotIn<T>(this FilterGroup filter, string field, params T[] values)
        {
            return filter.And(field, FilterOperator.NotIn, values);
        }

        public static FilterGroup OrIn<T>(this FilterGroup filter, string field, params T[] values)
        {
            return filter.Or(field, FilterOperator.In, values);
        }

        public static FilterGroup OrNotIn<T>(this FilterGroup filter, string field, params T[] values)
        {
            return filter.Or(field, FilterOperator.NotIn, values);
        }

        #endregion

        #region Contains

        public static FilterGroup WhereContains(this FilterGroup filter, string field, string value)
        {
            return filter.Where(field, FilterOperator.Contains, value);
        }

        public static FilterGroup WhereNotContains(this FilterGroup filter, string field, string value)
        {
            return filter.Where(field, FilterOperator.NotContains, value);
        }

        public static FilterGroup AndContains(this FilterGroup filter, string field, string value)
        {
            return filter.And(field, FilterOperator.Contains, value);
        }

        public static FilterGroup AndNotContains(this FilterGroup filter, string field, string value)
        {
            return filter.And(field, FilterOperator.NotContains, value);
        }

        public static FilterGroup OrContains(this FilterGroup filter, string field, string value)
        {
            return filter.Or(field, FilterOperator.Contains, value);
        }

        public static FilterGroup OrNotContains(this FilterGroup filter, string field, string value)
        {
            return filter.Or(field, FilterOperator.NotContains, value);
        }

        #endregion

        #region StartsWith

        public static FilterGroup WhereStartsWith(this FilterGroup filter, string field, string value)
        {
            return filter.Where(field, FilterOperator.StartsWith, value);
        }

        public static FilterGroup WhereNotStartsWith(this FilterGroup filter, string field, string value)
        {
            return filter.Where(field, FilterOperator.NotStartsWith, value);
        }

        public static FilterGroup AndStartsWith(this FilterGroup filter, string field, string value)
        {
            return filter.And(field, FilterOperator.StartsWith, value);
        }

        public static FilterGroup AndNotStartsWith(this FilterGroup filter, string field, string value)
        {
            return filter.And(field, FilterOperator.NotStartsWith, value);
        }

        public static FilterGroup OrStartsWith(this FilterGroup filter, string field, string value)
        {
            return filter.Or(field, FilterOperator.StartsWith, value);
        }

        public static FilterGroup OrNotStartsWith(this FilterGroup filter, string field, string value)
        {
            return filter.Or(field, FilterOperator.NotStartsWith, value);
        }

        #endregion

        #region EndsWith

        public static FilterGroup WhereEndsWith(this FilterGroup filter, string field, string value)
        {
            return filter.Where(field, FilterOperator.EndsWith, value);
        }

        public static FilterGroup WhereNotEndsWith(this FilterGroup filter, string field, string value)
        {
            return filter.Where(field, FilterOperator.NotEndsWith, value);
        }

        public static FilterGroup AndEndsWith(this FilterGroup filter, string field, string value)
        {
            return filter.And(field, FilterOperator.EndsWith, value);
        }

        public static FilterGroup AndNotEndsWith(this FilterGroup filter, string field, string value)
        {
            return filter.And(field, FilterOperator.NotEndsWith, value);
        }

        public static FilterGroup OrEndsWith(this FilterGroup filter, string field, string value)
        {
            return filter.Or(field, FilterOperator.EndsWith, value);
        }

        public static FilterGroup OrNotEndsWith(this FilterGroup filter, string field, string value)
        {
            return filter.Or(field, FilterOperator.NotEndsWith, value);
        }

        #endregion
    }
}
