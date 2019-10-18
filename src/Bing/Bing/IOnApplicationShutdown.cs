namespace Bing
{
    /// <summary>
    /// 应用程序关闭
    /// </summary>
    public interface IOnApplicationShutdown
    {
        /// <summary>
        /// 应用程序关闭
        /// </summary>
        /// <param name="context">应用程序关闭上下文</param>
        void OnApplicationShutdown(ApplicationShutdownContext context);
    }
}
