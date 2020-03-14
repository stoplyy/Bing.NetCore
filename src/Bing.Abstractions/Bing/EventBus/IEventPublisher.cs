using System;
using System.Threading.Tasks;

namespace Bing.EventBus
{
    /// <summary>
    /// 事件发布者
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件数据类型</typeparam>
        /// <param name="eventData">事件数据</param>
        Task PublishAsync<TEvent>(TEvent eventData) where TEvent : class;

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="eventType">事件数据类型</param>
        /// <param name="eventData">事件数据</param>
        Task PublishAsync(Type eventType, object eventData);
    }
}
