using System;

namespace Bing.EventBus
{
    /// <summary>
    /// <see cref="IEventHandler"/> 事件处理器的可释放包装
    /// </summary>
    public interface IEventHandlerDisposeWrapper : IDisposable
    {
        /// <summary>
        /// 事件处理器
        /// </summary>
        IEventHandler EventHandler { get; }
    }
}
