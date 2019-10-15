using System;
using System.Collections.Generic;

namespace Bing.DependencyInjection
{
    /// <summary>
    /// 服务公开操作列表
    /// </summary>
    public class ServiceExposingActionList : List<Action<IOnServiceExposingContext>>
    {
    }
}
