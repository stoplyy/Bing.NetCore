namespace Bing.Modularity
{
    /// <summary>
    /// 模块生命周期构造器基类
    /// </summary>
    public abstract class ModuleLifecycleContributorBase : IModuleLifecycleContributor
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <param name="module">模块</param>
        public virtual void Initialize(ApplicationInitializationContext context, IBingModule module)
        {
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="context">应用程序关闭上下文</param>
        /// <param name="module">模块</param>
        public virtual void Shutdown(ApplicationShutdownContext context, IBingModule module)
        {
        }
    }
}
