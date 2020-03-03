using System;
using System.IO;
using System.Threading.Tasks;
using Utf8Json;

namespace Bing.Serialization.Json.Utf8Json
{
    /// <summary>
    /// Utf8Json操作辅助类
    /// </summary>
    public static partial class Utf8JsonHelper
    {
        /// <summary>
        /// 装箱
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="o">对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<Stream> PackAsync<T>(T o, IJsonFormatterResolver resolver = null)
        {
            var ms = new MemoryStream();
            if (o == null)
                return ms;
            await PackAsync(o, ms, resolver);
            return ms;
        }

        /// <summary>
        /// 装箱
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="o">对象</param>
        /// <param name="stream">流</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task PackAsync<T>(T o, Stream stream, IJsonFormatterResolver resolver = null)
        {
            if (o == null || !stream.CanWrite)
                return;
            var bytes = await SerializeToBytesAsync(o, resolver);
            await stream.WriteAsync(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// 拆箱
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="stream">流</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<T> UnpackAsync<T>(Stream stream, IJsonFormatterResolver resolver = null) =>
            stream is null
                ? default
                : await DeserializeFromBytesAsync<T>(stream.CastToBytes(), resolver);

        /// <summary>
        /// 拆箱
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="type">类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<object> UnpackAsync(Stream stream, Type type, IJsonFormatterResolver resolver = null) =>
            stream is null
                ? default
                : await DeserializeFromBytesAsync(stream.CastToBytes(), type, resolver);
    }
}
