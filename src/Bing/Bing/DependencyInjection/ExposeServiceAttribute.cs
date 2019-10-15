using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bing.Utils.Extensions;

namespace Bing.DependencyInjection
{
    /// <summary>
    /// 公开服务
    /// </summary>
    public class ExposeServiceAttribute : Attribute, IExposedServiceTypesProvider
    {
        /// <summary>
        /// 初始化一个<see cref="ExposeServiceAttribute"/>类型的实例
        /// </summary>
        /// <param name="serviceTypes">服务类型集合</param>
        public ExposeServiceAttribute(params Type[] serviceTypes) => ServiceTypes = serviceTypes ?? new Type[0];

        /// <summary>
        /// 服务类型集合
        /// </summary>
        public Type[] ServiceTypes { get; }

        /// <summary>
        /// 是否包含默认服务
        /// </summary>
        public bool? IncludeDefaults { get; set; }

        /// <summary>
        /// 是否包含自身服务
        /// </summary>
        public bool? IncludeSelf { get; set; }

        /// <summary>
        /// 获取公开服务类型列表
        /// </summary>
        /// <param name="targetType">目标类型</param>
        public Type[] GetExposedServiceTypes(Type targetType)
        {
            var serviceList = ServiceTypes.ToList();
            if (IncludeDefaults == true)
            {
                foreach (var type in GetDefaultServices(targetType))
                    serviceList.AddIfNotContains(type);
                if (IncludeSelf != false)
                    serviceList.AddIfNotContains(targetType);
            }
            else if (IncludeSelf == true)
            {
                serviceList.AddIfNotContains(targetType);
            }
            return serviceList.ToArray();
        }

        /// <summary>
        /// 获取默认服务列表
        /// </summary>
        /// <param name="type">类型</param>
        private static List<Type> GetDefaultServices(Type type)
        {
            var serviceTypes = new List<Type>();
            foreach (var interfaceType in type.GetTypeInfo().GetInterfaces())
            {
                var interfaceName = interfaceType.Name;
                if (interfaceName.StartsWith("I"))
                    interfaceName = interfaceName.Right(interfaceName.Length - 1);
                if (type.Name.EndsWith(interfaceName))
                    serviceTypes.Add(interfaceType);
            }
            return serviceTypes;
        }
    }
}
