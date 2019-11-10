using System;
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// <see cref="IQueryable{T}"/> 扩展
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// 根据条件包含指定属性
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="condition">判断条件，该值为true时添加查询条件，否则忽略</param>
        /// <param name="property">属性表达式</param>
        public static IQueryable<TEntity> IncludeIfNeed<TEntity, TProperty>(this IQueryable<TEntity> source,
            bool condition, Expression<Func<TEntity, TProperty>> property) where TEntity : class =>
            condition ? source.Include(property) : source;

        /// <summary>
        /// 根据条件包含指定属性
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="condition">判断条件，该值为true时添加查询条件，否则忽略</param>
        /// <param name="property">属性表达式</param>
        public static IQueryable<TEntity> IncludeIfNeed<TEntity, TProperty>(this IQueryable<TEntity> source,
            Func<bool> condition, Expression<Func<TEntity, TProperty>> property) where TEntity : class =>
            condition() ? source.Include(property) : source;
    }
}
