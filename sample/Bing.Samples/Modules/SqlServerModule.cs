using Bing.AspNetCore;
using Bing.Core;
using Bing.Core.Modularity;
using Bing.Datas.Dapper;
using Bing.Datas.EntityFramework.SqlServer;
using Bing.Datas.Enums;
using Bing.Samples.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Samples.Modules
{
    /// <summary>
    /// SqlServer 模块
    /// </summary>
    [DependsOnModule(typeof(AspNetCoreModule))]
    public class SqlServerModule : AspNetCoreBingModule
    {
        /// <summary>
        /// 模块级别。级别越小越先启动
        /// </summary>
        public override ModuleLevel Level => ModuleLevel.Framework;

        /// <summary>
        /// 添加服务。将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">服务集合</param>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            // 注册工作单元
            services.AddSqlServerUnitOfWork<ISampleUnitOfWork, Bing.Samples.Data.UnitOfWorks.SqlServer.SampleUnitOfWork>(
                services.GetConfiguration().GetConnectionString("DefaultConnection"));

            // 注册SqlQuery
            services.AddSqlQuery<Bing.Samples.Data.UnitOfWorks.SqlServer.SampleUnitOfWork, Bing.Samples.Data.UnitOfWorks.SqlServer.SampleUnitOfWork>(options =>
            {
                options.DatabaseType = DatabaseType.SqlServer;
                options.IsClearAfterExecution = true;
            });
            // 注册SqlExecutor
            services.AddSqlExecutor();
            return services;
        }

        /// <summary>
        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        public override void UseModule(IApplicationBuilder app) => Enabled = true;
    }
}
