using System;
using AspectCore.DynamicProxy;
using Bing.Collections;

namespace Bing.DependencyInjection
{
    /// <summary>
    /// 服务注册上下文
    /// </summary>
    public class OnServiceRegisterdContext : IOnServiceRegistredContext
    {
        /// <summary>
        /// 初始化一个<see cref="OnServiceRegisterdContext"/>类型的实例
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">实现类型</param>
        public OnServiceRegisterdContext(Type serviceType, Type implementationType)
        {
            ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            ImplementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
            Interceptors = new TypeList<IInterceptor>();
        }

        /// <summary>
        /// 拦截器类型列表
        /// </summary>
        public ITypeList<IInterceptor> Interceptors { get; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// 实现类型
        /// </summary>
        public Type ImplementationType { get; }

    }
}
