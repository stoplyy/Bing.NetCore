namespace Bing.Modularity
{
    /// <summary>
    /// Bing 模块
    /// </summary>
    public interface IBingModule
    {
        /// <summary>
        /// 配置服务集合
        /// </summary>
        /// <param name="context">配置服务上下文</param>
        void ConfigureServices(ServiceConfigurationContext context);
    }
}
