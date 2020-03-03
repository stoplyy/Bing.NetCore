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
        /// 转换为Utf8Json字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static string ToUtf8Json<T>(this T obj, IJsonFormatterResolver resolver = null) => Utf8JsonHelper.Serialize(obj, resolver);

        /// <summary>
        /// 转换为Utf8Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static string ToUtf8Json(this object obj, IJsonFormatterResolver resolver = null) => Utf8JsonHelper.Serialize(obj, resolver);

        /// <summary>
        /// 转换为Utf8Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">对象类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static string ToUtf8Json(this object obj, Type type, IJsonFormatterResolver resolver = null) => Utf8JsonHelper.Serialize(obj, type, resolver);

        /// <summary>
        /// 转换为Utf8Json字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<string> ToUtf8JsonAsync<T>(this T obj, IJsonFormatterResolver resolver = null) => await Utf8JsonHelper.SerializeAsync(obj, resolver);

        /// <summary>
        /// 转换为Utf8Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<string> ToUtf8JsonAsync(this object obj, IJsonFormatterResolver resolver = null) => await Utf8JsonHelper.SerializeAsync(obj, resolver);

        /// <summary>
        /// 转换为Utf8Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">对象类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<string> ToUtf8JsonAsync(this object obj, Type type, IJsonFormatterResolver resolver = null) => await Utf8JsonHelper.SerializeAsync(obj, type, resolver);
    }
}
