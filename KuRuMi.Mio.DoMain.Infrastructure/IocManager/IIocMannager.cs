using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Infrastructure.IocManager
{
    /// <summary>
    /// IOC
    /// </summary>
    public interface IIocManager
    {
        /// <summary>
        /// 创建容器
        /// </summary>
        IContainer Con { get; }

        /// <summary>
        /// 注册容器
        /// </summary>
        ContainerBuilder build { get; }


        /// <summary>
        /// 完成注册
        /// </summary>
        void CompleteBuild();

        /// <summary>
        /// 取出实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();
    }
}
