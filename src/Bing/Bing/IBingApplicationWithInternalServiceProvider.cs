namespace Bing
{
    /// <summary>
    /// Bing 应用程序内部服务提供程序
    /// </summary>
    public interface IBingApplicationWithInternalServiceProvider : IBingApplication
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize();
    }
}
