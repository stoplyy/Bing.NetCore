using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bing.Modularity
{
    /// <summary>
    /// Bing 模块描述
    /// </summary>
    public interface IBingModuleDescriptor
    {
        /// <summary>
        /// 类型
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// 程序集
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Bing 模块实例
        /// </summary>
        IBingModule Instance { get; }

        /// <summary>
        /// 是否作为插件加载
        /// </summary>
        bool IsLoadedAsPlugIn { get; }

        /// <summary>
        /// 依赖关系列表
        /// </summary>
        IReadOnlyList<IBingModuleDescriptor> Dependencies { get; }
    }
}
