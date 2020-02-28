using System;
using System.Threading.Tasks;
using Bing.Applications;
using Bing.Logs.Aspects;
using Bing.Samples.Service.Requests;
using Bing.Validations.Aspects;

namespace Bing.Samples.Service.Abstractions
{
    /// <summary>
    /// 测试服务
    /// </summary>
    public interface ITestService : IService
    {
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="id">标识</param>
        [DebugLog]
        Task<string> GetAsync(Guid id);

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="content">内容</param>
        Task WriteLogAsync(string content);

        /// <summary>
        /// 测试AOP验证
        /// </summary>
        /// <param name="request">请求</param>
        Task TestAopValidateAsync([Valid] ValidSampleRequest request);
    }
}
