using KuRuMi.Mio.DoMain.Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Repository.EFRepository
{
    /// <summary>
    /// EF上下文
    /// </summary>
    public class KurumiMioDbContext : DbContext, IDbContext
    {
        public KurumiMioDbContext() : base("mio") { }

        public virtual DbSet<Sys_User> user { get; set; }
        public virtual DbSet<Banner> banner { get; set; }
        public virtual DbSet<Blogs> blogs { get; set; }
        public virtual DbSet<Atricle> atricle { get; set; }
        public virtual DbSet<ImgFiles> imgfiles { get; set; }
    }
}
