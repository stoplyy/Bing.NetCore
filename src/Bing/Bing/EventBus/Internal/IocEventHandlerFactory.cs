using System;
using System.Collections.Generic;
using System.Linq;
using Bing.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.EventBus.Internal
{
    /// <summary>
    /// 依赖注入事件处理器工厂
    /// </summary>
    internal class IocEventHandlerFactory : IEventHandlerFactory, IDisposable
    {
        /// <summary>
        /// 事件处理器类型
        /// </summary>
        public Type HandlerType { get; }

        /// <summary>
        /// 混合服务作用域工厂
        /// </summary>
        public IHybridServiceScopeFactory ScopeFactory { get; }

        /// <summary>
        /// 初始化一个<see cref="IocEventHandlerFactory"/>类型的实例
        /// </summary>
        /// <param name="serviceScopeFactory">混合服务作用域工厂</param>
        /// <param name="handlerType">事件处理器类型</param>
        public IocEventHandlerFactory(IHybridServiceScopeFactory serviceScopeFactory, Type handlerType)
        {
            ScopeFactory = serviceScopeFactory;
            HandlerType = handlerType;
        }

        /// <summary>
        /// 获取事件处理器
        /// </summary>
        public IEventHandlerDisposeWrapper GetHandler()
        {
            var scope = ScopeFactory.CreateScope();
            return new EventHandlerDisposeWrapper((IEventHandler)scope.ServiceProvider.GetRequiredService(HandlerType), () => scope.Dispose());
        }

        /// <summary>
        /// 是否在事件处理器工厂列表
        /// </summary>
        /// <param name="handlerFactories">事件处理器工厂列表</param>
        public bool IsInFactories(List<IEventHandlerFactory> handlerFactories) =>
            handlerFactories
                .OfType<IocEventHandlerFactory>()
                .Any(f => f.HandlerType == HandlerType);

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() { }
    }
}
