using System;
using System.Collections.Generic;
using System.Reflection;
using Bing.DependencyInjection;
using Bing.DependencyInjection.Default;
using Bing.DependencyInjection.Internal;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 服务集合 - 常规注册器 扩展
    /// </summary>
    public static class ServiceCollectionConventionalRegistrationExtensions
    {
        /// <summary>
        /// 注册常规注册器
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="registrar">常规注册器</param>
        public static IServiceCollection AddConventionalRegistrar(this IServiceCollection services,
            IConventionalRegistrar registrar)
        {
            GetOrCreateRegistrarList(services).Add(registrar);
            return services;
        }

        /// <summary>
        /// 获取常规注册器列表
        /// </summary>
        /// <param name="services">服务集合</param>
        internal static List<IConventionalRegistrar> GetConventionalRegistrars(this IServiceCollection services) => GetOrCreateRegistrarList(services);

        /// <summary>
        /// 获取或创建常规注册器列表
        /// </summary>
        /// <param name="services">服务集合</param>
        private static ConventionalRegistrarList GetOrCreateRegistrarList(IServiceCollection services)
        {
            var conventionalRegistrars =
                services.GetSingletonInstanceOrNull<IObjectAccessor<ConventionalRegistrarList>>()?.Value;
            if (conventionalRegistrars == null)
            {
                conventionalRegistrars = new ConventionalRegistrarList() { new DefaultConventionalRegistrar() };
                services.AddObjectAccessor(conventionalRegistrars);
            }
            return conventionalRegistrars;
        }

        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="services">服务集合</param>
        public static IServiceCollection AddAssemblyOf<T>(this IServiceCollection services) => services.AddAssembly(typeof(T).GetTypeInfo().Assembly);

        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="assembly">程序集</param>
        public static IServiceCollection AddAssembly(this IServiceCollection services, Assembly assembly)
        {
            foreach (var registrar in services.GetConventionalRegistrars())
                registrar.AddAssembly(services, assembly);
            return services;
        }

        /// <summary>
        /// 批量注册类型
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="types">类型集合</param>
        public static IServiceCollection AddTypes(this IServiceCollection services, params Type[] types)
        {
            foreach (var registrar in services.GetConventionalRegistrars())
                registrar.AddTypes(services, types);
            return services;
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="TType">类型</typeparam>
        /// <param name="services">服务集合</param>
        public static IServiceCollection AddType<TType>(this IServiceCollection services) => services.AddType(typeof(TType));

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="type">类型</param>
        public static IServiceCollection AddType(this IServiceCollection services, Type type)
        {
            foreach (var registrar in services.GetConventionalRegistrars())
                registrar.AddType(services, type);
            return services;
        }
    }
}
