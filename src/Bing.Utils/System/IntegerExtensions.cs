namespace System
{
    /// <summary>
    /// 转换数组选项
    /// </summary>
    public enum ToArrayOptions
    {
        /// <summary>
        /// 使用默认值
        /// </summary>
        UseDefaultValue,
        /// <summary>
        /// 使用索引
        /// </summary>
        UseIndex,
    }

    /// <summary>
    /// 整数(<see cref="Int32"/>) 扩展
    /// </summary>
    public static class IntegerExtensions
    {
        #region ToArray(转换为数组)

        /// <summary>
        /// 转换为数组
        /// </summary>
        /// <param name="count">计数</param>
        /// <param name="toArrayOptions">转换数组选项</param>
        public static int[] ToArray(this int? count, ToArrayOptions toArrayOptions = ToArrayOptions.UseDefaultValue) =>
            count?.ToArray(toArrayOptions);

        /// <summary>
        /// 转换为数组
        /// </summary>
        /// <param name="count">计数</param>
        /// <param name="toArrayOptions">转换数组选项</param>
        public static int[] ToArray(this int count, ToArrayOptions toArrayOptions = ToArrayOptions.UseDefaultValue) =>
            count.ToArray(index => toArrayOptions == ToArrayOptions.UseIndex ? index : 0);

        /// <summary>
        /// 转换为数组
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="count">计数</param>
        /// <param name="indexedValueSelector">索引值选择器</param>
        public static T[] ToArray<T>(this int? count, Func<int, T> indexedValueSelector = null) => count?.ToArray(indexedValueSelector);

        /// <summary>
        /// 转换为数组
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="count">计数</param>
        /// <param name="indexedValueSelector">索引值选择器</param>
        public static T[] ToArray<T>(this int count, Func<int, T> indexedValueSelector = null)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), count, $"{nameof(count)} 必须大于等于 -1.");
            var valArray = new T[count];
            for (var i = 0; i < count; i++)
                valArray[i] = indexedValueSelector == null ? default : indexedValueSelector(i);
            return valArray;
        }

        #endregion

    }
}
