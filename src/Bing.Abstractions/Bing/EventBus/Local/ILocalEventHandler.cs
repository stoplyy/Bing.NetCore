using System.Threading.Tasks;

namespace Bing.EventBus.Local
{
    /// <summary>
    /// 本地事件处理器
    /// </summary>
    /// <typeparam name="TEvent">事件数据类型</typeparam>
    public interface ILocalEventHandler<in TEvent> : IEventHandler
    {
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData">事件数据</param>
        Task HandleAsync(TEvent eventData);
    }
}
