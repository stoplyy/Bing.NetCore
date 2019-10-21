using Bing.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.AspNetCore
{
    /// <summary>
    /// Bing AspNetCore 模块
    /// </summary>
    public class BingAspNetCoreModule : BingModule
    {
        /// <summary>
        /// 预配置服务集合
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
        }

        /// <summary>
        /// 配置服务集合
        /// </summary>
        /// <param name="context">配置服务上下文</param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            AddAspNetServices(context.Services);
            context.Services.AddObjectAccessor<IApplicationBuilder>();
        }

        /// <summary>
        /// 注册AspNet服务集合
        /// </summary>
        /// <param name="services">服务集合</param>
        private static void AddAspNetServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
        }
    }
}
