using System;
using Bing.Domains.Entities;

namespace Bing.Domains.Repositories.MongoDB
{
    /// <summary>
    /// MongoDB 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IMongoDbRepository<TEntity> : IMongoDbRepository<TEntity, Guid>
        where TEntity : class, IAggregateRoot, IKey<Guid>
    {
    }

    /// <summary>
    /// MongoDB 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public interface IMongoDbRepository<TEntity, in TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IAggregateRoot, IKey<TKey>
    {
    }
}
