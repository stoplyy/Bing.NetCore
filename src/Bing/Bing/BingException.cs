using System;
using System.Runtime.Serialization;

namespace Bing
{
    /// <summary>
    /// Bing 框架系统异常
    /// </summary>
    public class BingException : Exception
    {
        /// <summary>
        /// 初始化一个<see cref="BingException"/>类型的实例
        /// </summary>
        public BingException() { }

        /// <summary>
        /// 初始化一个<see cref="BingException"/>类型的实例
        /// </summary>
        /// <param name="message">错误消息</param>
        public BingException(string message) : base(message) { }

        /// <summary>
        /// 初始化一个<see cref="BingException"/>类型的实例
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="innerException">内部异常</param>
        public BingException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// 初始化一个<see cref="BingException"/>类型的实例
        /// </summary>
        /// <param name="serializationInfo">序列化信息</param>
        /// <param name="context">流上下文</param>
        public BingException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context) { }
    }
}
