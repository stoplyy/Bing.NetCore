using System;

namespace Bing.EventBus.Distributed
{
    /// <summary>
    /// 分布式事件总线
    /// </summary>
    public interface IDistributedEventBus : IEventBus
    {
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="TEvent">事件数据类型</typeparam>
        /// <param name="handler">分布式事件处理器</param>
        IDisposable Subscribe<TEvent>(IDistributedEventHandler<TEvent> handler) where TEvent : class;
    }
}
