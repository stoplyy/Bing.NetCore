using System;
using System.Collections.Generic;
using System.Linq;

namespace Bing.Modularity.PlugIns
{
    /// <summary>
    /// 插件源列表
    /// </summary>
    public class PlugInSourceList : List<IPlugInSource>
    {
        /// <summary>
        /// 获取所有模块
        /// </summary>
        internal Type[] GetAllModules()
        {
            return this
                .SelectMany(plugInSource => plugInSource.GetModulesWithAllDependencies())
                .Distinct()
                .ToArray();
        }
    }
}
