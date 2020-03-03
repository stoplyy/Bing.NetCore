using System;
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
        /// 序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="o">序列化对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<string> SerializeAsync<T>(T o, IJsonFormatterResolver resolver = null) =>
            o is null
                ? string.Empty
                : await Task.Run(() => JsonSerializer.ToJsonString(o, resolver));

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="o">序列化对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<string> SerializeAsync(object o, IJsonFormatterResolver resolver = null) =>
            o is null
                ? string.Empty
                : await Task.Run(() => JsonSerializer.ToJsonString(o, resolver));

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="o">序列化对象</param>
        /// <param name="type">序列化对象类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<string> SerializeAsync(object o, Type type, IJsonFormatterResolver resolver = null) =>
            o is null
                ? string.Empty
                : await Task.Run(() => JsonSerializer.NonGeneric.ToJsonString(type, o, resolver));

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="o">序列化对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<byte[]> SerializeToBytesAsync<T>(T o, IJsonFormatterResolver resolver = null) =>
            o is null
                ? new byte[0]
                : await Task.Run(() => JsonSerializer.Serialize(o, resolver));

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="o">序列化对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<byte[]> SerializeToBytesAsync(object o, IJsonFormatterResolver resolver = null) =>
            o is null
                ? new byte[0]
                : await Task.Run(() => JsonSerializer.Serialize(o, resolver));

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="o">序列化对象</param>
        /// <param name="type">序列化对象类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<byte[]> SerializeToBytesAsync(object o, Type type, IJsonFormatterResolver resolver = null) =>
            o is null
                ? new byte[0]
                : await Task.Run(() => JsonSerializer.NonGeneric.Serialize(type, o, resolver));

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<T> DeserializeAsync<T>(string json, IJsonFormatterResolver resolver = null) =>
            string.IsNullOrWhiteSpace(json)
                ? default
                : await Task.Run(() => JsonSerializer.Deserialize<T>(json, resolver));

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <param name="type">反序列化对象类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<object> DeserializeAsync(string json, Type type, IJsonFormatterResolver resolver = null) =>
            string.IsNullOrWhiteSpace(json)
                ? default
                : await Task.Run(() => JsonSerializer.NonGeneric.Deserialize(type, json, resolver));

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<T> DeserializeFromBytesAsync<T>(byte[] data, IJsonFormatterResolver resolver = null) =>
            data is null || data.Length is 0
                ? default
                : await Task.Run(() => JsonSerializer.Deserialize<T>(data, resolver));

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="type">反序列化对象类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<object> DeserializeFromBytesAsync(byte[] data, Type type, IJsonFormatterResolver resolver = null)
        {
            if (data is null || data.Length is 0)
                return null;
            var reader = new JsonReader(data, 0);
            return await Task.Run(() => JsonSerializer.NonGeneric.Deserialize(type, ref reader, resolver));
        }
    }
}
