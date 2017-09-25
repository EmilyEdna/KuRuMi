using KuRuMi.Mio.DataObject.AutoMapperDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KuRuMi.Mio.DoMain.Infrastructure.IocManager;
using KuRuMi.Mio.DoMain.Model.Repositories;
using Autofac;
using KuRuMi.Mio.DoMain.Events.Handler;
using KuRuMi.Mio.DoMain.Events.Bus;

namespace KuRuMi.Mio.BootStarp
{

    /// <summary>
    /// 系统初始化
    /// </summary>
    public class OptionBootStarp
    {
        protected IEnumerable<Assembly> assembles { get; }
        protected IIocManager ioc { get; }
        public OptionBootStarp(IEnumerable<Assembly> ass)
        {
            assembles = ass;
            ioc = IocManager.Instance;
        }
        protected IEnumerable<Type> Repository => assembles.SelectMany(a => a.ExportedTypes.Where(t => t.GetInterfaces().Contains(typeof(IBaseRepository))));
        protected IEnumerable<Type> BaseDTO => assembles.SelectMany(a => a.ExportedTypes.Where(t => t.GetInterfaces().Contains(typeof(IAutoMapper))));
        protected IEnumerable<Type> Services => assembles.SelectMany(a => a.ExportedTypes.Where(t => t.GetInterfaces().Contains(typeof(IService))));
        /// <summary>
        /// 预加载
        /// </summary>
        public void Initialize()
        {
            //加载所有DTO
            BaseDTO.ToList().ForEach(s=> {
                var dtpye= Activator.CreateInstance(s) as IAutoMapper;
                ioc.build.RegisterInstance(dtpye).As<MapperConfigurationImpl>().SingleInstance().PropertiesAutowired();
            });
            //加载所有的仓储
            Repository.ToList().ForEach(s => {
                if (s.IsClass == true && s.IsGenericType == false)
                {
                    var dtpye = Activator.CreateInstance(s);
                    ioc.build.RegisterType(dtpye.GetType()).As(dtpye.GetType());
                }
            });
            //加载所有服务
            Services.ToList().ForEach(s =>
            {
                if (s.IsClass == true)
                {
                    var stype = Activator.CreateInstance(s);
                    ioc.build.RegisterType(stype.GetType()).As(stype.GetType());
                }
            });
            PostInit();
            EventBus.bus.RegisterAllHandler(assembles);
        }
        /// <summary>
        /// 注入
        /// </summary>
        protected void PostInit() {
            ioc.CompleteBuild();
        }
    }

}
