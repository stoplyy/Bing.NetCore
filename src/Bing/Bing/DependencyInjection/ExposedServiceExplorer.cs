using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bing.DependencyInjection
{
    /// <summary>
    /// 公开服务浏览器
    /// </summary>
    public static class ExposedServiceExplorer
    {
        /// <summary>
        /// 默认公开服务特性
        /// </summary>
        private static readonly ExposeServiceAttribute DefaultExposeServiceAttribute =
            new ExposeServiceAttribute() {IncludeDefaults = true};

        /// <summary>
        /// 获取公开服务列表
        /// </summary>
        /// <param name="type">类型</param>
        public static List<Type> GetExposedServices(Type type) =>
            type
                .GetCustomAttributes()
                .OfType<IExposedServiceTypesProvider>()
                .DefaultIfEmpty(DefaultExposeServiceAttribute)
                .SelectMany(x => x.GetExposedServiceTypes(type))
                .ToList();
    }
}
