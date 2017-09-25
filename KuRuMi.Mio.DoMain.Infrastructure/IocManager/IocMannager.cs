using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Infrastructure.IocManager
{
    /// <summary>
    /// Ioc实现类
    /// </summary>
    public class IocManager : IIocManager
    {

        private static readonly Lazy<IocManager> lazy = new Lazy<IocManager>(() => new IocManager());
        
        public static IocManager Instance { get => lazy.Value; }

        private readonly ContainerBuilder container = null;

        public IocManager()
        {
            container = new ContainerBuilder();
            build = container;
        }


        #region IOC
        /// <summary>
        /// 创建容器
        /// </summary>
        public IContainer Con { get; private set; }

        /// <summary>
        /// 注册容器
        /// </summary>
        public ContainerBuilder build { get; }

        /// <summary>
        /// 完成注册
        /// </summary>
        public void CompleteBuild()
        {
            Con = build.Build();
        }

        /// <summary>
        /// 取出实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return Con.Resolve<T>();
        }
        #endregion
    }
}
