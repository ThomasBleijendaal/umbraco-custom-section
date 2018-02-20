using Microsoft.Owin;
using Owin;
using System;
using Umbraco.Core;
using Umbraco.Core.Models.Identity;
using Umbraco.Web;
using Umbraco.Web.Security.Identity;
using UmbracoCustomSection.App_Plugins.CustomSection.Startup;
using UmbracoCustomSection.App_Plugins.CustomSection.UserManagement;

[assembly: OwinStartup("CustomSectionOwinStartup", typeof(CustomSectionOwinStartup))]
namespace UmbracoCustomSection.App_Plugins.CustomSection.Startup
{
    public class CustomSectionOwinStartup : UmbracoDefaultOwinStartup
    {
        protected override void ConfigureServices(IAppBuilder app)
        {
            app.SetUmbracoLoggerFactory();

            var applicationContext = ApplicationContext.Current;

            // Configure customized user manager
            app.ConfigureUserManagerForUmbracoBackOffice<CustomBackOfficeUserManager, BackOfficeIdentityUser>(
                applicationContext,
                (options, context) =>
                {
                    return CustomBackOfficeUserManager.Create(options, applicationContext);
                });
        }

        protected override void ConfigureUmbracoAuthentication(IAppBuilder app)
        {
            base.ConfigureUmbracoAuthentication(app);

            app.UseTwoFactorSignInCookie(Constants.Security.BackOfficeTwoFactorAuthenticationType, TimeSpan.FromMinutes(5));
        }
    }
}