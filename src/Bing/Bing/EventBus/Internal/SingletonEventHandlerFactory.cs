using System.Collections.Generic;
using System.Linq;

namespace Bing.EventBus.Internal
{
    /// <summary>
    /// 单例生命周期的事件处理器工厂
    /// </summary>
    internal class SingletonEventHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// 事件处理器实例
        /// </summary>
        public IEventHandler HandlerInstance { get; }

        /// <summary>
        /// 初始化一个<see cref="SingletonEventHandlerFactory"/>类型的实例
        /// </summary>
        /// <param name="handler">事件处理器</param>
        public SingletonEventHandlerFactory(IEventHandler handler) => HandlerInstance = handler;

        /// <summary>
        /// 获取事件处理器
        /// </summary>
        public IEventHandlerDisposeWrapper GetHandler() => new EventHandlerDisposeWrapper(HandlerInstance);

        /// <summary>
        /// 是否在事件处理器工厂列表
        /// </summary>
        /// <param name="handlerFactories">事件处理器工厂列表</param>
        public bool IsInFactories(List<IEventHandlerFactory> handlerFactories) =>
            handlerFactories
                .OfType<SingletonEventHandlerFactory>()
                .Any(f => f.HandlerInstance == HandlerInstance);
    }
}
