using System;

namespace Bing.Aspects
{
    /// <summary>
    /// 拦截器提供程序 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class InterceptorProviderAttribute : Attribute
    {
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// 初始化一个<see cref="InterceptorProviderAttribute"/>类型的实例
        /// </summary>
        /// <param name="type">拦截器提供程序类型</param>
        public InterceptorProviderAttribute(Type type)
        {
            if (!typeof(IInterceptorProvider).IsAssignableFrom(type))
                throw new ArgumentException($"Type '{type.FullName}' is not from interface {nameof(IInterceptorProvider)}");
            Type = type;
        }
    }
}
