using Bing.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bing
{
    /// <summary>
    /// 应用程序初始化上下文(<see cref="ApplicationInitializationContext"/>) 扩展
    /// </summary>
    public static class ApplicationInitializationContextExtensions
    {
        /// <summary>
        /// 获取应用程序构建器
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        public static IApplicationBuilder GetApplicationBuilder(this ApplicationInitializationContext context) => context.ServiceProvider.GetRequiredService<IObjectAccessor<IApplicationBuilder>>().Value;

        /// <summary>
        /// 获取环境变量
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        public static IHostingEnvironment GetEnvironment(this ApplicationInitializationContext context) => context.ServiceProvider.GetRequiredService<IHostingEnvironment>();

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        public static IConfiguration GetConfiguration(this ApplicationInitializationContext context) => context.ServiceProvider.GetRequiredService<IConfiguration>();

        /// <summary>
        /// 获取日志工厂
        /// </summary>
        /// <param name="context">应用程序㞊上下文</param>
        public static ILoggerFactory GetLoggerFactory(this ApplicationInitializationContext context) => context.ServiceProvider.GetRequiredService<ILoggerFactory>();
    }
}
