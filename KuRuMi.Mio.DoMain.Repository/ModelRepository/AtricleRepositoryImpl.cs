using KuRuMi.Mio.DoMain.Model.Model;
using KuRuMi.Mio.DoMain.Model.Repositories.EntityRepository;
using KuRuMi.Mio.DoMain.Repository.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Repository.ModelRepository
{
    /// <summary>
    /// 文章实现仓库
    /// </summary>
    public class AtricleRepositoryImpl: RepositoryImpl<Atricle>, IAtricleRepository
    {
        public KurumiMioDbContext context => lazy.Context as KurumiMioDbContext;
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="aggregateRoot"></param>
        public override void Add(Atricle aggregateRoot)
        {
            base.Add(aggregateRoot);
        }
    }
}
