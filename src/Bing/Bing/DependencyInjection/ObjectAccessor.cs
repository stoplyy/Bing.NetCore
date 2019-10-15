namespace Bing.DependencyInjection
{
    /// <summary>
    /// 对象访问器
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class ObjectAccessor<T> : IObjectAccessor<T>
    {
        /// <summary>
        /// 初始化一个<see cref="ObjectAccessor{T}"/>类型的实例
        /// </summary>
        public ObjectAccessor() { }

        /// <summary>
        /// 初始化一个<see cref="ObjectAccessor{T}"/>类型的实例
        /// </summary>
        /// <param name="obj">对象</param>
        public ObjectAccessor(T obj) => Value = obj;

        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }
    }
}
