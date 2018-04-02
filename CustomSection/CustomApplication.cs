using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using ExpressMapper;
using Microsoft.EntityFrameworkCore;
using System.Web.Http;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Web;
using UmbracoCustomSection.App_Plugins.CustomSection.Controllers;
using UmbracoCustomSection.App_Plugins.CustomSection.Data;
using UmbracoCustomSection.App_Plugins.CustomSection.Models;
using UmbracoCustomSection.App_Plugins.CustomSection.ViewModels;

namespace UmbracoCustomSection.App_Plugins.CustomSection
{
    public class CustomApplication : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            var builder = new ContainerBuilder();

            //Register all controllers in Tree name space
            builder.RegisterApiControllers(typeof(CustomTreeController).Assembly);

            //register umbraco MVC + WebApi controllers used by the admin site
            builder.RegisterControllers(typeof(UmbracoApplication).Assembly);
            builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly);

            builder.Register(context =>
            {
                var options = new DbContextOptionsBuilder<CustomSectionDbContext>();
                options.UseInMemoryDatabase(databaseName: "CustomSection");

                var ctx = new CustomSectionDbContext(options.Options);

                return ctx;
            }).InstancePerRequest();

            var container = builder.Build();

            //Set the MVC DependencyResolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //Set the WebApi DependencyResolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //Seed the database
            CustomSectionDbInitializer.Initialize(DependencyResolver.Current.GetService<CustomSectionDbContext>());
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // this is where you can setup things when the application starts
            
            Mapper.Register<Node, NodeViewModel>();
            Mapper.Compile();
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // nothing is really started here yet..
        }
    }
}