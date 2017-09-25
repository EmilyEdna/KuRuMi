using KuRuMi.Mio.DoMain.Model.BaseModel;
using KuRuMi.Mio.DoMain.Model.Repositories;
using KuRuMi.Mio.DoMain.MongoDbCache.MongoDbCache;
using KuRuMi.Mio.DoMain.RedisCache;
using KuRuMi.Mio.DoMain.Repository.BaseUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Repository.EFRepository
{
    /// <summary>
    /// 仓储的泛型实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryImpl<TEntity> : IRepository<TEntity> where TEntity : AggregateRoot
    {
        public readonly UnitOfWorkContext lazy = null;
        //public readonly RedisCachingImpl Redis = null;
        //public readonly MongoDbCacheService Mongo = null;
        public RepositoryImpl()
        {
            lazy = new UnitOfWorkContext();
            //Redis = new RedisCachingImpl();
            //Mongo = new MongoDbCacheService();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="aggregateRoot"></param>
        public virtual void Add(TEntity aggregateRoot)
        {
            lazy.RegisterNew<TEntity>(aggregateRoot);
            lazy.Commit();       
        }

        /// <summary>
        /// 通过key获取聚合根
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TEntity GetKey(Guid key)
        {
            return lazy.Context.Set<TEntity>().Find(key);
        }
        /// <summary>
        /// 拉姆达查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> LoadAll(Expression<Func<TEntity, bool>> predicate)
        {
            return lazy.Context.Set<TEntity>().AsNoTracking().Where(predicate).AsQueryable();
        }
        /// <summary>
        /// Sql复杂查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> LoadForSql(string sql)
        {
            return lazy.Context.Set<TEntity>().SqlQuery(sql).AsNoTracking().AsQueryable();
        }
        /// <summary>
        /// 拉姆达查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> LoadListAll(Expression<Func<TEntity, bool>> predicate)
        {
            return lazy.Context.Set<TEntity>().AsNoTracking().Where(predicate).ToList();
        }
        /// <summary>
        /// Sql复杂查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> LoadListForSql(string sql)
        {
            return lazy.Context.Set<TEntity>().SqlQuery(sql).AsNoTracking().ToList();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="aggregateRoot"></param>
        public virtual void Remove(TEntity aggregateRoot)
        {
            lazy.RegisterDeleted<TEntity>(aggregateRoot);
            lazy.Commit();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="aggregateRoot"></param>
        public virtual void Update(TEntity aggregateRoot)
        {
            lazy.RegisterModified<TEntity>(aggregateRoot);
            lazy.Commit();
        }

    }
}
