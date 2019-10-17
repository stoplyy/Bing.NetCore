using System;
using System.Linq;
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
        public static ObjectAccessor<T> TryAddObjectAccessor<T>(this IServiceCollection services) =>
            services.Any(x => x.ServiceType == typeof(ObjectAccessor<T>))
                ? services.GetSingletonInstance<ObjectAccessor<T>>()
                : services.AddObjectAccessor<T>();

        /// <summary>
        /// 添加对象访问器
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="services">服务集合</param>
        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services) => services.AddObjectAccessor(new ObjectAccessor<T>());

        /// <summary>
        /// 添加对象访问器
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="obj">对象</param>
        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services, T obj) => services.AddObjectAccessor(new ObjectAccessor<T>(obj));

        /// <summary>
        /// 添加对象访问器
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="accessor">对象访问器</param>
        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services,
            ObjectAccessor<T> accessor)
        {
            if(services.Any(x=>x.ServiceType==typeof(ObjectAccessor<T>)))
                throw new ArgumentException($"该对象访问器类型之前已注册: {typeof(T).AssemblyQualifiedName}");

            // 将访问器添加到服务集合起始点，便于快速检索
            services.Insert(0,ServiceDescriptor.Singleton(typeof(ObjectAccessor<>),accessor));
            services.Insert(0, ServiceDescriptor.Singleton(typeof(IObjectAccessor<>), accessor));

            return accessor;
        }

        /// <summary>
        /// 获取可空对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="services">服务集合</param>
        public static T GetObjectOrNull<T>(this IServiceCollection services) where T:class => services.GetSingletonInstanceOrNull<IObjectAccessor<T>>()?.Value;

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="services">服务集合</param>
        public static T GetObject<T>(this IServiceCollection services) where T : class =>
            services.GetObjectOrNull<T>() ??
            throw new NullReferenceException(
                $"在服务集合中无法找到 {typeof(T).AssemblyQualifiedName} 对象。请确保之前使用过 AddObjectAccessor 方法!");
    }
}
