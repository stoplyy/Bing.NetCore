namespace Bing.Modularity
{
    /// <summary>
    /// 预配置服务集合
    /// </summary>
    public interface IPreConfigureServices
    {
        /// <summary>
        /// 预配置服务集合
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        void PreConfigureServices(ServiceConfigurationContext context);
    }
}
