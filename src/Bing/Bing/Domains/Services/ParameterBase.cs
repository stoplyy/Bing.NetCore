using System.Linq;
using Bing.Exceptions;
using Bing.Validations;
using Bing.Validations.Abstractions;

namespace Bing.Domains.Services
{
    /// <summary>
    /// 参数基类
    /// </summary>
    public abstract class ParameterBase : IValidatable
    {
        /// <summary>
        /// 验证
        /// </summary>
        public virtual ValidationResultCollection Validate()
        {
            var result = DataAnnotationValidation.Validate(this);
            if (result.IsValid)
                return ValidationResultCollection.Success;
            throw new Warning(result.First().ErrorMessage);
        }
    }
}
