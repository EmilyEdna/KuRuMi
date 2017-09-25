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
    /// 图片文件仓储实现
    /// </summary>
    public class ImgFilesRepositoryImpl : RepositoryImpl<ImgFiles>, IImgFilesRepositirory
    {
        public KurumiMioDbContext context => lazy.Context as KurumiMioDbContext;

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="aggregateRoot"></param>
        public override void Add(ImgFiles aggregateRoot)
        {
            base.Add(aggregateRoot);
        }
    }
}
