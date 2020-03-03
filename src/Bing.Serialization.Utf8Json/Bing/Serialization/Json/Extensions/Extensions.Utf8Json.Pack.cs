using System;
using System.IO;
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
        /// 装箱
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static Stream Utf8JsonPack<T>(this T obj, IJsonFormatterResolver resolver = null) => Utf8JsonHelper.Pack(obj, resolver);

        /// <summary>
        /// 装箱
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="stream">流</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static void Utf8JsonPackTo<T>(this T obj, Stream stream, IJsonFormatterResolver resolver = null) => Utf8JsonHelper.Pack(obj, stream, resolver);

        /// <summary>
        /// 装箱
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="obj">对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static void Utf8JsonPackBy(this Stream stream, object obj, IJsonFormatterResolver resolver = null) => Utf8JsonHelper.Pack(obj, stream, resolver);

        /// <summary>
        /// 装箱
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<Stream> Utf8JsonPackAsync<T>(this T obj, IJsonFormatterResolver resolver = null) => await Utf8JsonHelper.PackAsync(obj, resolver);

        /// <summary>
        /// 装箱
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="stream">流</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task Utf8JsonPackToAsync<T>(this T obj, Stream stream, IJsonFormatterResolver resolver = null) => await Utf8JsonHelper.PackAsync(obj, stream, resolver);

        /// <summary>
        /// 装箱
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="obj">对象</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task Utf8JsonPackByAsync(this Stream stream, object obj, IJsonFormatterResolver resolver = null) => await Utf8JsonHelper.PackAsync(obj, stream, resolver);

        /// <summary>
        /// 拆箱
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="stream">流</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static T Utf8JsonUnpack<T>(this Stream stream, IJsonFormatterResolver resolver = null) => Utf8JsonHelper.Unpack<T>(stream, resolver);

        /// <summary>
        /// 拆箱
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="type">类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static object Utf8JsonUnpack(this Stream stream, Type type, IJsonFormatterResolver resolver = null) => Utf8JsonHelper.Unpack(stream, type, resolver);

        /// <summary>
        /// 拆箱
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="stream">流</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<T> Utf8JsonUnpackAsync<T>(this Stream stream, IJsonFormatterResolver resolver = null) => await Utf8JsonHelper.UnpackAsync<T>(stream, resolver);

        /// <summary>
        /// 拆箱
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="type">类型</param>
        /// <param name="resolver">Json格式化解析器</param>
        public static async Task<object> Utf8JsonUnpackAsync(this Stream stream, Type type, IJsonFormatterResolver resolver = null) => await Utf8JsonHelper.UnpackAsync(stream, type, resolver);
    }
}
