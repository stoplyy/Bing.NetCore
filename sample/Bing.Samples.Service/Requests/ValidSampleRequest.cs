using System.ComponentModel.DataAnnotations;
using Bing.Applications.Dtos;
using Bing.Exceptions;
using Bing.Validations;

namespace Bing.Samples.Service.Requests
{
    /// <summary>
    /// 验证简单请求
    /// </summary>
    public class ValidSampleRequest : RequestBase
    {
        /// <summary>
        /// 系统标识
        /// </summary>
        [Required(ErrorMessage = "系统标识不能为空")]
        public string Id { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        public override ValidationResultCollection Validate()
        {
            if (Id == "1")
                throw new Warning("系统标识不能为 1。");
            return base.Validate();
        }
    }
}
