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
        /// 将Utf8Json字符串转换为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static T FromUtf8Json<T>(this string json, IJsonFormatterResolver resolver = null) => Utf8JsonHelper.Deserialize<T>(json, resolver);

        /// <summary>
        /// 将Utf8Json字符串转换为对象
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <param name="type">对象类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static object FromUtf8Json(this string json, Type type, IJsonFormatterResolver resolver = null) => Utf8JsonHelper.Deserialize(json, type, resolver);

        /// <summary>
        /// 将Utf8Json字符串转换为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<T> FromUtf8JsonAsync<T>(this string json, IJsonFormatterResolver resolver = null) => await Utf8JsonHelper.DeserializeAsync<T>(json, resolver);

        /// <summary>
        /// 将Utf8Json字符串转换为对象
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <param name="type">对象类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<object> FromUtf8JsonAsync(this string json, Type type, IJsonFormatterResolver resolver = null) => await Utf8JsonHelper.DeserializeAsync(json, type, resolver);
    }
}
