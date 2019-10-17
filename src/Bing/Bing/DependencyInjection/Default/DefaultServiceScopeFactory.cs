using Microsoft.Extensions.DependencyInjection;

namespace Bing.DependencyInjection.Default
{
    /// <summary>
    /// 默认服务作用域工厂
    /// </summary>
    [ExposeService(typeof(IHybridServiceScopeFactory), typeof(DefaultServiceScopeFactory))]
    public class DefaultServiceScopeFactory : IHybridServiceScopeFactory, ITransientDependency
    {
        /// <summary>
        /// 初始化一个<see cref="DefaultServiceScopeFactory"/>类型的实例
        /// </summary>
        /// <param name="factory">服务作用域工厂</param>
        public DefaultServiceScopeFactory(IServiceScopeFactory factory) => Factory = factory;

        /// <summary>
        /// 服务作用域工厂
        /// </summary>
        protected IServiceScopeFactory Factory { get; }

        /// <summary>
        /// 创建作用域
        /// </summary>
        public IServiceScope CreateScope() => Factory.CreateScope();
    }
}
