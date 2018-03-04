using Microsoft.Owin;
using Owin;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models.Identity;
using Umbraco.Web;
using Umbraco.Web.Security.Identity;
using UmbracoCustomSection.App_Plugins.CustomSection.Extensions;
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

            app.UseUmbracoBackOfficeAdfsAuthentication(
                caption: "Azure Active Directory",
                style: "btn-microsoft",
                icon: "fa-windows",

                defaultUserGroups: new[] { "writer" },
                defaultCulture: "en-US",

                onAutoLinking: (user, externalLogin) =>
                {
                    // flag indicating that this new user has been autolinked from an external login
                    user.AddRole("autolinked");
                },

                onSecurityTokenValidated: (message) =>
                {
                    var emailClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
                    var nameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

                    var identity = message.AuthenticationTicket.Identity;

                    if (!identity.HasClaim(c => c.Type == emailClaimType) && identity.HasClaim(c => c.Type == nameClaimType))
                    {
                        var nameClaim = identity.Claims.First(c => c.Type == nameClaimType);
                        var emailClaim = new Claim(emailClaimType, nameClaim.Value);

                        identity.AddClaim(emailClaim);
                    }

                    return Task.FromResult(0);
                });
        }
    }
}