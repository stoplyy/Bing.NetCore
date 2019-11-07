using System.Collections.Generic;
using Bing.Utils.Internal;

namespace System.Linq
{
    /// <summary>
    /// 泛型集合<see cref="IEnumerable{T}"/> 扩展
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class EnumerableExtensions
    {
        #region ToListSafe(安全获取列表)

        /// <summary>
        /// 安全获取列表。当数据源为null时，不会抛异常
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="source">数据源</param>
        public static List<TSource> ToListSafe<TSource>(this IEnumerable<TSource> source) => source == null ? new List<TSource>() : source.ToList();

        #endregion

        #region ToDictionarySafe(安全获取字典)

        /// <summary>
        /// 安全获取字典。当数据源为null时，不会抛异常
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="keySelector">键选择器</param>
        public static Dictionary<TKey, TSource> ToDictionarySafe<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector) =>
            source == null ? new Dictionary<TKey, TSource>() : source.ToDictionary(keySelector);

        /// <summary>
        /// 安全获取字典。当数据源为null时，不会抛异常
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TElement">元素类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="keySelector">键选择器</param>
        /// <param name="elementSelector">元素选择器</param>
        public static Dictionary<TKey, TElement> ToDictionarySafe<TSource, TKey, TElement>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) =>
            source == null
                ? new Dictionary<TKey, TElement>()
                : source.ToDictionary(keySelector, elementSelector);

        #endregion

        #region ToHashSetSafe(安全获取哈希集合)

        /// <summary>
        /// 安全获取哈希集合。当值为null时，不会抛出异常
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="source">数据源</param>
        public static HashSet<TSource> ToHashSetSafe<TSource>(this IEnumerable<TSource> source) =>
            source == null ? new HashSet<TSource>() : new HashSet<TSource>(source);

        /// <summary>
        /// 安全获取哈希集合。当值为null时，不会抛出异常
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="keySelector">键选择器</param>
        public static HashSet<TKey> ToHashSetSafe<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector) =>
            source == null ? new HashSet<TKey>() : new HashSet<TKey>(source.Select(keySelector));

        #endregion

        #region Compare(比较两个集合)

        /// <summary>
        /// 比较两个集合。返回差集、交集
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="a">数据源A</param>
        /// <param name="b">数据源B</param>
        public static (List<TSource> Left, List<TSource> Right, List<TSource> Intersect) Compare<TSource>(
            this IEnumerable<TSource> a, IEnumerable<TSource> b)
        {
            var aList = a.ToListSafe();
            var bList = b.ToListSafe();

            var left = aList.Except(bList).ToListSafe();
            var right = bList.Except(aList).ToListSafe();
            var intersect = aList.Except(left).ToListSafe();

            return (left, right, intersect);
        }

        /// <summary>
        /// 比较两个集合。返回差集、交集
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <param name="a">数据源A</param>
        /// <param name="b">数据源B</param>
        /// <param name="keySelector">键选择器</param>
        public static (List<TSource> Left, List<TSource> Right, List<TSource> Intersect) Compare<TSource, TKey>(
            this IEnumerable<TSource> a, IEnumerable<TSource> b, Func<TSource, TKey> keySelector)
        {
            var aList = a.ToListSafe();
            var bList = b.ToListSafe();

            var comparer = new KeyComparer<TSource, TKey>(keySelector);

            var left = aList.Except(bList, comparer).ToListSafe();
            var right = bList.Except(aList, comparer).ToListSafe();
            var intersect = aList.Except(left, comparer).ToListSafe();

            return (left, right, intersect);
        }

        #endregion
    }
}
