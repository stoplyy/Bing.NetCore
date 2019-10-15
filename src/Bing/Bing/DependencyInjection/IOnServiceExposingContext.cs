using System;
using System.Collections.Generic;

namespace Bing.DependencyInjection
{
    /// <summary>
    /// 服务公开上下文
    /// </summary>
    public interface IOnServiceExposingContext
    {
        /// <summary>
        /// 实现类型
        /// </summary>
        Type ImplementationType { get; }

        /// <summary>
        /// 公开类型列表
        /// </summary>
        List<Type> ExposedTypes { get; }
    }
}
