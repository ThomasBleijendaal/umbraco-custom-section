using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Umbraco.Core;
using Umbraco.Core.Configuration;
using Umbraco.Core.Models.Identity;
using Umbraco.Core.Security;
using Umbraco.Web;
using Umbraco.Web.Security.Identity;
using UmbracoCustomSection.App_Plugins.CustomSection.TwoFactorProviders;

namespace UmbracoCustomSection.App_Plugins.CustomSection.UserManagement
{
    class CustomBackOfficeUserManager : BackOfficeUserManager, IUmbracoBackOfficeTwoFactorOptions
    {
        public CustomBackOfficeUserManager(IUserStore<BackOfficeIdentityUser, int> store) : base(store)
        {
        }

        /// <summary>
        /// Creates a BackOfficeUserManager instance with all default options and the default BackOfficeUserManager 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="userService"></param>
        /// <param name="externalLoginService"></param>
        /// <param name="membershipProvider"></param>
        /// <returns></returns>
        public static CustomBackOfficeUserManager Create(IdentityFactoryOptions<CustomBackOfficeUserManager> options, ApplicationContext applicationContext)
        {
            var membershipProvider = MembershipProviderExtensions.GetUsersMembershipProvider().AsUmbracoMembershipProvider();

            var userService = applicationContext.Services.UserService;
            var entityService = applicationContext.Services.EntityService;
            var externalLoginService = applicationContext.Services.ExternalLoginService;

            var contentSectionConfig = UmbracoConfig.For.UmbracoSettings().Content;

            var manager = new CustomBackOfficeUserManager(new CustomBackOfficeUserStore(userService, entityService, externalLoginService, membershipProvider));

            manager.InitUserManager(manager, membershipProvider, options.DataProtectionProvider, contentSectionConfig);

            manager.RegisterTwoFactorProvider("DefaultProvider", new DefaultTwoFactorProvider(options.DataProtectionProvider.Create("DefaultProvider")));

            return manager;
        }

        /// <summary>
        /// Override to return true
        /// </summary>
        public override bool SupportsUserTwoFactor
        {
            get { return true; }
        }

        /// <summary>
        /// Return the view for the 2FA screen
        /// </summary>
        /// <param name="owinContext"></param>
        /// <param name="umbracoContext"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public string GetTwoFactorView(IOwinContext owinContext, UmbracoContext umbracoContext, string username)
        {
            var user = ApplicationContext.Current.Services.UserService.GetByUsername(username);

            // TODO: determine whether 2FA has been setup
            var userSetup = true;

            if (userSetup)
            {
                return "/App_Plugins/CustomSection/backoffice/twoFactor/twoFactorLogin.html";
            }
            else
            {
                return "/App_Plugins/CustomSection/backoffice/twoFactor/twoFactorEmail.html";
            }
        }
    }

}
