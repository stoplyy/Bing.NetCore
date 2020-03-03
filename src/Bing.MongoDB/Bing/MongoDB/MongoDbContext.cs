using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Bing.MongoDB
{
    /// <summary>
    /// MongoDB 上下文
    /// </summary>
    public abstract class MongoDbContext : IMongoDbContext
    {
        /// <summary>
        /// 命令列表
        /// </summary>
        private readonly List<Func<Task>> _commands;

        /// <summary>
        /// 配置
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// MongoDB 数据库
        /// </summary>
        public IMongoDatabase Database { get; set; }

        /// <summary>
        /// MongoDB 会话
        /// </summary>
        public IClientSessionHandle Session { get; set; }

        /// <summary>
        /// MongoDB 客户端
        /// </summary>
        public MongoClient MongoClient { get; set; }

        public MongoDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _commands = new List<Func<Task>>();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="func">函数</param>
        public virtual void AddCommand(Func<Task> func) => _commands.Add(func);

        /// <summary>
        /// 保存变更
        /// </summary>
        public virtual async Task<int> SaveChanges()
        {
            ConfigureMongo();
            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();
                var commandTasks = _commands.Select(c => c());
                await Task.WhenAll(commandTasks);
                await Session.CommitTransactionAsync();
            }
            return _commands.Count;
        }

        /// <summary>
        /// 集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        public virtual IMongoCollection<TEntity> Collection<TEntity>()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="name">集合名</param>
        public virtual IMongoCollection<TEntity> Collection<TEntity>(string name)
        {
            ConfigureMongo();
            return Database.GetCollection<TEntity>(name);
        }

        /// <summary>
        /// 配置MongoDB
        /// </summary>
        protected void ConfigureMongo()
        {
            if (MongoClient != null)
                return;
            MongoClient = new MongoClient(_configuration["MongoSettings:Connection"]);
            Database = MongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"]);
        }
    }
}
