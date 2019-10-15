using System;
using AspectCore.DynamicProxy;
using Bing.Collections;

namespace Bing.DependencyInjection
{
    /// <summary>
    /// 服务注册上下文
    /// </summary>
    public interface IOnServiceRegistredContext
    {
        /// <summary>
        /// 拦截器类型列表
        /// </summary>
        ITypeList<IInterceptor> Interceptors { get; }

        /// <summary>
        /// 实现类型
        /// </summary>
        Type ImplementationType { get; }
    }
}
