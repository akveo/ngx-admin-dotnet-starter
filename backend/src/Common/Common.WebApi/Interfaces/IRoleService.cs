/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.WebApi
{
    public interface IRoleService
    {
        Task<IdentityResult> AssignToRole(int userId, string roleName);
        Task<IdentityResult> UnassignRole(int userId, string roleName);
        Task<IList<string>> GetRoles(int userId);
    }
}