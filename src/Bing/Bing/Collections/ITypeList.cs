using System;
using System.Collections.Generic;

namespace Bing.Collections
{
    /// <summary>
    /// 类型列表
    /// </summary>
    public interface ITypeList : ITypeList<object>
    {
    }

    /// <summary>
    /// 类型列表
    /// </summary>
    /// <typeparam name="TBaseType">基类型</typeparam>
    public interface ITypeList<in TBaseType> : IList<Type>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        void Add<T>() where T : TBaseType;

        /// <summary>
        /// 添加。如果列表中还没有类型，则将其添加到列表中
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        void TryAdd<T>() where T : TBaseType;

        /// <summary>
        /// 是否包含指定类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        bool Contains<T>() where T : TBaseType;

        /// <summary>
        /// 移除
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        void Remove<T>() where T : TBaseType;
    }
}
