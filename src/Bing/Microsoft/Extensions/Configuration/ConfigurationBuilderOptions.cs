using System.Reflection;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// 配置构建选项配置
    /// </summary>
    public class ConfigurationBuilderOptions
    {
        /// <summary>
        /// 用户安全程序集
        /// </summary>
        public Assembly UserSecretsAssembly { get; set; }

        /// <summary>
        /// 用户安全标识
        /// </summary>
        public string UserSecretsId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; } = "appsettings";

        /// <summary>
        /// 环境名称。支持：Development(开发环境)、Staging(演示环境)、Production(生产环境)
        /// </summary>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// 基路径。用于读取配置文件的根路径
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// 环境变量前缀
        /// </summary>
        public string EnvironmentVariablesPrefix { get; set; }
    }
}
