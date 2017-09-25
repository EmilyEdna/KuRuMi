using KuRuMi.Mio.DoMain.Model.Model;
using KuRuMi.Mio.DoMain.Model.Repositories.EntityRepository;
using KuRuMi.Mio.DoMain.Repository.EFRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Repository.ModelRepository
{
    /// <summary>
    /// 博客仓储实现
    /// </summary>
    public class BlogsRepositoryImpl: RepositoryImpl<Blogs>, IBlogsRepository
    {
        public KurumiMioDbContext context => lazy.Context as KurumiMioDbContext;
        /// <summary>
        /// 保存博客
        /// </summary>
        /// <param name="aggregateRoot"></param>
        public override void Add(Blogs aggregateRoot)
        {
            base.Add(aggregateRoot);
        }
        /// <summary>
        /// 获取博客部分信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override IQueryable<Blogs> LoadForSql(string sql)
        {
            return base.LoadForSql(sql);
        }
    }
}
