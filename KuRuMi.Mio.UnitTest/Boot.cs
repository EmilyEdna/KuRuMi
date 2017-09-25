using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KuRuMi.Mio.DoMain.Repository.ModelRepository;
using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO;
using KuRuMi.Mio.DataObject.AutoMapperDTO;
using AutoMapper;
using KuRuMi.Mio.DoMain.Model.Model;
using KuRuMi.Mio.DoMain.Infrastructure.Logger;

namespace KuRuMi.Mio.UnitTest
{
    [TestClass]
    public class Boot
    {
        [TestMethod]
        public void TestMethod()
        {
            //IIocManager ioc = new IocManager();
            //var s = ioc.Resolve<CostomRepositoryImpl>();
            UserRepositoryImpl s = new UserRepositoryImpl();
            Sys_UserDTO u = new Sys_UserDTO() ;
            u.Id = Guid.NewGuid();
            u.userName = "123";
            u.passWord = "456";
            u.email = "kk@123.com";
            MapperConfigurationImpl ip = new MapperConfigurationImpl();
           var z =  Mapper.Map<Sys_UserDTO, Sys_User>(u);
            s.Add(z);
            UnitExtension.Log("添加成功");
        }
    }
}
