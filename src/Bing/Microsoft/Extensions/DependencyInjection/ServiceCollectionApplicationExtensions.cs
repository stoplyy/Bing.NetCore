using System;
using Bing;
using Bing.Modularity;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 服务集合 - 应用程序 扩展
    /// </summary>
    public static class ServiceCollectionApplicationExtensions
    {
        /// <summary>
        /// 注册应用程序
        /// </summary>
        /// <typeparam name="TStartupModule">应用程序启动(入口)模块类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="optionsAction">选项配置操作</param>
        /// <returns></returns>
        public static IBingApplicationWithExternalServiceProvider AddApplication<TStartupModule>(
            this IServiceCollection services, Action<BingApplicationCreationOptions> optionsAction = null)
            where TStartupModule : IBingModule =>
            BingApplicationFactory.Create<TStartupModule>(services, optionsAction);


        /// <summary>
        /// 注册应用程序
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="startupModuleType">应用程序启动(入口)模块类型</param>
        /// <param name="optionsAction">选项配置操作</param>
        public static IBingApplicationWithExternalServiceProvider AddApplication(this IServiceCollection services,
            Type startupModuleType, Action<BingApplicationCreationOptions> optionsAction = null) =>
            BingApplicationFactory.Create(startupModuleType, services, optionsAction);
    }
}
