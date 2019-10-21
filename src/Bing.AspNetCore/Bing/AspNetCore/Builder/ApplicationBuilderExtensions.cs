using System;
using Bing.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.AspNetCore.Builder
{
    /// <summary>
    /// 应用程序构建器(<see cref="IApplicationBuilder"/>) 扩展
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 初始化应用程序
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        public static void InitializeApplication(this IApplicationBuilder app)
        {
            if (null == app)
                throw new ArgumentNullException(nameof(app));
            app.ApplicationServices.GetRequiredService<ObjectAccessor<IApplicationBuilder>>().Value = app;
            app.ApplicationServices.GetRequiredService<IBingApplicationWithExternalServiceProvider>()
                .Initialize(app.ApplicationServices);
        }
    }
}
