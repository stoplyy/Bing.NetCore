namespace Bing.EventBus
{
    /// <summary>
    /// 事件数据（可继承的泛型参数）
    /// </summary>
    public interface IEventDataWithInheritableGenericArgument
    {
        /// <summary>
        /// 获取构造函数参数
        /// </summary>
        object[] GetConstructorArgs();
    }
}
