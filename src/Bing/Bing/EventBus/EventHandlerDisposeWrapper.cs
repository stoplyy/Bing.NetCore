using System;

namespace Bing.EventBus
{
    /// <summary>
    /// <see cref="IEventHandler"/> 事件处理器的可释放包装
    /// </summary>
    public class EventHandlerDisposeWrapper : IEventHandlerDisposeWrapper
    {
        /// <summary>
        /// 可释放的操作
        /// </summary>
        private readonly Action _disposeAction;

        /// <summary>
        /// 事件处理器
        /// </summary>
        public IEventHandler EventHandler { get; }

        /// <summary>
        /// 初始化一个<see cref="EventHandlerDisposeWrapper"/>类型的实例
        /// </summary>
        /// <param name="eventHandler">事件处理器</param>
        /// <param name="disposeAction">可释放的操作</param>
        public EventHandlerDisposeWrapper(IEventHandler eventHandler, Action disposeAction = null)
        {
            _disposeAction = disposeAction;
            EventHandler = eventHandler;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose() => _disposeAction?.Invoke();
    }
}
