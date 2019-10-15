using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.DependencyInjection
{
    /// <summary>
    /// 默认常规注册器
    /// </summary>
    public class DefaultConventionalRegistrar : ConventionalRegistrarBase
    {
        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="type">类型</param>
        public override void AddType(IServiceCollection services, Type type)
        {
            throw new NotImplementedException();
        }

    }
}
