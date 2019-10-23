using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using AspectCore.Extensions.AspectScope;
using AspectCore.Extensions.DependencyInjection;
using Bing.AspNetCore.DependencyInjection;
using Bing.DependencyInjection;
using Bing.Modularity;
using Bing.Utils.Extensions;
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
            context.Services.AddSingleton<IScopeServiceResolver, RequestScopedServiceResolver>();
        }

        /// <summary>
        /// 注册AspNet服务集合
        /// </summary>
        /// <param name="services">服务集合</param>
        private static void AddAspNetServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            // 注入当前用户，替换Thread.CurrentPrincipal的作用
            services.AddTransient<System.Security.Principal.IPrincipal>(provider =>
            {
                var accessor = provider.GetService<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
                return accessor?.HttpContext?.User;
            });
            // 注入用户会话
            services.AddSingleton<Bing.Sessions.ISession, Bing.Sessions.Session>();
            // 注册编码
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            services.ConfigureDynamicProxy(config =>
            {
                config.EnableParameterAspect();
                config.NonAspectPredicates.Add(t =>
                    Bing.Utils.Helpers.Reflection.GetTopBaseType(t.DeclaringType).SafeString() ==
                    "Microsoft.EntityFrameworkCore.DbContext");
            });
            services.AddScoped<IAspectScheduler, ScopeAspectScheduler>();
            services.AddScoped<IAspectBuilderFactory, ScopeAspectBuilderFactory>();
            services.AddScoped<IAspectContextFactory, ScopeAspectContextFactory>();
        }
    }
}
