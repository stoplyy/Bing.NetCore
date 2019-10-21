using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bing.Utils.Reflections
{
    /// <summary>
    /// 属性缓存操作辅助类
    /// </summary>
    public static class PropertyCacheHelper
    {
        /// <summary>
        /// 类型属性缓存
        /// </summary>
        private static readonly Dictionary<RuntimeTypeHandle, IList<PropertyInfo>> TypePropertiesCache =
            new Dictionary<RuntimeTypeHandle, IList<PropertyInfo>>();

        /// <summary>
        /// 类型属性字典缓存
        /// </summary>
        private static readonly Dictionary<RuntimeTypeHandle, IDictionary<string, PropertyInfo>>
            TypePropertiesDictionaryCache = new Dictionary<RuntimeTypeHandle, IDictionary<string, PropertyInfo>>();

        /// <summary>
        /// 获取属性列表
        /// </summary>
        /// <param name="type">类型</param>
        public static IList<PropertyInfo> GetPropertiesFromCache(this Type type)
        {
            if (TypePropertiesCache.TryGetValue(type.TypeHandle, out var pis))
                return pis;
            return TypePropertiesCache[type.TypeHandle] = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanRead)
                .ToList();
        }

        /// <summary>
        /// 获取属性列表
        /// </summary>
        /// <param name="instance">实例</param>
        public static IList<PropertyInfo> GetPropertiesFromCache(this object instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            return instance.GetType().GetPropertiesFromCache();
        }

        /// <summary>
        /// 获取属性字典
        /// </summary>
        /// <param name="type">类型</param>
        public static IDictionary<string, PropertyInfo> GetPropertiesDictionaryFromCache(this Type type)
        {
            if (TypePropertiesDictionaryCache.TryGetValue(type.TypeHandle, out var pis))
                return pis;
            return TypePropertiesDictionaryCache[type.TypeHandle] = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanRead)
                .ToDictionary(x => x.Name, x => x);
        }

        /// <summary>
        /// 获取属性字典
        /// </summary>
        /// <param name="instance">实例</param>
        public static IDictionary<string, PropertyInfo> GetPropertiesDictionaryFromCache(this object instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            return instance.GetType().GetPropertiesDictionaryFromCache();
        }
    }
}
