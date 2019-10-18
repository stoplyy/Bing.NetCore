using System;
using System.Linq;
using Bing.Modularity.Internal;

namespace Bing.Modularity.PlugIns
{
    /// <summary>
    /// 插件源(<see cref="IPlugInSource"/>) 扩展
    /// </summary>
    public static class PlugInSourceExtensions
    {
        /// <summary>
        /// 获取模块所有依赖关系
        /// </summary>
        /// <param name="plugInSource">插件源</param>
        public static Type[] GetModulesWithAllDependencies(this IPlugInSource plugInSource)
        {
            if (plugInSource == null)
                throw new ArgumentException(nameof(plugInSource));
            return plugInSource
                .GetModules()
                .SelectMany(BingModuleHelper.FindAllModuleTypes)
                .Distinct()
                .ToArray();
        }
    }
}
