using System;
using System.Collections.Generic;
using System.Linq;
using Bing.Modularity.Internal;
using Bing.Modularity.PlugIns;
using Bing.Utils.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Modularity
{
    /// <summary>
    /// 模块加载器
    /// </summary>
    public class ModuleLoader : IModuleLoader
    {
        /// <summary>
        /// 加载模块列表
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="startupModuleType">应用程序启动(入口)模块类型</param>
        /// <param name="plugInSources">插件源列表</param>
        public IBingModuleDescriptor[] LoadModules(IServiceCollection services, Type startupModuleType, PlugInSourceList plugInSources)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (startupModuleType == null)
                throw new ArgumentNullException(nameof(startupModuleType));
            if (plugInSources == null)
                throw new ArgumentNullException(nameof(plugInSources));

            var modules = GetDescriptors(services, startupModuleType, plugInSources);
            ConfigureServices(modules,services);
            return modules.ToArray();
        }

        /// <summary>
        /// 获取模块描述列表
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="startupModuleType">应用程序启动(入口)模块类型</param>
        /// <param name="plugInSources">插件源列表</param>
        private List<IBingModuleDescriptor> GetDescriptors(IServiceCollection services, Type startupModuleType,
            PlugInSourceList plugInSources)
        {
            var modules = new List<BingModuleDescriptor>();
            FillModules(modules, services, startupModuleType, plugInSources);
            SetDependencies(modules);
            return modules.Cast<IBingModuleDescriptor>().ToList();
        }

        /// <summary>
        /// 填充模块列表
        /// </summary>
        /// <param name="modules">模块描述列表</param>
        /// <param name="services">服务集合</param>
        /// <param name="startupModuleType">应用程序启动(入口)模块类型</param>
        /// <param name="plugInSources">插件源列表</param>
        protected virtual void FillModules(List<BingModuleDescriptor> modules, IServiceCollection services,
            Type startupModuleType, PlugInSourceList plugInSources)
        {
            // 所有模块将从启动模块开始
            foreach (var moduleType in BingModuleHelper.FindAllModuleTypes(startupModuleType))
                modules.Add(CreateModuleDescriptor(services, moduleType));

            // 插件模块列表
            foreach (var moduleType in plugInSources.GetAllModules())
            {
                if (modules.Any(x => x.Type == moduleType))
                    continue;
                modules.Add(CreateModuleDescriptor(services, moduleType, true));
            }
        }

        /// <summary>
        /// 设置依赖列表
        /// </summary>
        /// <param name="modules">模块描述列表</param>
        protected virtual void SetDependencies(List<BingModuleDescriptor> modules)
        {
            foreach (var module in modules)
                SetDependencies(modules, module);
        }

        /// <summary>
        /// 按依赖关系进行排序
        /// </summary>
        /// <param name="modules">模块描述列表</param>
        /// <param name="startupModuleType">应用程序启动(入口)模块类型</param>
        protected virtual List<IBingModuleDescriptor> SortByDependency(List<IBingModuleDescriptor> modules,
            Type startupModuleType)
        {
            var sortedModules = modules.SortByDependencies(m => m.Dependencies);
            sortedModules.MoveItem(m => m.Type == startupModuleType, modules.Count - 1);
            return sortedModules;
        }

        /// <summary>
        /// 创建模块描述
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="moduleType">模块类型</param>
        /// <param name="isLoadedAsPlugIn">是否作为插件加载</param>
        protected virtual BingModuleDescriptor CreateModuleDescriptor(IServiceCollection services, Type moduleType,
            bool isLoadedAsPlugIn = false) =>
            new BingModuleDescriptor(moduleType, CreateAndRegisterModule(services, moduleType),
                isLoadedAsPlugIn);

        /// <summary>
        /// 创建并注册模块
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="moduleType">模块类型</param>
        protected virtual IBingModule CreateAndRegisterModule(IServiceCollection services, Type moduleType)
        {
            var module = (IBingModule)Activator.CreateInstance(moduleType);
            services.AddSingleton(moduleType, module);
            return module;
        }

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="modules">模块描述列表</param>
        /// <param name="services">服务集合</param>
        protected virtual void ConfigureServices(List<IBingModuleDescriptor> modules, IServiceCollection services)
        {
            var context = new ServiceConfigurationContext(services);
            services.AddSingleton(context);
            foreach (var module in modules)
            {
                if (module.Instance is BingModule bingModule)
                    bingModule.ServiceConfigurationContext = context;
            }

            // 预配置服务
            foreach (var module in modules.Where(x => x.Instance is IPreConfigureServices))
                ((IPreConfigureServices)module.Instance).PreConfigureServices(context);
            // 配置服务
            foreach (var module in modules)
            {
                if (module.Instance is BingModule bingModule)
                {
                    if (!bingModule.SkipAutoServiceRegistration)
                        services.AddAssembly(module.Type.Assembly);
                }
                module.Instance.ConfigureServices(context);
            }
            // 后配置服务
            foreach (var module in modules.Where(x => x.Instance is IPostConfigureServices))
                ((IPostConfigureServices)module.Instance).PostConfigureServices(context);

            // 重置服务配置上下文
            foreach (var module in modules)
            {
                if (module.Instance is BingModule bingModule)
                    bingModule.ServiceConfigurationContext = null;
            }
        }

        /// <summary>
        /// 设置依赖关系列表
        /// </summary>
        /// <param name="modules">模块描述列表</param>
        /// <param name="module">模块描述</param>
        protected virtual void SetDependencies(List<BingModuleDescriptor> modules, BingModuleDescriptor module)
        {
            foreach (var dependedModuleType in BingModuleHelper.FindDependedModuleTypes(module.Type))
            {
                var dependedModule = modules.FirstOrDefault(x => x.Type == dependedModuleType);
                if (dependedModule == null)
                    throw new BingException($"无法从 {module.Type.AssemblyQualifiedName} 中找到依赖的模块 {dependedModuleType.AssemblyQualifiedName}");
                module.AddDependency(dependedModule);
            }
        }
    }
}
