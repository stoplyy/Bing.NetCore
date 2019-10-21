using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bing.Finders;
using Bing.Modularity;

namespace Bing.Reflections
{
    /// <summary>
    /// 程序集查找器
    /// </summary>
    public class AssemblyFinder : FinderBase<Assembly>, IAssemblyFinder
    {
        /// <summary>
        /// 模块容器
        /// </summary>
        private readonly IModuleContainer _moduleContainer;

        /// <summary>
        /// 初始化一个<see cref="AssemblyFinder"/>类型的实例
        /// </summary>
        /// <param name="moduleContainer">模块容器</param>
        public AssemblyFinder(IModuleContainer moduleContainer)
        {
            _moduleContainer = moduleContainer;
        }

        /// <summary>
        /// 重写已实现所有项的查找
        /// </summary>
        protected override Assembly[] FindAllItems()
        {
            var assemblies = new List<Assembly>();
            foreach (var module in _moduleContainer.Modules)
                assemblies.Add(module.Type.Assembly);
            return assemblies.Distinct().ToArray();
        }
    }
}
