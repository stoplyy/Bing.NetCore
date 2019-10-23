namespace Bing
{
    /// <summary>
    /// 应用程序初始化
    /// </summary>
    public interface IOnApplicationInitialization
    {
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        void OnApplicationInitialization(ApplicationInitializationContext context);
    }
}
