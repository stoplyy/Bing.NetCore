using System;

namespace Bing
{
    /// <summary>
    /// Bing 应用程序外部服务提供程序
    /// </summary>
    public interface IBingApplicationWithExternalServiceProvider : IBingApplication
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="serviceProvider">服务提供程序</param>
        void Initialize(IServiceProvider serviceProvider);
    }
}
