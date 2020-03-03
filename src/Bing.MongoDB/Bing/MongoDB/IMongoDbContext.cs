using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Bing.MongoDB
{
    /// <summary>
    /// MongoDB 上下文
    /// </summary>
    public interface IMongoDbContext : IDisposable
    {
        /// <summary>
        /// MongoDB 数据库
        /// </summary>
        IMongoDatabase Database { get; }

        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="func">函数</param>
        void AddCommand(Func<Task> func);

        /// <summary>
        /// 保存变更
        /// </summary>
        Task<int> SaveChanges();

        /// <summary>
        /// 集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        IMongoCollection<TEntity> Collection<TEntity>();

        /// <summary>
        /// 集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="name">集合名</param>
        IMongoCollection<TEntity> Collection<TEntity>(string name);
    }
}
