using System;

namespace Bing.DependencyInjection
{
    /// <summary>
    /// 忽略注入。标注了此特性的类，将忽略依赖注入的自动映射
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class IgnoreDependencyAttribute : Attribute
    {
    }
}
