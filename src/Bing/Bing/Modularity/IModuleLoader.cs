using System;
using Bing.Modularity.PlugIns;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Modularity
{
    /// <summary>
    /// 模块加载器
    /// </summary>
    public interface IModuleLoader
    {
        /// <summary>
        /// 加载模块列表
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="startupModuleType">应用程序启动(入口)模块类型</param>
        /// <param name="plugInSources">插件源列表</param>
        IBingModuleDescriptor[] LoadModules(IServiceCollection services, Type startupModuleType, PlugInSourceList plugInSources);
    }
}
