namespace Bing.Modularity
{
    /// <summary>
    /// 应用程序预初始化模块生命周期构造器
    /// </summary>
    public class OnPreApplicationInitializationModuleLifecycleContributor : ModuleLifecycleContributorBase
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <param name="module">模块</param>
        public override void Initialize(ApplicationInitializationContext context, IBingModule module) => (module as IOnPreApplicationInitialization)?.OnPreApplicationInitialization(context);
    }

    /// <summary>
    /// 应用程序初始化模块生命周期构造器
    /// </summary>
    public class OnApplicationInitializationModuleLifecycleContributor : ModuleLifecycleContributorBase
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <param name="module">模块</param>
        public override void Initialize(ApplicationInitializationContext context, IBingModule module) => (module as IOnApplicationInitialization)?.OnApplicationInitialization(context);
    }

    /// <summary>
    /// 应用程序后初始化模块生命周期构造器
    /// </summary>
    public class OnPostApplicationInitializationModuleLifecycleContributor : ModuleLifecycleContributorBase
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <param name="module">模块</param>
        public override void Initialize(ApplicationInitializationContext context, IBingModule module) => (module as IOnPostApplicationInitialization)?.OnPostApplicationInitialization(context);
    }

    /// <summary>
    /// 应用程序关闭模块生命周期构造器
    /// </summary>
    public class OnApplicationShutdownModuleLifecycleContributor : ModuleLifecycleContributorBase
    {
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="context">应用程序关闭上下文</param>
        /// <param name="module">模块</param>
        public override void Shutdown(ApplicationShutdownContext context, IBingModule module) => (module as IOnApplicationShutdown)?.OnApplicationShutdown(context);
    }
}
