using System;
using Bing.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 服务集合 - 注册操作 扩展
    /// </summary>
    public static class ServiceCollectionRegistrationActionExtensions
    {
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="registrationAction">服务注册上下文操作</param>
        public static void OnRegistered(this IServiceCollection services,
            Action<IOnServiceRegistredContext> registrationAction) =>
            GetOrCreateRegistrationActionList(services).Add(registrationAction);

        /// <summary>
        /// 获取服务注册操作列表
        /// </summary>
        /// <param name="services">服务集合</param>
        public static ServiceRegistrationActionList GetRegistrationActionList(this IServiceCollection services) => GetOrCreateRegistrationActionList(services);

        /// <summary>
        /// 获取或创建服务注册操作列表
        /// </summary>
        /// <param name="services">服务集合</param>
        private static ServiceRegistrationActionList GetOrCreateRegistrationActionList(IServiceCollection services)
        {
            var actionList = services.GetSingletonInstanceOrNull<IObjectAccessor<ServiceRegistrationActionList>>()
                ?.Value;
            if (actionList == null)
            {
                actionList = new ServiceRegistrationActionList();
                services.AddObjectAccessor(actionList);
            }
            return actionList;
        }

        /// <summary>
        /// 服务公开
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="exposeAction">服务公开上下文操作</param>
        public static void OnExposing(this IServiceCollection services, Action<IOnServiceExposingContext> exposeAction) => GetOrCreateExposingActionList(services).Add(exposeAction);

        /// <summary>
        /// 获取服务公开操作列表
        /// </summary>
        /// <param name="services">服务集合</param>
        public static ServiceExposingActionList GetExposingActionList(this IServiceCollection services) => GetOrCreateExposingActionList(services);

        /// <summary>
        /// 获取或创建服务公开操作列表
        /// </summary>
        /// <param name="services">服务集合</param>
        private static ServiceExposingActionList GetOrCreateExposingActionList(this IServiceCollection services)
        {
            var actionList = services.GetSingletonInstanceOrNull<IObjectAccessor<ServiceExposingActionList>>()
                ?.Value;
            if (actionList == null)
            {
                actionList = new ServiceExposingActionList();
                services.AddObjectAccessor(actionList);
            }
            return actionList;
        }
    }
}
