using Bing.Events.Cap;
using Bing.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Samples.Modules
{
    /// <summary>
    /// Cap 模块
    /// </summary>
    public class CapModule:BingModule
    {
        /// <summary>
        /// 配置服务集合
        /// </summary>
        /// <param name="context">配置服务上下文</param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // 注册CAP事件总线服务
            context.Services.AddCapEventBus(o =>
            {
                o.UseEntityFramework<Bing.Samples.Data.UnitOfWorks.MySql.SampleUnitOfWork>();
                o.UseDashboard();
                // 设置处理成功的数据在数据库中保存的时间（秒），为保证系统性能，数据会定期清理
                o.SucceedMessageExpiredAfter = 24 * 3600;
                // 设置失败重试次数
                o.FailedRetryCount = 5;
                o.Version = "bing_test";
                // 启用内存队列
                //o.UseInMemoryMessageQueue();
                // 启用RabbitMQ
                o.UseRabbitMQ(x =>
                {
                    x.HostName = "47.106.107.73";
                    x.UserName = "admin";
                    x.Password = "bing2019.00";
                });
            });
        }
    }
}
