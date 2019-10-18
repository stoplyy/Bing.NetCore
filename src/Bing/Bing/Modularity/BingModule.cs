using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Modularity
{
    /// <summary>
    /// Bing 模块
    /// </summary>
    public abstract class BingModule :
        IBingModule
        , IOnPreApplicationInitialization
        , IOnApplicationInitialization
        , IOnPostApplicationInitialization
        , IOnApplicationShutdown
        , IPreConfigureServices
        , IPostConfigureServices
    {
        /// <summary>
        /// 是否跳过自动服务诸恶
        /// </summary>
        protected internal bool SkipAutoServiceRegistration { get; protected set; }

        /// <summary>
        /// 服务配置上下文
        /// </summary>
        private ServiceConfigurationContext _serviceConfigurationContext;

        /// <summary>
        /// 服务配置上下文
        /// </summary>
        protected internal ServiceConfigurationContext ServiceConfigurationContext
        {
            get
            {
                if (_serviceConfigurationContext == null)
                    throw new BingException(
                        $"{nameof(ServiceConfigurationContext)} 仅在 {nameof(ConfigureServices)}、{nameof(PreConfigureServices)} 以及 {nameof(PostConfigureServices)} 方法中。");
                return _serviceConfigurationContext;
            }
            internal set => _serviceConfigurationContext = value;
        }

        /// <summary>
        /// 预配置服务集合
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        public virtual void PreConfigureServices(ServiceConfigurationContext context)
        {
        }

        /// <summary>
        /// 配置服务集合
        /// </summary>
        /// <param name="context">配置服务上下文</param>
        public virtual void ConfigureServices(ServiceConfigurationContext context)
        {
        }

        /// <summary>
        /// 后配置服务集合
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        public virtual void PostConfigureServices(ServiceConfigurationContext context)
        {
        }

        /// <summary>
        /// 应用程序预初始化
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        public virtual void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
        }

        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        public virtual void OnApplicationInitialization(ApplicationInitializationContext context)
        {
        }

        /// <summary>
        /// 应用程序后初始化
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        public virtual void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
        }

        /// <summary>
        /// 应用程序关闭
        /// </summary>
        /// <param name="context">应用程序关闭上下文</param>
        public virtual void OnApplicationShutdown(ApplicationShutdownContext context)
        {
        }

        /// <summary>
        /// 是否Bing模块
        /// </summary>
        /// <param name="type">类型</param>
        public static bool IsBingModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsClass &&
                   !typeInfo.IsAbstract &&
                   !typeInfo.IsGenericType &&
                   typeof(IBingModule).GetTypeInfo().IsAssignableFrom(type);
        }

        /// <summary>
        /// 检查Bing模块类型
        /// </summary>
        /// <param name="moduleType">模块类型</param>
        internal static void CheckBingModuleType(Type moduleType)
        {
            if (!IsBingModule(moduleType))
                throw new ArgumentException($"给定类型不是 Bing模块: {moduleType.AssemblyQualifiedName}");
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <typeparam name="TOptions">选项配置类型</typeparam>
        /// <param name="configureOptions">配置选项操作</param>
        protected void Configure<TOptions>(Action<TOptions> configureOptions) where TOptions : class => ServiceConfigurationContext.Services.Configure(configureOptions);

        /// <summary>
        /// 配置
        /// </summary>
        /// <typeparam name="TOptions">选项配置类型</typeparam>
        /// <param name="name">名称</param>
        /// <param name="configureOptions">配置选项操作</param>
        protected void Configure<TOptions>(string name, Action<TOptions> configureOptions) where TOptions : class => ServiceConfigurationContext.Services.Configure(name, configureOptions);

        /// <summary>
        /// 配置
        /// </summary>
        /// <typeparam name="TOptions">选型配置类型</typeparam>
        /// <param name="configuration">配置</param>
        /// <param name="configureBinder">配置绑定器操作</param>
        protected void Configure<TOptions>(IConfiguration configuration, Action<BinderOptions> configureBinder)
            where TOptions : class =>
            ServiceConfigurationContext.Services.Configure<TOptions>(configuration, configureBinder);

        /// <summary>
        /// 配置
        /// </summary>
        /// <typeparam name="TOptions">选型配置类型</typeparam>
        /// <param name="name">名称</param>
        /// <param name="configuration">配置</param>
        protected void Configure<TOptions>(string name, IConfiguration configuration) where TOptions : class => ServiceConfigurationContext.Services.Configure<TOptions>(name, configuration);

        /// <summary>
        /// 预配置
        /// </summary>
        /// <typeparam name="TOptions">选项配置类型</typeparam>
        /// <param name="configureOptions">配置选项操作</param>
        protected void PreConfigure<TOptions>(Action<TOptions> configureOptions) where TOptions : class => ServiceConfigurationContext.Services.PreConfigure(configureOptions);
    }
}
