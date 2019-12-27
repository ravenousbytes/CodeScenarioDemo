using AdministrationTool.Data.Services;
using AdministrationTool.Web.Mapping;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace AdministrationTool.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMappingProfile());
            });
            builder.RegisterInstance(config.CreateMapper()).As<IMapper>().SingleInstance();
            //Test data - in memory
            builder.RegisterType<TestUserData>().As<IUserData>().SingleInstance();
            //Data - database
            //builder.RegisterType<SqlUserData>().As<IUserData>().InstancePerRequest();
            //builder.RegisterType<AdministrationDbContext>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}