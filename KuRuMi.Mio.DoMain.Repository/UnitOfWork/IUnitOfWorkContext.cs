using KuRuMi.Mio.DoMain.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Repository.UnitOfWork
{
    public interface IUnitOfWorkContext : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 将指定的聚合根标注为“新建”状态。
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        void RegisterNew<TAggregateRoot>(TAggregateRoot obj)
            where TAggregateRoot : class, IAggregateRoot;
        /// <summary>
        /// 将指定的聚合根标注为“更改”状态。
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        void RegisterModified<TAggregateRoot>(TAggregateRoot obj)
            where TAggregateRoot : class, IAggregateRoot;
        /// <summary>
        /// 将指定的聚合根标注为“删除”状态。
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj)
            where TAggregateRoot : class, IAggregateRoot;
    }
}
