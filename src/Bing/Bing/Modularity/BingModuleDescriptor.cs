using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;

namespace Bing.Modularity
{
    /// <summary>
    /// Bing 模块描述
    /// </summary>
    public class BingModuleDescriptor : IBingModuleDescriptor
    {
        /// <summary>
        /// 初始化一个<see cref="BingModuleDescriptor"/>类型的实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="instance">模块实例</param>
        /// <param name="isLoadedAsPlugIn">是否作为插件加载</param>
        public BingModuleDescriptor(Type type, IBingModule instance, bool isLoadedAsPlugIn)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (instance == null)
                throw new ArgumentException(nameof(instance));
            if (!type.GetTypeInfo().IsInstanceOfType(instance))
                throw new ArgumentException($"给定的模块实例 ({instance.GetType().AssemblyQualifiedName}) 不是给定模块 {type.AssemblyQualifiedName} 类型的实例");

            Type = type;
            Assembly = type.Assembly;
            Instance = instance;
            IsLoadedAsPlugIn = isLoadedAsPlugIn;
            _dependencies = new List<IBingModuleDescriptor>();
        }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 程序集
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// Bing 模块实例
        /// </summary>
        public IBingModule Instance { get; }

        /// <summary>
        /// 是否作为插件加载
        /// </summary>
        public bool IsLoadedAsPlugIn { get; }

        /// <summary>
        /// 依赖关系列表
        /// </summary>
        private readonly List<IBingModuleDescriptor> _dependencies;

        /// <summary>
        /// 依赖关系列表
        /// </summary>
        public IReadOnlyList<IBingModuleDescriptor> Dependencies => _dependencies.ToImmutableList();

        /// <summary>
        /// 添加依赖
        /// </summary>
        /// <param name="descriptor">模块描述</param>
        public void AddDependency(IBingModuleDescriptor descriptor) => _dependencies.Add(descriptor);

        /// <summary>
        /// 输出字符串
        /// </summary>
        public override string ToString() => $"[BingModuleDescriptor {Type.FullName}]";
    }
}
