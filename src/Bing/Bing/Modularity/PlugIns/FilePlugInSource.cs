using System;
using System.Collections.Generic;
using System.Runtime.Loader;
using Bing.Utils.Extensions;

namespace Bing.Modularity.PlugIns
{
    /// <summary>
    /// 文件插件源
    /// </summary>
    public class FilePlugInSource:IPlugInSource
    {
        /// <summary>
        /// 初始化一个<see cref="FilePlugInSource"/>类型的实例
        /// </summary>
        /// <param name="filePaths">文件路径集合</param>
        public FilePlugInSource(params string[] filePaths) => FilePaths = filePaths ?? new string[0];

        /// <summary>
        /// 文件路径集合
        /// </summary>
        public string[] FilePaths { get; }

        /// <summary>
        /// 获取模块列表
        /// </summary>
        public Type[] GetModules()
        {
            var modules=new List<Type>();
            foreach (var filePath in FilePaths)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);
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
    }
}
