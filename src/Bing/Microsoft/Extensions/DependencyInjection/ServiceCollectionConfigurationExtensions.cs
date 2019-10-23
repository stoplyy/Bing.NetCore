using Bing.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 服务集合 - 配置 扩展
    /// </summary>
    public static class ServiceCollectionConfigurationExtensions
    {
        /// <summary>
        /// 设置配置
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configurationRoot">配置</param>
        public static IServiceCollection SetConfiguration(this IServiceCollection services,
            IConfigurationRoot configurationRoot) =>
            services.Replace(
                ServiceDescriptor.Singleton<IConfigurationAccessor>(
                    new DefaultConfigurationAccessor(configurationRoot)));

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="services">服务集合</param>
        public static IConfiguration GetConfiguration(this IServiceCollection services) => services.GetSingletonInstance<IConfiguration>();
    }
}
