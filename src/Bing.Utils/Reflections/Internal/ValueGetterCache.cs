using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace Bing.Utils.Reflections.Internal
{
    /// <summary>
    /// 值获取器缓存
    /// </summary>
    /// <typeparam name="TParam">参数类型</typeparam>
    /// <typeparam name="TReturn">返回类型</typeparam>
    internal class ValueGetterCache<TParam, TReturn>
    {
        /// <summary>
        /// 函数字典
        /// </summary>
        private static readonly ConcurrentDictionary<int, Func<TParam, TReturn>> Functions =
            new ConcurrentDictionary<int, Func<TParam, TReturn>>();

        /// <summary>
        /// 获取或添加函数缓存
        /// </summary>
        /// <param name="propertyInfo">属性信息</param>
        internal static Func<TParam, TReturn> GetOrAddFunctionCache(PropertyInfo propertyInfo)
        {
            var key = propertyInfo.MetadataToken;
            if (Functions.TryGetValue(key, out var func))
                return func;
            return Functions[key] = GetCastObjectFunction(propertyInfo);
        }

        /// <summary>
        /// 获取映射对象函数
        /// </summary>
        /// <param name="propertyInfo">属性信息</param>
        private static Func<TParam, TReturn> GetCastObjectFunction(PropertyInfo propertyInfo)
        {
            var instance = Expression.Parameter(typeof(TReturn), "i");
            var convert = Expression.TypeAs(instance, propertyInfo.DeclaringType);
            var property = Expression.Property(convert, propertyInfo);
            var cast = Expression.TypeAs(property, typeof(TReturn));
            var lambda = Expression.Lambda<Func<TParam, TReturn>>(cast, instance);
            return lambda.Compile();
        }
    }
}
