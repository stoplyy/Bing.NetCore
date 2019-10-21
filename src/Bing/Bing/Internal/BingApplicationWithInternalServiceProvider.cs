using System;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Internal
{
    /// <summary>
    /// Bing 应用程序内部服务提供程序
    /// </summary>
    internal class BingApplicationWithInternalServiceProvider : BingApplicationBase, IBingApplicationWithInternalServiceProvider
    {
        /// <summary>
        /// 初始化一个<see cref="BingApplicationBase"/>类型的实例
        /// </summary>
        /// <param name="startupModuleType">应用程序启动(入口)模块类型</param>
        /// <param name="optionsAction">选项配置操作</param>
        public BingApplicationWithInternalServiceProvider(
            Type startupModuleType
            , Action<BingApplicationCreationOptions> optionsAction)
            : this(startupModuleType, new ServiceCollection(), optionsAction)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="BingApplicationBase"/>类型的实例
        /// </summary>
        /// <param name="startupModuleType">应用程序启动(入口)模块类型</param>
        /// <param name="services">服务集合</param>
        /// <param name="optionsAction">选项配置操作</param>
        public BingApplicationWithInternalServiceProvider(
            Type startupModuleType
            , IServiceCollection services
            , Action<BingApplicationCreationOptions> optionsAction)
            : base(startupModuleType, services, optionsAction)
        {
            Services.AddSingleton<IBingApplicationWithInternalServiceProvider>(this);
        }

        /// <summary>
        /// 服务作用域
        /// </summary>
        public IServiceScope ServiceScope { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            ServiceScope = Services.BuildServiceProviderFromFactory().CreateScope();
            SetServiceProvider(ServiceScope.ServiceProvider);
            InitializeModules();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            ServiceScope.Dispose();
        }
    }
}
