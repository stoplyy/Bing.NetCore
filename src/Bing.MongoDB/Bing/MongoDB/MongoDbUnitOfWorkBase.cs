using System;
using System.Threading.Tasks;
using Bing.Datas.UnitOfWorks;
using Microsoft.Extensions.Configuration;

namespace Bing.MongoDB
{
    /// <summary>
    /// MongoDB 工作单元基类
    /// </summary>
    public abstract class MongoDbUnitOfWorkBase : MongoDbContext, IUnitOfWork
    {
        protected MongoDbUnitOfWorkBase(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// 提交，返回影响的行数
        /// </summary>
        public int Commit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 提交，返回影响的行数
        /// </summary>
        public async Task<int> CommitAsync() => await base.SaveChanges();
    }
}
