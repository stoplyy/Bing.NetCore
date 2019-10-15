using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Bing.Collections
{
    /// <summary>
    /// 类型列表
    /// </summary>
    public class TypeList : TypeList<object>, ITypeList { }

    /// <summary>
    /// 类型列表
    /// </summary>
    /// <typeparam name="TBaseType">基类型</typeparam>
    public class TypeList<TBaseType> : ITypeList<TBaseType>
    {
        /// <summary>
        /// 类型列表
        /// </summary>
        private readonly List<Type> _typeList;

        /// <summary>
        /// 初始化一个<see cref="TypeList{TBaseType}"/>类型的实例
        /// </summary>
        public TypeList() => _typeList = new List<Type>();

        /// <summary>
        /// 总数
        /// </summary>
        public int Count => _typeList.Count;

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// 获取或设置 类型
        /// </summary>
        /// <param name="index">索引</param>
        public Type this[int index]
        {
            get => _typeList[index];
            set
            {
                CheckType(value);
                _typeList[index] = value;
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public void Add<T>() where T : TBaseType => _typeList.Add(typeof(T));

        /// <summary>
        /// 添加。如果列表中还没有类型，则将其添加到列表中
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public void TryAdd<T>() where T : TBaseType
        {
            if (Contains<T>())
                return;
            Add<T>();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item">项</param>
        public void Add(Type item)
        {
            CheckType(item);
            _typeList.Add(item);
        }

        /// <summary>
        /// 插入。从指定索引插入类型
        /// </summary>
        /// <param name="index">索引值</param>
        /// <param name="item">项</param>
        public void Insert(int index, Type item)
        {
            CheckType(item);
            _typeList.Insert(index, item);
        }

        /// <summary>
        /// 获取指定项索引值
        /// </summary>
        /// <param name="item">项</param>
        public int IndexOf(Type item) => _typeList.IndexOf(item);

        /// <summary>
        /// 是否包含指定类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public bool Contains<T>() where T : TBaseType => Contains(typeof(T));

        /// <summary>
        /// 是否包含指定类型
        /// </summary>
        /// <param name="item">项</param>
        public bool Contains(Type item) => _typeList.Contains(item);

        /// <summary>
        /// 移除
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public void Remove<T>() where T : TBaseType => _typeList.Remove(typeof(T));

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="item">项</param>
        public bool Remove(Type item) => _typeList.Remove(item);

        /// <summary>
        /// 移除指定索引的项
        /// </summary>
        /// <param name="index">索引值</param>
        public void RemoveAt(int index) => _typeList.RemoveAt(index);

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear() => _typeList.Clear();

        /// <summary>
        /// 从目标数组的指定索引开始，将整个列表复制到兼容的一维数组
        /// </summary>
        /// <param name="array">目标数组</param>
        /// <param name="arrayIndex">目标数组索引值</param>
        public void CopyTo(Type[] array, int arrayIndex) => _typeList.CopyTo(array, arrayIndex);

        /// <summary>
        /// 返回遍历集合的枚举数
        /// </summary>
        public IEnumerator<Type> GetEnumerator() => _typeList.GetEnumerator();

        /// <summary>
        /// 返回遍历集合的枚举数
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() => _typeList.GetEnumerator();

        /// <summary>
        /// 检查类型
        /// </summary>
        /// <param name="item">类型s</param>
        private static void CheckType(Type item)
        {
            if (!typeof(TBaseType).GetTypeInfo().IsAssignableFrom(item))
                throw new ArgumentException(
                    $"给定类型 ({item.AssemblyQualifiedName}) 应为 {typeof(TBaseType).AssemblyQualifiedName} 的实例",
                    nameof(item));
        }
    }
}
