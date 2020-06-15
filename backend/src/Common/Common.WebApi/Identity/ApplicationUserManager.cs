/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the �docs� folder for license information on type of purchased license.
*/

using Common.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace Common.WebApi.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store) : base(store)
        {
            UserValidator = new UserValidator<ApplicationUser, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4
            };

            UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(Startup.DataProtectionProvider.Create("ASP.NET Identity"));
        }

        /// <summary>
        /// Overridden method to change user's password without provided current password
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="currentPassword">Current Password</param>
        /// <param name="newPassword">New Password</param>
        /// <returns>IdentityResult</returns>
        public override async Task<IdentityResult> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var passwordStore = Store as IUserPasswordStore<ApplicationUser, int>;
            var user = await base.FindByIdAsync(userId);
            if (user == null)
            {
                throw new System.InvalidOperationException("User not found");
            }

            var result = await UpdatePassword(passwordStore, user, newPassword);
            if (!result.Succeeded)
            {
                return result;
            }

            return await UpdateAsync(user);
        }
    }
}
