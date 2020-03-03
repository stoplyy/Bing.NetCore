using System;
using System.Threading.Tasks;
using Bing.Serialization.Json.Utf8Json;
using Utf8Json;

// ReSharper disable once CheckNamespace
namespace Bing.Serialization.Json
{
    /// <summary>
    /// Utf8Json 扩展
    /// </summary>
    public static partial class Utf8JsonExtensions
    {
        /// <summary>
        /// 转换为Utf8Json字节数组
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">序列化对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static byte[] ToUtf8JsonBytes<T>(this T obj, IJsonFormatterResolver resolver = null) => Utf8JsonHelper.SerializeToBytes(obj, resolver);

        /// <summary>
        /// 转换为Utf8Json字节数组
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">序列化对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<byte[]> ToUtf8JsonBytesAsync<T>(this T obj, IJsonFormatterResolver resolver = null) => await Utf8JsonHelper.SerializeToBytesAsync(obj, resolver);

        /// <summary>
        /// 从Utf8Json字节数组反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">json字节数组</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static T FromUtf8JsonBytes<T>(this byte[] data, IJsonFormatterResolver resolver = null) => Utf8JsonHelper.DeserializeFromBytes<T>(data, resolver);

        /// <summary>
        /// 从Utf8Json字节数组反序列化为对象
        /// </summary>
        /// <param name="data">json字节数组</param>
        /// <param name="type">对象类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static object FromUtf8JsonBytes(this byte[] data, Type type, IJsonFormatterResolver resolver = null) => Utf8JsonHelper.DeserializeFromBytes(data, type, resolver);

        /// <summary>
        /// 从Utf8Json字节数组反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">json字节数组</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<T> FromUtf8JsonBytesAsync<T>(this byte[] data, IJsonFormatterResolver resolver = null) => await Utf8JsonHelper.DeserializeFromBytesAsync<T>(data, resolver);

        /// <summary>
        /// 从Utf8Json字节数组反序列化为对象
        /// </summary>
        /// <param name="data">json字节数组</param>
        /// <param name="type">对象类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<object> FromUtf8JsonBytesAsync(this byte[] data, Type type, IJsonFormatterResolver resolver = null) => await Utf8JsonHelper.DeserializeFromBytesAsync(data, type, resolver);
    }
}
