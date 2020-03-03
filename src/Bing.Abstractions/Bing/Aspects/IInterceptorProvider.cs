using System;
using System.Reflection;

namespace Bing.Aspects
{
    /// <summary>
    /// 拦截器提供程序
    /// </summary>
    public interface IInterceptorProvider
    {
        /// <summary>
        /// 拦截器类型
        /// </summary>
        Type InterceptorType { get; }

        /// <summary>
        /// 能否拦截
        /// </summary>
        /// <param name="method">方法信息</param>
        bool CanIntercept(MethodInfo method);
    }
}
