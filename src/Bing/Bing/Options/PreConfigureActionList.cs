using System;
using System.Collections.Generic;

namespace Bing.Options
{
    /// <summary>
    /// 预配置操作列表
    /// </summary>
    /// <typeparam name="TOptions">选项配置类型</typeparam>
    public class PreConfigureActionList<TOptions>:List<Action<TOptions>>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="options">选项配置</param>
        public void Configure(TOptions options)
        {
            foreach (var action in this)
                action(options);
        }
    }
}
