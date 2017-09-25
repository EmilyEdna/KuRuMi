using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Repository.UnitOfWork
{
    /// <summary>
    /// 表示所有集成该接口都是工作单元的一种实现
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 提交
        /// </summary>
        void Commit();

        /// <summary>
        /// 异步提交
        /// </summary>
        /// <returns></returns>
        Task CommitSyncAsync();

        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();

        /// <summary>
        /// 已经提交过了
        /// </summary>
        bool Committed { get; }

        /// <summary>
        /// 事务支持
        /// </summary>
        //bool DistributedTransactionSupported { get; }
    }
}
