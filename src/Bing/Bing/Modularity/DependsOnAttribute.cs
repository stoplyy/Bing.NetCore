using System;

namespace Bing.Modularity
{
    /// <summary>
    /// Bing 模块依赖
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute, IDependedTypesProvider
    {
        /// <summary>
        /// 初始化一个<see cref="DependsOnAttribute"/>类型的实例
        /// </summary>
        /// <param name="dependedTypes">依赖类型集合</param>
        public DependsOnAttribute(params Type[] dependedTypes) => DependedTypes = dependedTypes ?? new Type[0];

        /// <summary>
        /// 依赖类型集合
        /// </summary>
        public Type[] DependedTypes { get; }

        /// <summary>
        /// 获取依赖类型列表
        /// </summary>
        public Type[] GetDependedTypes() => DependedTypes;
    }
}
