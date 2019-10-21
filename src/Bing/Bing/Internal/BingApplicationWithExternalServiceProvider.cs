using System;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Internal
{
    /// <summary>
    /// Bing 应用程序外部服务提供程序
    /// </summary>
    internal class BingApplicationWithExternalServiceProvider : BingApplicationBase, IBingApplicationWithExternalServiceProvider
    {
        /// <summary>
        /// 初始化一个<see cref="BingApplicationBase"/>类型的实例
        /// </summary>
        /// <param name="startupModuleType">应用程序启动(入口)模块类型</param>
        /// <param name="services">服务集合</param>
        /// <param name="optionsAction">选项配置操作</param>
        public BingApplicationWithExternalServiceProvider(
            Type startupModuleType
            , IServiceCollection services
            , Action<BingApplicationCreationOptions> optionsAction)
            : base(startupModuleType, services, optionsAction)
        {
            services.AddSingleton<IBingApplicationWithExternalServiceProvider>(this);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="serviceProvider">服务提供程序</param>
        public void Initialize(IServiceProvider serviceProvider)
        {
            SetServiceProvider(serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider)));
            InitializeModules();
        }
    }
}
