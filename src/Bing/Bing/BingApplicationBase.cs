using System;
using System.Collections.Generic;
using Bing.DependencyInjection;
using Bing.Internal;
using Bing.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Bing
{
    /// <summary>
    /// Bing 应用程序抽象基类
    /// </summary>
    public abstract class BingApplicationBase : IBingApplication
    {
        /// <summary>
        /// 初始化一个<see cref="BingApplicationBase"/>类型的实例
        /// </summary>
        /// <param name="startupModuleType">应用程序启动(入口)模块类型</param>
        /// <param name="services">服务集合</param>
        /// <param name="optionsAction">选项配置操作</param>
        internal BingApplicationBase(
            Type startupModuleType
            , IServiceCollection services
            , Action<BingApplicationCreationOptions> optionsAction)
        {
            StartupModuleType = startupModuleType ?? throw new ArgumentNullException(nameof(startupModuleType));
            Services = services ?? throw new ArgumentNullException(nameof(services));

            services.TryAddObjectAccessor<IServiceProvider>();

            var options = new BingApplicationCreationOptions(services);
            optionsAction?.Invoke(options);

            services.AddSingleton<IBingApplication>(this);
            services.AddSingleton<IModuleContainer>(this);

            services.AddCoreServices();
            services.AddCoreBingServices(this, options);

            Modules = LoadModules(services, options);
        }

        /// <summary>
        /// 模块列表
        /// </summary>
        public IReadOnlyList<IBingModuleDescriptor> Modules { get; }

        /// <summary>
        /// 应用程序启动(入口)模块类型
        /// </summary>
        public Type StartupModuleType { get; }

        /// <summary>
        /// 服务集合。应用程序初始化后，无法向该集合添加新服务
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 服务提供程序。不能在初始化应用程序之前使用它。
        /// </summary>
        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// 关闭
        /// </summary>
        public virtual void Shutdown()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                scope.ServiceProvider
                    .GetRequiredService<IModuleManager>()
                    .ShutdownModules(new ApplicationShutdownContext(scope.ServiceProvider));
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
        }

        /// <summary>
        /// 设置服务提供程序
        /// </summary>
        /// <param name="serviceProvider">服务提供程序</param>
        protected virtual void SetServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ServiceProvider.GetRequiredService<ObjectAccessor<IServiceProvider>>().Value = ServiceProvider;
        }

        /// <summary>
        /// 初始化模块列表
        /// </summary>
        protected virtual void InitializeModules()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                scope.ServiceProvider
                    .GetRequiredService<IModuleManager>()
                    .InitializeModules(new ApplicationInitializationContext(scope.ServiceProvider));
            }
        }

        /// <summary>
        /// 加载模块列表
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="options">选项配置</param>
        private IReadOnlyList<IBingModuleDescriptor> LoadModules(IServiceCollection services, BingApplicationCreationOptions options) =>
            services.GetSingletonInstance<IModuleLoader>()
                .LoadModules(services, StartupModuleType, options.PlugInSources);
    }
}
