namespace Bing.Modularity
{
    /// <summary>
    /// 模块管理器
    /// </summary>
    public interface IModuleManager
    {
        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        void InitializeModules(ApplicationInitializationContext context);

        /// <summary>
        /// 关闭模块
        /// </summary>
        /// <param name="context">应用程序关闭上下文</param>
        void ShutdownModules(ApplicationShutdownContext context);
    }
}
