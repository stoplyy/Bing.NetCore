using System;
using System.Collections.Generic;

namespace Bing.DependencyInjection
{
    /// <summary>
    /// 服务公开上下文
    /// </summary>
    public class OnServiceExposingContext : IOnServiceExposingContext
    {
        /// <summary>
        /// 初始化一个<see cref="OnServiceExposingContext"/>类型的实例
        /// </summary>
        /// <param name="implementationType">实现类型</param>
        /// <param name="exposedTypes">公开类型列表</param>
        public OnServiceExposingContext(Type implementationType, List<Type> exposedTypes)
        {
            ImplementationType = implementationType ?? throw new ArgumentException(nameof(implementationType));
            ExposedTypes = exposedTypes ?? throw new ArgumentException(nameof(exposedTypes));
        }

        /// <summary>
        /// 实现类型
        /// </summary>
        public Type ImplementationType { get; }

        /// <summary>
        /// 公开类型列表
        /// </summary>
        public List<Type> ExposedTypes { get; }
    }
}
