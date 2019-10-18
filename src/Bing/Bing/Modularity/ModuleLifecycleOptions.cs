using Bing.Collections;

namespace Bing.Modularity
{
    /// <summary>
    /// 模块生命周期选项配置
    /// </summary>
    public class ModuleLifecycleOptions
    {
        /// <summary>
        /// 初始化一个<see cref="ModuleLifecycleOptions"/>类型的实例
        /// </summary>
        public ModuleLifecycleOptions() => Contributors = new TypeList<IModuleLifecycleContributor>();

        /// <summary>
        /// 构造器列表
        /// </summary>
        public ITypeList<IModuleLifecycleContributor> Contributors { get; }
    }
}
