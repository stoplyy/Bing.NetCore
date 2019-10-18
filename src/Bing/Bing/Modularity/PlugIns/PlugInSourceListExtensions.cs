using System;
using System.IO;

namespace Bing.Modularity.PlugIns
{
    /// <summary>
    /// 插件源列表(<see cref="PlugInSourceList"/>) 扩展
    /// </summary>
    public static class PlugInSourceListExtensions
    {
        /// <summary>
        /// 添加文件夹
        /// </summary>
        /// <param name="list">插件源列表</param>
        /// <param name="folder">文件夹</param>
        /// <param name="searchOption">查询选项配置</param>
        public static void AddFolder(this PlugInSourceList list, string folder,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            list.Add(new FolderPlugInSource(folder, searchOption));
        }

        /// <summary>
        /// 添加模块类型列表
        /// </summary>
        /// <param name="list">插件源列表</param>
        /// <param name="moduleTypes">模块类型列表</param>
        public static void AddTypes(this PlugInSourceList list, params Type[] moduleTypes)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            list.Add(new TypePlugInSource(moduleTypes));
        }

        /// <summary>
        /// 添加文件列表
        /// </summary>
        /// <param name="list">插件源列表</param>
        /// <param name="filePaths">文件路径列表</param>
        public static void AddFiles(this PlugInSourceList list, params string[] filePaths)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            list.Add(new FilePlugInSource(filePaths));
        }
    }
}
