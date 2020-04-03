﻿using AdventureWorks.Services.HumanResources;
using AdventureWorks.Services.Production;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Web.Mvc;
using System.Web.Http;
using AdventureWorks.Services.Documents;
using AdventureWorks.Services.Interfaces;

namespace AdventureWorks.Infrastructure
{
    public class AutofacConfig
    {
        public static void ConfigureContainerForMvc(Type app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(app.Assembly);
            RegisterTypes(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        public static void ConfigureContainerForWebApi(HttpConfiguration config, Type app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(app.Assembly);
            RegisterTypes(builder);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<DepartmentService>().As<IDepartmentService>().InstancePerRequest();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerRequest();
            builder.RegisterType<AzureFileUploader>().As<IFileUploader>().InstancePerRequest();
        }
    }
}