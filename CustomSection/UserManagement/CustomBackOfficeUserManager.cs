using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
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

        /// <summary>
        /// Called when a new user should be created.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override async Task<IdentityResult> CreateAsync(BackOfficeIdentityUser user)
        {
            // autolinked users have this role assigned before they are saved
            var isAutolinked = user.Roles.Any(r => r.RoleId == "autolinked");

            var result = await base.CreateAsync(user);

            if (isAutolinked)
            {
                // do custom stuff when a new user is auto linked
            }

            return result;
        }

        /// <summary>
        /// Called when a user adds their external login to their account.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public override async Task<IdentityResult> AddLoginAsync(int userId, UserLoginInfo login)
        {
            var result = await base.AddLoginAsync(userId, login);

            if (result.Succeeded && login.LoginProvider == ConfigurationManager.AppSettings["AdfsFederationServerIdentifier"])
            {
                // do custom stuff when an existing user links their account to their external login
            }

            return result;
        }

        /// <summary>
        /// Called when a user removes their external login from their account.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public override async Task<IdentityResult> RemoveLoginAsync(int userId, UserLoginInfo login)
        {
            var result = await base.RemoveLoginAsync(userId, login);

            if (result.Succeeded && login.LoginProvider == ConfigurationManager.AppSettings["AdfsFederationServerIdentifier"])
            {
                // do custom stuff when an existing user removes their extenal login from their account
            }

            return result;
        }
    }

}
