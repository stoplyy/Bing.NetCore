using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Bing.Reflections;
using Bing.Reflections.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.DependencyInjection
{
    /// <summary>
    /// 常规注册器抽象基类
    /// </summary>
    public abstract class ConventionalRegistrarBase : IConventionalRegistrar
    {
        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="assembly">程序集</param>
        public virtual void AddAssembly(IServiceCollection services, Assembly assembly)
        {
            var types = AssemblyHelper.GetAllTypes(assembly)
                .Where(
                    type => type != null &&
                            type.IsClass &&
                            !type.IsAbstract &&
                            !type.IsGenericType)
                .ToArray();
            AddTypes(services,types);
        }

        /// <summary>
        /// 注册批量类型
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="types">类型集合</param>
        public virtual void AddTypes(IServiceCollection services, params Type[] types)
        {
            foreach (var type in types)
                AddType(services, type);
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="type">类型</param>
        public abstract void AddType(IServiceCollection services, Type type);

        /// <summary>
        /// 常规注册是否已禁用
        /// </summary>
        /// <param name="type">类型</param>
        protected virtual bool IsConventionalRegistrationDisabled(Type type) => type.IsDefined(typeof(IgnoreDependencyAttribute), true);

        /// <summary>
        /// 触发服务公开
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="implementationType">实现类型</param>
        /// <param name="serviceTypes">服务类型列表</param>
        protected virtual bool TriggerServiceExposing(IServiceCollection services, Type implementationType,
            List<Type> serviceTypes)
        {
            return false;
        }
    }
}
