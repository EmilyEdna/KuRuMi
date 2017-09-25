using KuRuMi.Mio.DoMain.Model.BaseModel;
using KuRuMi.Mio.DoMain.Infrastructure;
using KuRuMi.Mio.DoMain.Repository.EFRepository;
using KuRuMi.Mio.DoMain.Repository.UnitOfWork;
using System.Data.Entity;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Repository.BaseUnitOfWork
{
    /// <summary>
    /// 工作单元上下文
    /// </summary>
    public class UnitOfWorkContext: DisposableObject, IEFUnitOfWorkContext
    {
        private KurumiMioDbContext kurumi = null;

        public UnitOfWorkContext() {
            kurumi = new KurumiMioDbContext();
        }

        public DbContext Context { get { return kurumi; } }

        #region 工作单元
        public bool Committed { get; protected set; }

        /// <summary>
        /// 同步提交
        /// </summary>
        public void Commit()
        {
            if (!Committed)
            {
                Context.SaveChanges();
                Committed = true;
            }
        }

        /// <summary>
        /// 异步提交
        /// </summary>
        /// <returns></returns>
        public async Task CommitSyncAsync()
        {
            if (!Committed)
            {
                await Context.SaveChangesAsync();
                Committed = true;
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!Committed)
                    Commit();
                Context.Dispose();
                kurumi.Dispose();
            }
        }

        /// <summary>
        /// 回滚
        /// </summary>
        public void Rollback()
        {
            Committed = false;
        }
        #endregion

        #region IEFUnitOfWorkContext接口
        /// <summary>
        /// 删除未提交
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="obj"></param>
        public void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj) where TAggregateRoot : class, IAggregateRoot
        {
            Context.Entry(obj).State = EntityState.Deleted;
            Committed = false;
        }

        /// <summary>
        /// 修改未提交
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="obj"></param>
        public void RegisterModified<TAggregateRoot>(TAggregateRoot obj) where TAggregateRoot : class, IAggregateRoot
        {
            if (Context.Entry(obj).State == EntityState.Detached)
            {
                Context.Set<TAggregateRoot>().Attach(obj);
            }
            Context.Entry(obj).State = EntityState.Modified;
            Committed = false;
        }

        /// <summary>
        /// 新建未提交
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="obj"></param>
        public void RegisterNew<TAggregateRoot>(TAggregateRoot obj) where TAggregateRoot : class, IAggregateRoot
        {
            var state = Context.Entry(obj).State;
            if (state == EntityState.Detached)
            {
                Context.Entry(obj).State = EntityState.Added;
            }
            Committed = false;
        }
        #endregion
    }
}
