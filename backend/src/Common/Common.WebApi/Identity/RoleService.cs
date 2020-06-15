/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.WebApi.Identity
{
    public class RoleService<TUser, TRole> : IRoleService
        where TUser : Entities.User, IUser<int>
        where TRole : Entities.Role, IRole<int>, new()
    {
        protected readonly UserManager<TUser, int> userManager;
        protected readonly RoleManager<TRole, int> roleManager;

        public RoleService(UserManager<TUser, int> userManager, RoleManager<TRole, int> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IdentityResult> AssignToRole(int userId, string roleName)
        {
            if (await roleManager.RoleExistsAsync(roleName))
            {
                return await userManager.AddToRoleAsync(userId, roleName);
            }

            return IdentityResult.Failed("Invalid role name");
        }

        public async Task<IdentityResult> UnassignRole(int userId, string roleName)
        {
            if (await roleManager.RoleExistsAsync(roleName))
            {
                return await userManager.RemoveFromRoleAsync(userId, roleName);
            }

            return IdentityResult.Failed("Invalid role name");
        }

        public Task<IList<string>> GetRoles(int userId)
        {
            return userManager.GetRolesAsync(userId);
        }
    }
}