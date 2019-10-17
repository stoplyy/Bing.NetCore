using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bing.DependencyInjection.Default
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
            if (IsConventionalRegistrationDisabled(type))
                return;
            var dependencyAttribute = GetDependencyAttributeOrNull(type);
            var lifeTime = GetLifeTimeOrNull(type, dependencyAttribute);
            if (lifeTime == null)
                return;
            var serviceTypes = ExposedServiceExplorer.GetExposedServices(type);
            TriggerServiceExposing(services, type, serviceTypes);
            foreach (var serviceType in serviceTypes)
            {
                var serviceDescriptor = ServiceDescriptor.Describe(serviceType, type, lifeTime.Value);
                if (dependencyAttribute.ReplaceExisting == true)
                    services.Replace(serviceDescriptor);
                else if (dependencyAttribute.TryAdd == true)
                    services.TryAdd(serviceDescriptor);
                else
                    services.Add(serviceDescriptor);
            }
        }

        /// <summary>
        /// 获取依赖注入特性
        /// </summary>
        /// <param name="type">类型</param>
        protected virtual DependencyAttribute GetDependencyAttributeOrNull(Type type) => type.GetCustomAttribute<DependencyAttribute>();

        /// <summary>
        /// 获取生命周期
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="dependencyAttribute">依赖注入特性</param>
        protected virtual ServiceLifetime? GetLifeTimeOrNull(Type type, DependencyAttribute dependencyAttribute) => dependencyAttribute?.Lifetime ?? GetServiceLifetimeFromClassHierarcy(type);

        /// <summary>
        /// 从类层次结构中获取生命周期
        /// </summary>
        /// <param name="type">类型</param>
        protected virtual ServiceLifetime? GetServiceLifetimeFromClassHierarcy(Type type)
        {
            if (typeof(ITransientDependency).GetTypeInfo().IsAssignableFrom(type))
                return ServiceLifetime.Transient;
            if (typeof(ISingletonDependency).GetTypeInfo().IsAssignableFrom(type))
                return ServiceLifetime.Singleton;
            if (typeof(IScopedDependency).GetTypeInfo().IsAssignableFrom(type))
                return ServiceLifetime.Scoped;
            return null;
        }
    }
}
