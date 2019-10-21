using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Bing.Reflections.Internal;
using Bing.Utils.Extensions;

namespace Bing.Modularity.PlugIns
{
    /// <summary>
    /// 文件夹插件源
    /// </summary>
    public class FolderPlugInSource : IPlugInSource
    {
        /// <summary>
        /// 初始一个<see cref="FolderPlugInSource"/>类型的实例
        /// </summary>
        /// <param name="folder">文件夹</param>
        /// <param name="searchOption">查询选项配置</param>
        public FolderPlugInSource(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            Folder = folder ?? throw new ArgumentNullException(nameof(folder));
            SearchOption = searchOption;
        }

        /// <summary>
        /// 文件夹
        /// </summary>
        public string Folder { get; }

        /// <summary>
        /// 查询选项配置
        /// </summary>
        public SearchOption SearchOption { get; set; }

        /// <summary>
        /// 过滤器
        /// </summary>
        public Func<string, bool> Filter { get; set; }

        /// <summary>
        /// 获取模块列表
        /// </summary>
        public Type[] GetModules()
        {
            var modules = new List<Type>();
            foreach (var assembly in GetAssemblies())
            {
                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (BingModule.IsBingModule(type))
                            modules.AddIfNotContains(type);
                    }
                }
                catch (Exception ex)
                {
                    throw new BingException($"无法从程序集中获取模块类型: {assembly.FullName}", ex);
                }
            }
            return modules.ToArray();
        }

        /// <summary>
        /// 获取程序集列表
        /// </summary>
        private IEnumerable<Assembly> GetAssemblies()
        {
            var assemblyFiles = AssemblyHelper.GetAssemblyFiles(Folder, SearchOption);
            if (Filter != null)
                assemblyFiles = assemblyFiles.Where(Filter);
            return assemblyFiles.Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);
        }
    }
}
