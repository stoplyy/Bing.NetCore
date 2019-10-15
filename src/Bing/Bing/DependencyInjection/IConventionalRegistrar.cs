using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.DependencyInjection
{
    /// <summary>
    /// 常规注册器
    /// </summary>
    public interface IConventionalRegistrar
    {
        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="assembly">程序集</param>
        void AddAssembly(IServiceCollection services, Assembly assembly);

        /// <summary>
        /// 注册批量类型
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="types">类型集合</param>
        void AddTypes(IServiceCollection services, params Type[] types);

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="type">类型</param>
        void AddType(IServiceCollection services, Type type);
    }
}
