using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System.Threading.Tasks;
using TwoFactorAuthNet;
using Umbraco.Core.Models.Identity;

namespace UmbracoCustomSection.App_Plugins.CustomSection.TwoFactorProviders
{
    public class DefaultTwoFactorProvider : DataProtectorTokenProvider<BackOfficeIdentityUser, int>, IUserTokenProvider<BackOfficeIdentityUser, int>
    {
        public DefaultTwoFactorProvider(IDataProtector protector) : base(protector)
        {
        }

        Task<bool> IUserTokenProvider<BackOfficeIdentityUser, int>.IsValidProviderForUserAsync(UserManager<BackOfficeIdentityUser, int> manager, BackOfficeIdentityUser user)
        {
            return Task.FromResult(true);
        }

        Task<bool> IUserTokenProvider<BackOfficeIdentityUser, int>.ValidateAsync(string purpose, string token, UserManager<BackOfficeIdentityUser, int> manager, BackOfficeIdentityUser user)
        {
            var tfa = new TwoFactorAuth("CustomSection");

            return Task.FromResult(tfa.VerifyCode("XANIK3POC23RCRYN", token));
        }
    }
}