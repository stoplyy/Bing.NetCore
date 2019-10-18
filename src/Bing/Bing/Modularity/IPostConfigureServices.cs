namespace Bing.Modularity
{
    /// <summary>
    /// 后配置服务集合
    /// </summary>
    public interface IPostConfigureServices
    {
        /// <summary>
        /// 后配置服务集合
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        void PostConfigureServices(ServiceConfigurationContext context);
    }
}
