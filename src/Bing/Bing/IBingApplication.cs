using System;
using Bing.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Bing
{
    /// <summary>
    /// Bing 应用程序
    /// </summary>
    public interface IBingApplication:IModuleContainer,IDisposable
    {
        /// <summary>
        /// 应用程序启动(入口)模块类型
        /// </summary>
        Type StartupModuleType { get; }

        /// <summary>
        /// 服务集合。应用程序初始化后，无法向该集合添加新服务
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// 服务提供程序。不能在初始化应用程序之前使用它。
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 关闭
        /// </summary>
        void Shutdown();
    }
}
