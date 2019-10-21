using Bing.AspNetCore;
using Bing.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Samples.DI
{
    /// <summary>
    /// 依赖注入模块
    /// </summary>
    [DependsOn(typeof(BingAspNetCoreModule))]
    public class DISampleModule : BingModule
    {
        /// <summary>
        /// 配置服务集合
        /// </summary>
        /// <param name="context">配置服务上下文</param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
        }

        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseDeveloperExceptionPage();
        }
    }
}
