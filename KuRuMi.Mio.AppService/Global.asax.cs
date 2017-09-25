using KuRuMi.Mio.BootStarp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Routing;

namespace KuRuMi.Mio.AppService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            InitSystem();
        }

        protected void InitSystem() {
            var assembles = BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(n => n.FullName.StartsWith("KuRuMi.Mio"));
            OptionBootStarp boot = new OptionBootStarp(assembles);
            boot.Initialize();
        }
    }
}
