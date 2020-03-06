using Bing.AspNetCore;
using Bing.Core.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Samples.Modules
{
    /// <summary>
    /// Sample 模块
    /// </summary>
    [DependsOnModule(typeof(AspNetCoreModule))]
    public class SampleAspNetCoreModule : AspNetCoreBingModule
    {
        /// <summary>
        /// 模块级别。级别越小越先启动
        /// </summary>
        public override ModuleLevel Level => ModuleLevel.Application;

        /// <summary>
        /// 添加服务。将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">服务集合</param>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            // 注册上传服务
            services.AddUploadService();
            // 注册Api接口服务
            services.AddApiInterfaceService();
            return services;
        }
    }
}
