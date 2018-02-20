using System.Threading.Tasks;
using Umbraco.Core.Models.Identity;
using Umbraco.Core.Security;
using Umbraco.Core.Services;

namespace UmbracoCustomSection.App_Plugins.CustomSection.UserManagement
{
    public class CustomBackOfficeUserStore : BackOfficeUserStore
    {
        public CustomBackOfficeUserStore(IUserService userService, IEntityService entityService, IExternalLoginService externalLoginService, MembershipProviderBase usersMembershipProvider)
            : base(userService, entityService, externalLoginService, usersMembershipProvider)
        {
        }

        /// <summary>
        /// Override to support setting whether two factor authentication is enabled for the user
        /// </summary>
        /// <param name="user"/><param name="enabled"/>
        /// <returns/>
        /// <remarks>
        /// Two factor authentication is always enabled and cannot be deactivated.
        /// </remarks>
        public override Task SetTwoFactorEnabledAsync(BackOfficeIdentityUser user, bool enabled)
        {
            user.TwoFactorEnabled = true;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Returns whether two factor authentication is enabled for the user
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        /// <remarks>
        /// Two factor authentication is always enabled and cannot be deactivated.
        /// </remarks>
        public override Task<bool> GetTwoFactorEnabledAsync(BackOfficeIdentityUser user)
        {
            return Task.FromResult(true);
        }
    }
}