using System;
using Bing.Internal;
using Bing.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Bing
{
    /// <summary>
    /// 应用程序工厂
    /// </summary>
    public static class BingApplicationFactory
    {
        /// <summary>
        /// 创建应用程序内部服务提供程序
        /// </summary>
        /// <typeparam name="TStartupModule">应用程序启动(入口)模块类型</typeparam>
        /// <param name="optionsAction">选项配置操作</param>
        public static IBingApplicationWithInternalServiceProvider Create<TStartupModule>(
            Action<BingApplicationCreationOptions> optionsAction = null) where TStartupModule : IBingModule =>
            Create(typeof(TStartupModule), optionsAction);

        /// <summary>
        /// 创建应用程序内部服务提供程序
        /// </summary>
        /// <param name="startupModuleType">应用程序启动(入口)模块类型</param>
        /// <param name="optionsAction">选项配置操作</param>
        public static IBingApplicationWithInternalServiceProvider Create(Type startupModuleType,
            Action<BingApplicationCreationOptions> optionsAction = null) =>
            new BingApplicationWithInternalServiceProvider(startupModuleType, optionsAction);

        /// <summary>
        /// 创建应用程序外部服务提供程序
        /// </summary>
        /// <typeparam name="TStartupModule">应用程序启动(入口)模块类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="optionsAction">选项配置操作</param>
        public static IBingApplicationWithExternalServiceProvider Create<TStartupModule>(IServiceCollection services,
            Action<BingApplicationCreationOptions> optionsAction = null) where TStartupModule : IBingModule =>
            Create(typeof(TStartupModule), services, optionsAction);

        /// <summary>
        /// 创建应用程序外部服务提供程序
        /// </summary>
        /// <param name="startupModuleType">应用程序启动(入口)模块类型</param>
        /// <param name="services">服务集合</param>
        /// <param name="optionsAction">选项配置操作</param>
        public static IBingApplicationWithExternalServiceProvider Create(Type startupModuleType
            , IServiceCollection services
            , Action<BingApplicationCreationOptions> optionsAction = null) =>
            new BingApplicationWithExternalServiceProvider(startupModuleType, services, optionsAction);
    }
}
