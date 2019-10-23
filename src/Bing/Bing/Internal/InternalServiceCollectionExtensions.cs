using Bing.Modularity;
using Bing.Reflections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bing.Internal
{
    /// <summary>
    /// 内部服务集合(<see cref="IServiceCollection"/>) 扩展
    /// </summary>
    internal static class InternalServiceCollectionExtensions
    {
        /// <summary>
        /// 注册核心服务
        /// </summary>
        /// <param name="services">服务集合</param>
        internal static void AddCoreServices(this IServiceCollection services)
        {
            services.AddOptions();
            services.AddLogging();
        }

        /// <summary>
        /// 注册Bing核心服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="bingApplication">Bing应用程序</param>
        /// <param name="applicationCreationOptions">Bing应用程序创建选项配置</param>
        internal static void AddCoreBingServices(this IServiceCollection services, IBingApplication bingApplication,
            BingApplicationCreationOptions applicationCreationOptions)
        {
            var moduleLoader = new ModuleLoader();

            services.TryAddSingleton<IModuleLoader>(moduleLoader);
            services.AddAssemblyOf<IBingApplication>();

            var finder = new AppDomainAllAssemblyFinder();
            foreach (var assembly in finder.FindAll(true))
            {
                services.AddAssembly(assembly);
            }

            services.Configure<ModuleLifecycleOptions>(options =>
            {
                options.Contributors.Add<OnPreApplicationInitializationModuleLifecycleContributor>();
                options.Contributors.Add<OnApplicationInitializationModuleLifecycleContributor>();
                options.Contributors.Add<OnPostApplicationInitializationModuleLifecycleContributor>();
                options.Contributors.Add<OnApplicationShutdownModuleLifecycleContributor>();
            });
        }
    }
}
