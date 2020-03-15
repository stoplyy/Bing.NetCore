using Bing.Dependency;

namespace Bing.Helpers
{
    /// <summary>
    /// 容器操作
    /// </summary>
    public static class Ioc
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        public static T Create<T>()
        {
            return ServiceLocator.Instance.GetService<T>();
        }
    }
}
