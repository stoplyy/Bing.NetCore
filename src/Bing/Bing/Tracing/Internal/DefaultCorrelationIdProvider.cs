using System;
using Bing.DependencyInjection;

namespace Bing.Tracing.Internal
{
    /// <summary>
    /// 默认跟踪关联ID提供程序
    /// </summary>
    internal class DefaultCorrelationIdProvider : ICorrelationIdProvider, ISingletonDependency
    {
        /// <summary>
        /// 跟踪关联ID提供程序
        /// </summary>
        public string Get() => CreateNewCorrelationId();

        /// <summary>
        /// 创建新跟踪关联ID
        /// </summary>
        protected virtual string CreateNewCorrelationId() => Guid.NewGuid().ToString("N");
    }
}
