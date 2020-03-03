using System;
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
        public static string Serialize<T>(T o, IJsonFormatterResolver resolver = null) =>
            o is null
                ? string.Empty
                : JsonSerializer.ToJsonString(o, resolver);

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="o">序列化对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static string Serialize(object o, IJsonFormatterResolver resolver = null) =>
            o is null
                ? string.Empty
                : JsonSerializer.ToJsonString(o, resolver);

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="o">序列化对象</param>
        /// <param name="type">序列化对象类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static string Serialize(object o, Type type, IJsonFormatterResolver resolver = null) =>
            o is null
                ? string.Empty
                : JsonSerializer.NonGeneric.ToJsonString(type, o, resolver);

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="o">序列化对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static byte[] SerializeToBytes<T>(T o, IJsonFormatterResolver resolver = null) =>
            o is null
                ? new byte[0]
                : JsonSerializer.Serialize(o, resolver);

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="o">序列化对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static byte[] SerializeToBytes(object o, IJsonFormatterResolver resolver = null) =>
            o is null
                ? new byte[0]
                : JsonSerializer.Serialize(o, resolver);

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="o">序列化对象</param>
        /// <param name="type">序列化对象类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static byte[] SerializeToBytes(object o, Type type, IJsonFormatterResolver resolver = null) =>
            o is null
                ? new byte[0]
                : JsonSerializer.NonGeneric.Serialize(type, o, resolver);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static T Deserialize<T>(string json, IJsonFormatterResolver resolver = null) =>
            string.IsNullOrWhiteSpace(json)
                ? default
                : JsonSerializer.Deserialize<T>(json, resolver);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <param name="type">反序列化对象类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static object Deserialize(string json, Type type, IJsonFormatterResolver resolver = null) =>
            string.IsNullOrWhiteSpace(json)
                ? default
                : JsonSerializer.NonGeneric.Deserialize(type, json, resolver);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static T DeserializeFromBytes<T>(byte[] data, IJsonFormatterResolver resolver = null) =>
            data is null || data.Length is 0
                ? default
                : JsonSerializer.Deserialize<T>(data, resolver);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="type">反序列化对象类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static object DeserializeFromBytes(byte[] data, Type type, IJsonFormatterResolver resolver = null)
        {
            if (data is null || data.Length is 0)
                return null;
            var reader = new JsonReader(data, 0);
            return JsonSerializer.NonGeneric.Deserialize(type, ref reader, resolver);
        }
    }
}
