using Umbraco.Core;

namespace UmbracoCustomSection.App_Plugins.CustomSection
{
    public class CustomApplication : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // here you can setup your DI
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // this is where you can setup things when the application starts
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // nothing is really started here yet..
        }
    }
}