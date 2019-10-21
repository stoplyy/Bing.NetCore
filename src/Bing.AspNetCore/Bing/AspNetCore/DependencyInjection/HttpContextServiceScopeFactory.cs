using System;
using Bing.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.AspNetCore.DependencyInjection
{
    /// <summary>
    /// Http上下文服务作用域工厂
    /// </summary>
    [ExposeService(typeof(IHybridServiceScopeFactory), typeof(HttpContextServiceScopeFactory))]
    [Dependency(ServiceLifetime.Transient, ReplaceExisting = true)]
    public class HttpContextServiceScopeFactory : IHybridServiceScopeFactory, ITransientDependency
    {
        /// <summary>
        /// 初始化一个<see cref="HttpContextServiceScopeFactory"/>类型的实例
        /// </summary>
        /// <param name="httpContextAccessor">Http上下文访问器</param>
        /// <param name="serviceScopeFactory">服务作用域工厂</param>
        public HttpContextServiceScopeFactory(
            IHttpContextAccessor httpContextAccessor
            , IServiceScopeFactory serviceScopeFactory)
        {
            HttpContextAccessor = httpContextAccessor;
            ServiceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// Http上下文访问器
        /// </summary>
        protected IHttpContextAccessor HttpContextAccessor { get; }

        /// <summary>
        /// 服务作用域工厂
        /// </summary>
        protected IServiceScopeFactory ServiceScopeFactory { get; }

        /// <summary>
        /// 创建作用域
        /// </summary>
        public virtual IServiceScope CreateScope()
        {
            var httpContext = HttpContextAccessor.HttpContext;
            if (httpContext == null)
                return ServiceScopeFactory.CreateScope();
            return new NonDisposedHttpContextServiceScope(httpContext.RequestServices);
        }

        /// <summary>
        /// 不释放Http上下文服务作用域
        /// </summary>
        protected class NonDisposedHttpContextServiceScope : IServiceScope
        {
            /// <summary>
            /// 初始化一个<see cref="NonDisposedHttpContextServiceScope"/>类型的实例
            /// </summary>
            /// <param name="serviceProvider">服务提供程序</param>
            public NonDisposedHttpContextServiceScope(IServiceProvider serviceProvider) => ServiceProvider = serviceProvider;

            /// <summary>
            /// 服务提供程序
            /// </summary>
            public IServiceProvider ServiceProvider { get; }

            /// <summary>
            /// 释放资源
            /// </summary>
            public void Dispose()
            {
            }
        }
    }
}
