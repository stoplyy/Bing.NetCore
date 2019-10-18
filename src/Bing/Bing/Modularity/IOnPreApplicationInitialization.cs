namespace Bing.Modularity
{
    /// <summary>
    /// 应用程序预初始化
    /// </summary>
    public interface IOnPreApplicationInitialization
    {
        /// <summary>
        /// 应用程序预初始化
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        void OnPreApplicationInitialization(ApplicationInitializationContext context);
    }
}
