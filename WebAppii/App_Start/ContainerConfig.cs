using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppii.Controllers;
using WebAppii.Repository.Common;
using WebAppii.Service.Common;
using WebAppii.Service;
using Autofac.Integration.WebApi;
using System.Reflection;

namespace WebAppii.App_Start
{
    public class ContainerConfig
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<HoodieService>().As<IHoodieService>();
            builder.RegisterType<HoodieRepository>().As<IHoodieRepository>();
            return builder.Build();
        }
    }
}