using System;
using Bing.Modularity.PlugIns;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bing
{
    /// <summary>
    /// Bing 应用程序创建选项配置
    /// </summary>
    public class BingApplicationCreationOptions
    {
        /// <summary>
        /// 初始化一个<see cref="BingApplicationCreationOptions"/>类型的实例
        /// </summary>
        /// <param name="services">服务集合</param>
        public BingApplicationCreationOptions(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            PlugInSources = new PlugInSourceList();
            Configuration = new ConfigurationBuilderOptions();
        }

        /// <summary>
        /// 服务集合
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 插件源列表
        /// </summary>
        public PlugInSourceList PlugInSources { get; }

        /// <summary>
        /// 配置
        /// </summary>
        public ConfigurationBuilderOptions Configuration { get; }
    }
}
