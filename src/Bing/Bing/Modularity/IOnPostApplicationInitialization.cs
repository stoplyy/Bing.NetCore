namespace Bing.Modularity
{
    /// <summary>
    /// 应用程序后初始化
    /// </summary>
    public interface IOnPostApplicationInitialization
    {
        /// <summary>
        /// 应用程序后初始化
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        void OnPostApplicationInitialization(ApplicationInitializationContext context);
    }
}
