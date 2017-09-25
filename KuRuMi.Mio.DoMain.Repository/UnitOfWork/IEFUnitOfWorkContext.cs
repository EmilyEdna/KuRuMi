using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Repository.UnitOfWork
{
    /// <summary>
    /// 表示是EF仓储单元的标识
    /// </summary>
    public interface IEFUnitOfWorkContext : IUnitOfWorkContext
    {
        DbContext Context { get; }
    }
}
