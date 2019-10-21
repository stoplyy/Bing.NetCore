using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bing.Utils.Reflections.Internal;

namespace Bing.Utils.Reflections
{
    /// <summary>
    /// 值获取器。
    /// 参考地址：https://github.com/shps951023/ValueGetter/blob/master/ValueGetter/ValueGetter.cs
    /// </summary>
    public static class ValueGetter
    {
        /// <summary>
        /// 获取并转换为字符串字典
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="instance">实例</param>
        public static Dictionary<string, string> GetToStringValues<T>(this T instance) => instance?.GetType()
            .GetPropertiesFromCache().ToDictionary(key => key.Name, value => value.GetToStringValue<T>(instance));

        /// <summary>
        /// 获取并转换为字符串值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="propertyInfo">属性信息</param>
        /// <param name="instance">实例</param>
        public static string GetToStringValue<T>(this PropertyInfo propertyInfo, T instance) => instance != null
            ? ValueGetterCache<T, object>.GetOrAddFunctionCache(propertyInfo)(instance)?.ToString()
            : null;

        /// <summary>
        /// 获取对象字典
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="instance">实例</param>
        public static Dictionary<string, object> GetObjectValues<T>(this T instance) => instance?.GetType()
            .GetPropertiesFromCache().ToDictionary(key => key.Name, value => value.GetObjectValue<T>(instance));

        /// <summary>
        /// 获取对象值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="propertyInfo">属性信息</param>
        /// <param name="instance">实例</param>
        public static object GetObjectValue<T>(this PropertyInfo propertyInfo, T instance) => instance != null
            ? ValueGetterCache<T, object>.GetOrAddFunctionCache(propertyInfo)(instance)
            : null;
    }
}
