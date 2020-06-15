/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Common.IdentityManagement
{
    public static class UserManagerExtensions
    {
        public static async Task<ClaimsIdentity> CreateIdentityAsync<TApplicationUser>(this UserManager<TApplicationUser, int> manager, TApplicationUser user)
            where TApplicationUser : User, IUser<int>, new()
        {
            return await manager.CreateIdentityAsync(user, "JWT");
        }

    }
}
