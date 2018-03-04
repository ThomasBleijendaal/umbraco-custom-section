using Microsoft.AspNet.Identity.Owin;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.WsFederation;
using Owin;
using System;
using System.Configuration;
using System.Threading.Tasks;
using Umbraco.Core.Models.Identity;
using Umbraco.Web.Security.Identity;
using Constants = Umbraco.Core.Constants;

namespace UmbracoCustomSection.App_Plugins.CustomSection.Extensions
{
    public static class AdfsAuthenticationExtensions
    {
        public static IAppBuilder UseUmbracoBackOfficeAdfsAuthentication(
            this IAppBuilder app,
            string caption = "AD FS",
            string style = "btn-microsoft",
            string icon = "fa-windows",
            string[] defaultUserGroups = null,
            string defaultCulture = "en-GB",
            Action<BackOfficeIdentityUser, ExternalLoginInfo> onAutoLinking = null,
            Func<SecurityTokenValidatedNotification<WsFederationMessage, WsFederationAuthenticationOptions>, Task> onSecurityTokenValidated = null
        )
        {
            var adfsMetadataEndpoint = ConfigurationManager.AppSettings["AdfsMetadataEndpoint"];
            var adfsRelyingParty = ConfigurationManager.AppSettings["AdfsRelyingParty"];
            var adfsFederationServerIdentifier = ConfigurationManager.AppSettings["AdfsFederationServerIdentifier"];

            app.SetDefaultSignInAsAuthenticationType(Constants.Security.BackOfficeExternalAuthenticationType);

            var wsFedOptions = new WsFederationAuthenticationOptions
            {
                Wtrealm = adfsRelyingParty,
                MetadataAddress = adfsMetadataEndpoint,
                SignInAsAuthenticationType = Constants.Security.BackOfficeExternalAuthenticationType,
                Caption = caption
            };

            wsFedOptions.ForUmbracoBackOffice(style, icon);

            wsFedOptions.AuthenticationType = adfsFederationServerIdentifier;

            var autoLinking = new ExternalSignInAutoLinkOptions(
                autoLinkExternalAccount: true,
                defaultUserGroups: defaultUserGroups ?? new[] { "writer" },
                defaultCulture: defaultCulture)
            {
                OnAutoLinking = onAutoLinking
            };

            if (onSecurityTokenValidated != null)
            {
                wsFedOptions.Notifications = new WsFederationAuthenticationNotifications
                {
                    SecurityTokenValidated = onSecurityTokenValidated
                };
            }

            wsFedOptions.SetExternalSignInAutoLinkOptions(autoLinking);

            app.UseWsFederationAuthentication(wsFedOptions);

            return app;
        }
    }
}