using System;
using System.Collections.Generic;
using System.Linq;

namespace Bing.EventBus.Internal
{
    /// <summary>
    /// 即时生命周期的事件处理器工厂
    /// </summary>
    /// <typeparam name="THandler">事件处理器类型</typeparam>
    internal class TransientEventHandlerFactory<THandler> : TransientEventHandlerFactory, IEventHandlerFactory 
        where THandler : IEventHandler, new()
    {
        /// <summary>
        /// 初始化一个<see cref="TransientEventHandlerFactory{THandler}"/>类型的实例
        /// </summary>
        public TransientEventHandlerFactory() : base(typeof(THandler)) { }

        /// <summary>
        /// 创建事件处理器
        /// </summary>
        protected override IEventHandler CreateHandler() => new THandler();
    }

    /// <summary>
    /// 即时生命周期的事件处理器工厂
    /// </summary>
    internal class TransientEventHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// 事件处理器类型
        /// </summary>
        public Type HandlerType { get; }

        /// <summary>
        /// 初始化一个<see cref="TransientEventHandlerFactory"/>类型的实例
        /// </summary>
        /// <param name="handlerType">事件处理器类型</param>
        public TransientEventHandlerFactory(Type handlerType) => HandlerType = handlerType;

        /// <summary>
        /// 获取事件处理器
        /// </summary>
        public IEventHandlerDisposeWrapper GetHandler()
        {
            var handler = CreateHandler();
            return new EventHandlerDisposeWrapper(handler, () => (handler as IDisposable)?.Dispose());
        }

        /// <summary>
        /// 是否在事件处理器工厂列表
        /// </summary>
        /// <param name="handlerFactories">事件处理器工厂列表</param>
        public bool IsInFactories(List<IEventHandlerFactory> handlerFactories) =>
            handlerFactories
                .OfType<TransientEventHandlerFactory>()
                .Any(f => f.HandlerType == HandlerType);

        /// <summary>
        /// 创建事件处理器
        /// </summary>
        protected virtual IEventHandler CreateHandler() => (IEventHandler)Activator.CreateInstance(HandlerType);
    }
}
