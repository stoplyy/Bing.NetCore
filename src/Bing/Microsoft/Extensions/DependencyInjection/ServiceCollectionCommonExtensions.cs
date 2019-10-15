using System;
using System.Linq;
using System.Reflection;
using Bing;
using Bing.Utils.Helpers;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 服务集合 - 通用 扩展
    /// </summary>
    public static class ServiceCollectionCommonExtensions
    {
        /// <summary>
        /// 是否已注册指定类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="services">服务集合</param>
        public static bool IsAdded<T>(this IServiceCollection services) => services.IsAdded(typeof(T));

        /// <summary>
        /// 是否已注册指定类型
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="type">类型</param>
        public static bool IsAdded(this IServiceCollection services, Type type) => services.Any(x => x.ServiceType == type);

        /// <summary>
        /// 获取单例注册服务对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="services">服务集合</param>
        public static T GetSingletonInstanceOrNull<T>(this IServiceCollection services) => (T)services.FirstOrDefault(x => x.ServiceType == typeof(T))?.ImplementationInstance;

        /// <summary>
        /// 获取单例注册服务对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="services">服务集合</param>
        public static T GetSingletonInstance<T>(this IServiceCollection services)
        {
            var instance = services.GetSingletonInstanceOrNull<T>();
            if (instance == null)
                throw new InvalidOperationException($"无法找到已注册的单例服务: {typeof(T).AssemblyQualifiedName}");
            return instance;
        }

        /// <summary>
        /// 从工厂构建服务提供程序
        /// </summary>
        /// <param name="services">服务集合</param>
        public static IServiceProvider BuildServiceProviderFromFactory(this IServiceCollection services)
        {
            Check.NotNull(services,nameof(services));

            foreach (var service in services)
            {
                var factoryInterface = service.ImplementationInstance?.GetType()
                    .GetTypeInfo()
                    .GetInterfaces()
                    .FirstOrDefault(x =>
                        x.GetTypeInfo().IsGenericType &&
                        x.GetGenericTypeDefinition() == typeof(IServiceProviderFactory<>));
                if(factoryInterface==null)
                    continue;
                var containerBuilderType = factoryInterface.GenericTypeArguments[0];
                return (IServiceProvider)typeof(ServiceCollectionCommonExtensions).GetTypeInfo().GetMethods()
                    .Single(m => m.Name == nameof(BuildServiceProviderFromFactory) && m.IsGenericMethod)
                    .MakeGenericMethod(containerBuilderType)
                    .Invoke(null, new object[] {services, null});
            }
            
            return services.BuildServiceProvider();
        }

        /// <summary>
        /// 从工厂构建服务提供程序
        /// </summary>
        /// <typeparam name="TContainerBuilder">容器构建器</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="builderAction">构建操作</param>
        public static IServiceProvider BuildServiceProviderFromFactory<TContainerBuilder>(
            this IServiceCollection services, Action<TContainerBuilder> builderAction = null)
        {
            Check.NotNull(services, nameof(services));

            var serviceProviderFactory =
                services.GetSingletonInstanceOrNull<IServiceProviderFactory<TContainerBuilder>>();
            if (serviceProviderFactory == null)
                throw new BingException(
                    $"无法从 {services} 中找到 {typeof(IServiceProviderFactory<TContainerBuilder>).FullName}。");
            var builder = serviceProviderFactory.CreateBuilder(services);
            builderAction?.Invoke(builder);
            return serviceProviderFactory.CreateServiceProvider(builder);
        }

    }
}
