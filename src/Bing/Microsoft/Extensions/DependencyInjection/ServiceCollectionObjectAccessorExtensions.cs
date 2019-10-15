using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bing.Dependency;
using Bing.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 服务集合 - 对象访问器 扩展
    /// </summary>
    public static class ServiceCollectionObjectAccessorExtensions
    {
        /// <summary>
        /// 添加对象访问器。如果服务集合还没有，则将其添加到服务集合中
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="services">服务集合</param>
        public static ObjectAccessor<T> TryAddObjectAccessor<T>(this IServiceCollection services)
        {
            if (services.Any(x => x.ServiceType == typeof(ObjectAccessor<T>)))
                return services.GetSingletonInstance<ObjectAccessor<T>>();
            return null;
        }
    }
}
