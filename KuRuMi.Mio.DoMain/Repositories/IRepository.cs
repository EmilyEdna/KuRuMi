using KuRuMi.Mio.DoMain.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Model.Repositories
{
    ///<summary>
    /// 在DDD中仓储只能操作聚合根
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>:IBaseRepository where TEntity : AggregateRoot
    {
        /// <summary>
        /// 直接拉姆达表达查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> LoadListAll(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 延缓拉姆达表达式查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> LoadAll(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 直接SQL查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<TEntity> LoadListForSql(string sql);
        /// <summary>
        /// 延缓SQL查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IQueryable<TEntity> LoadForSql(string sql);
        /// <summary>
        /// 根据聚合根的ID值，从仓储中读取聚合根。
        /// </summary>
        /// <param name="key">聚合根的ID值。</param>
        /// <returns>聚合根实例。</returns>
        TEntity GetKey(Guid key);
        /// <summary>
        /// 将指定的聚合根添加到仓储中。
        /// </summary>
        /// <param name="aggregateRoot">需要添加到仓储的聚合根实例。</param>
        void Add(TEntity aggregateRoot);
        /// <summary>
        /// 将指定的聚合根从仓储中移除。
        /// </summary>
        /// <param name="aggregateRoot">需要从仓储中移除的聚合根。</param>
        void Remove(TEntity aggregateRoot);
        /// <summary>
        /// 更新指定的聚合根。
        /// </summary>
        /// <param name="aggregateRoot">需要更新的聚合根。</param>
        void Update(TEntity aggregateRoot);
    }
}
