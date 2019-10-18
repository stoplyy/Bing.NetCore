using System;
using System.Collections.Generic;
using System.Linq;
using Bing.Dependency;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bing.Modularity
{
    /// <summary>
    /// 模块管理器
    /// </summary>
    public class ModuleManager : IModuleManager, ISingletonDependency
    {
        /// <summary>
        /// 模块容器
        /// </summary>
        private readonly IModuleContainer _moduleContainer;

        /// <summary>
        /// 模块生命周期构造器列表
        /// </summary>
        private readonly IEnumerable<IModuleLifecycleContributor> _lifecycleContributors;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<ModuleManager> _logger;

        /// <summary>
        /// 初始化一个<see cref="ModuleManager"/>类型的实例
        /// </summary>
        /// <param name="moduleContainer">模块容器</param>
        /// <param name="logger">日志</param>
        /// <param name="options">模块生命周期选项配置</param>
        /// <param name="serviceProvider">服务提供程序</param>
        public ModuleManager(IModuleContainer moduleContainer
            , ILogger<ModuleManager> logger
            , IOptions<ModuleLifecycleOptions> options
            , IServiceProvider serviceProvider)
        {
            _moduleContainer = moduleContainer;
            _logger = logger;
            _lifecycleContributors = options.Value
                .Contributors
                .Select(serviceProvider.GetRequiredService)
                .Cast<IModuleLifecycleContributor>()
                .ToArray();
        }

        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        public void InitializeModules(ApplicationInitializationContext context)
        {
            LogListOfModules();
            foreach (var contributor in _lifecycleContributors)
            {
                foreach (var module in _moduleContainer.Modules)
                    contributor.Initialize(context, module.Instance);
            }
            _logger.LogInformation($"已初始化所有 Bing 模块.");
        }

        /// <summary>
        /// 日志输出模块列表
        /// </summary>
        private void LogListOfModules()
        {
            _logger.LogInformation("已加载 Bing 模块:");
            foreach (var module in _moduleContainer.Modules)
                _logger.LogInformation($"- {module.Type.FullName}");
        }

        /// <summary>
        /// 关闭模块
        /// </summary>
        /// <param name="context">应用程序关闭上下文</param>
        public void ShutdownModules(ApplicationShutdownContext context)
        {
            var modules = _moduleContainer.Modules.Reverse().ToList();
            foreach (var contributor in _lifecycleContributors)
            {
                foreach (var module in modules)
                    contributor.Shutdown(context, module.Instance);
            }
        }
    }
}
