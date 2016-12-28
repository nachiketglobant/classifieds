using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Classifieds.MasterDataAPI.Resolver;
using Classifieds.MastersData.BusinessServices;
using Classifieds.MastersData.Repository;
using Classifieds.Common;


namespace Classifieds.MasterDataAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IMasterDataService, MasterDataService>(new HierarchicalLifetimeManager());
            container.RegisterType<IMasterDataRepository, MasterDataRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDBRepository, DBRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ILogger, Logger>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "MastersData",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
