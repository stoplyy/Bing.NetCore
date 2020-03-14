using System.Threading.Tasks;

namespace Bing.EventBus.Distributed
{
    /// <summary>
    /// 分布式事件处理器
    /// </summary>
    /// <typeparam name="TEvent">事件数据类型</typeparam>
    public interface IDistributedEventHandler<in TEvent> : IEventHandler
    {
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData">事件数据</param>
        Task HandleAsync(TEvent eventData);
    }
}
