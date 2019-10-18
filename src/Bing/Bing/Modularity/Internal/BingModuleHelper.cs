using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bing.Utils.Extensions;

namespace Bing.Modularity.Internal
{
    /// <summary>
    /// Bing模块操作辅助类
    /// </summary>
    internal static class BingModuleHelper
    {
        /// <summary>
        /// 查找所有模块类型列表
        /// </summary>
        /// <param name="startupModuleType">应用程序启动(入口)模块类型</param>
        public static List<Type> FindAllModuleTypes(Type startupModuleType)
        {
            var moduleTypes = new List<Type>();
            AddModuleAndDependenciesResursively(moduleTypes, startupModuleType);
            return moduleTypes;
        }

        /// <summary>
        /// 查找依赖模块类型列表
        /// </summary>
        /// <param name="moduleType">模块类型</param>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            BingModule.CheckBingModuleType(moduleType);

            var dependencies = new List<Type>();
            var dependencyDescriptors = moduleType
                .GetCustomAttributes()
                .OfType<IDependedTypesProvider>();

            foreach (var descriptor in dependencyDescriptors)
            {
                foreach (var dependedModuleType in descriptor.GetDependedTypes())
                    dependencies.AddIfNotContains(dependedModuleType);
            }
            return dependencies;
        }

        /// <summary>
        /// 递归添加模块以及依赖项
        /// </summary>
        /// <param name="moduleTypes">模块类型列表</param>
        /// <param name="moduleType">模块类型</param>
        private static void AddModuleAndDependenciesResursively(List<Type> moduleTypes, Type moduleType)
        {
            BingModule.CheckBingModuleType(moduleType);
            if (moduleTypes.Contains(moduleType))
                return;
            moduleTypes.Add(moduleType);
            foreach (var dependedModuleType in FindDependedModuleTypes(moduleType))
                AddModuleAndDependenciesResursively(moduleTypes, dependedModuleType);
        }
    }
}
