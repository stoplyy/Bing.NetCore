using System;

namespace Bing.DependencyInjection
{
    /// <summary>
    /// 公开服务类型提供程序
    /// </summary>
    public interface IExposedServiceTypesProvider
    {
        /// <summary>
        /// 获取公开服务类型列表
        /// </summary>
        /// <param name="targetType">目标类型</param>
        Type[] GetExposedServiceTypes(Type targetType);
    }
}
