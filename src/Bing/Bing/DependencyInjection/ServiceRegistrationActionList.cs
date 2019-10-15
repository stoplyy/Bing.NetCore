using System;
using System.Collections.Generic;

namespace Bing.DependencyInjection
{
    /// <summary>
    /// 服务注册操作列表
    /// </summary>
    public class ServiceRegistrationActionList : List<Action<IOnServiceRegistredContext>>
    {
    }
}
