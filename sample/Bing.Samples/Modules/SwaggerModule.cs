using System.ComponentModel;
using Bing.AspNetCore;
using Bing.Core.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Bing.Samples.Modules
{
    /// <summary>
    /// Swagger 模块
    /// </summary>
    [Description("SwaggerApi模块")]
    [DependsOnModule(typeof(AspNetCoreModule))]
    public class SwaggerModule:AspNetCoreBingModule
    {
        /// <summary>
        /// 模块级别。级别越小越先启动
        /// </summary>
        public override ModuleLevel Level => ModuleLevel.Application;

        /// <summary>
        /// 模块启动顺序。模块启动的顺序先按级别启动，同一级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 2;

        /// <summary>
        /// 添加服务。将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">服务集合</param>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc($"v1", new OpenApiInfo() {Title = "Bing.Samples", Version = "1"});
            });
            //services.AddSwaggerCustom(SwaggerOptions);
            return services;
        }

        /// <summary>
        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        public override void UseModule(IApplicationBuilder app)
        {
            app.UseSwagger(o =>
            {
            });
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json","Bing.Samples V1");
            });
            //app.UseSwaggerCustom(SwaggerOptions);
            Enabled = true;
        }

        ///// <summary>
        ///// Swagger选项配置
        ///// </summary>
        //private CustomSwaggerOptions SwaggerOptions = new CustomSwaggerOptions()
        //{
        //    ProjectName = "Bing.Samples 在线文档调试",
        //    UseCustomIndex = true,
        //    RoutePrefix = "swagger",
        //    ApiVersions = new List<ApiVersion>() { new ApiVersion() { Description = "", Version = "v1" } },
        //    SwaggerAuthorizations = new List<CustomSwaggerAuthorization>() { },
        //    AddSwaggerGenAction = config =>
        //    {
        //        //config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Bing.Samples.xml"), true);

        //        config.OperationFilter<RequestHeaderOperationFilter>();
        //        config.OperationFilter<ResponseHeadersOperationFilter>();
        //        config.OperationFilter<FileParameterOperationFilter>();

        //        // 授权组合
        //        config.OperationFilter<SecurityRequirementsOperationFilter>();
        //        config.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
        //        // 启用Swagger验证功能，与AddSecurityDefinition方法指定的方案名称一致
        //        config.AddSecurityRequirement(new OpenApiSecurityRequirement()
        //        {
        //            {
        //                new OpenApiSecurityScheme
        //                {
        //                    Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "oauth2"}
        //                },
        //                new[] {"readAccess", "writeAccess"}
        //            }
        //        });

        //        config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
        //        {
        //            Type = SecuritySchemeType.ApiKey,
        //            Description = "Token令牌",
        //            Name = "Authorization",
        //            In = ParameterLocation.Header,
        //        });
        //        // 设置所有参数为驼峰式命名
        //        config.DescribeAllParametersInCamelCase();
        //    },
        //    UseSwaggerAction = config =>
        //    {
        //    },
        //    UseSwaggerUIAction = config =>
        //    {
        //        config.InjectJavascript("/swagger/resources/jquery");
        //        config.InjectStylesheet("/swagger/resources/swagger-common");
        //        config.UseDefaultSwaggerUI();
        //    }
        //};
    }
}
