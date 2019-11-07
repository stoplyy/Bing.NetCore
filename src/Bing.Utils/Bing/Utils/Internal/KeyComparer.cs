using System;
using System.Collections.Generic;

namespace Bing.Utils.Internal
{
    /// <summary>
    /// 键比较器
    /// </summary>
    internal class KeyComparer<TSource, TKey> : EqualityComparer<TSource>
    {
        /// <summary>
        /// 键选择器
        /// </summary>
        private readonly Func<TSource, TKey> _keySelector;

        /// <summary>
        /// 初始化一个<see cref="KeyComparer{TSource,TKey}"/>类型的实例
        /// </summary>
        /// <param name="keySelector">键选择器</param>
        public KeyComparer(Func<TSource, TKey> keySelector) => _keySelector = keySelector;

        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="x">数据源X</param>
        /// <param name="y">数据源B</param>
        public override bool Equals(TSource x, TSource y) => x == null || y == null
            ? x == null && y == null
            : _keySelector(x).Equals(_keySelector(y));

        /// <summary>
        /// 获取哈希编码
        /// </summary>
        /// <param name="obj">数据源</param>
        public override int GetHashCode(TSource obj) =>
            obj == null ? base.GetHashCode() : _keySelector(obj).GetHashCode();
    }
}
