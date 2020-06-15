/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Services.Infrastructure;
using Common.Utils;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Common.IdentityManagement
{
    public class UserStore<TUser, TApplicationUser, TRole, TApplicationRole, TUserRole, TApplicationUserRole,
        TUserClaim, TApplicationUserClaim> :
        IUserStore<TApplicationUser, int>,
        IUserPasswordStore<TApplicationUser, int>,
        IUserEmailStore<TApplicationUser, int>,
        IUserRoleStore<TApplicationUser, int>,
        IUserClaimStore<TApplicationUser, int>
        where TUser : User, new()
        where TApplicationUser : TUser, IUser<int>
        where TRole : Role, new()
        where TUserRole : UserRole, new()
        where TApplicationRole : TRole, IRole<int>, new()
        where TApplicationUserRole : TUserRole, new()
        where TUserClaim : UserClaim, new()
        where TApplicationUserClaim : TUserClaim, new()
    {
        protected readonly IIdentityUserRepository<TUser> userRepository;
        protected readonly IUserClaimRepository<TUserClaim> userClaimRepository;
        protected readonly IUserRoleRepository<TUserRole> userRoleRepository;
        protected readonly IRoleStore<TApplicationRole, int> roleStore;
        protected ICurrentContextProvider contextProvider;
        protected readonly ContextSession session;

        public UserStore(
            ICurrentContextProvider contextProvider,
            IRoleStore<TApplicationRole, int> roleStore,
            IIdentityUserRepository<TUser> userRepository,
            IUserRoleRepository<TUserRole> userRoleRepository,
            IUserClaimRepository<TUserClaim> userClaimRepository)
        {
            this.contextProvider = contextProvider;
            this.userRepository = userRepository;
            this.roleStore = roleStore;
            this.userRoleRepository = userRoleRepository;
            this.userClaimRepository = userClaimRepository;
            session = contextProvider.GetCurrentContext();
        }

        public void Dispose()
        {
        }

        public async Task CreateAsync(TApplicationUser user)
        {
            var newUser = await userRepository.Edit(user.MapTo<TUser>(), session);

            user.Id = newUser.Id;
            user.Email = newUser.Email;
            user.FirstName = newUser.FirstName;
            user.LastName = newUser.LastName;
            user.Password = newUser.Password;
            user.Login = newUser.Login;
            user.UserRoles = newUser.UserRoles;
            user.Claims = newUser.Claims;
        }

        public async Task DeleteAsync(TApplicationUser user)
        {
            await userRepository.Delete(user.Id, session);
        }

        public async Task<TApplicationUser> FindByIdAsync(int userId)
        {
            var baseUser = await userRepository.GetById(userId, session, true);
            return baseUser.MapTo<TApplicationUser>();
        }

        public async Task<TApplicationUser> FindByNameAsync(string login)
        {
            var baseUser = await userRepository.GetByLogin(login, session, true);
            return baseUser.MapTo<TApplicationUser>();
        }

        public async Task UpdateAsync(TApplicationUser user)
        {
            var newUser = await userRepository.Edit(user.MapTo<TUser>(), session);
            user.Id = newUser.Id;
            user.Email = newUser.Email;
            user.FirstName = newUser.FirstName;
            user.LastName = newUser.LastName;
            user.Password = newUser.Password;
            user.Login = newUser.Login;
            user.UserRoles = newUser.UserRoles;
            user.Claims = newUser.Claims;
        }

        public Task SetPasswordHashAsync(TApplicationUser user, string passwordHash)
        {
            user.Password = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(TApplicationUser user)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(TApplicationUser user)
        {
            return Task.FromResult(user?.Password != null && user.Password.Length > 0);
        }

        public Task SetEmailAsync(TApplicationUser user, string email)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(TApplicationUser user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(TApplicationUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public async Task<TApplicationUser> FindByEmailAsync(string email)
        {
            var baseUser = await userRepository.GetByEmail(email, session, true);
            return baseUser.MapTo<TApplicationUser>();
        }

        public async Task AddToRoleAsync(TApplicationUser user, string roleName)
        {
            var roleEntity = await roleStore.FindByNameAsync(roleName);
            if (roleEntity != null)
            {
                var userRole = new TApplicationUserRole {UserId = user.Id, RoleId = roleEntity.Id};
                await userRoleRepository.Add(userRole.MapTo<TUserRole>(), session);
            }
        }

        public async Task RemoveFromRoleAsync(TApplicationUser user, string roleName)
        {
            var roleEntity = await roleStore.FindByNameAsync(roleName);
            if (roleEntity != null)
            {
                var roleId = roleEntity.Id;
                var userId = user.Id;
                var userRole = await userRoleRepository.Get(userId, roleId, session);
                if (userRole != null)
                {
                    await userRoleRepository.Delete(userId, roleId, session);
                }
            }
        }

        public async Task<IList<string>> GetRolesAsync(TApplicationUser user)
        {
            var userId = user.Id;
            return await userRoleRepository.GetByUserId(userId, session);
        }

        public async Task<bool> IsInRoleAsync(TApplicationUser user, string roleName)
        {
            var role = await roleStore.FindByNameAsync(roleName);
            if (role != null)
            {
                var userId = user.Id;
                var roleId = role.Id;
                var userRole = await userRoleRepository.Get(userId, roleId, session);
                if (userRole != null)
                    return true;
            }

            return false;
        }

        public async Task<IList<Claim>> GetClaimsAsync(TApplicationUser user)
        {
            if (user.Claims == null || user.Claims.Count == 0)
            {
                var claims = await userClaimRepository.GetByUserId(user.Id, session);
                return claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
            }

            return user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
        }

        public async Task AddClaimAsync(TApplicationUser user, Claim claim)
        {
            var userClaim = new TUserClaim {UserId = user.Id, ClaimType = claim.Type, ClaimValue = claim.Value};
            await userClaimRepository.Add(userClaim, session);
        }

        public async Task RemoveClaimAsync(TApplicationUser user, Claim claim)
        {
            await userClaimRepository.Delete(user.Id, claim.Type, claim.Value, session);
            var claimsToDelete = user.Claims.Where(cl => cl.ClaimValue == claim.Value && cl.ClaimType == claim.Type);
            foreach (var cl in claimsToDelete)
            {
                user.Claims.Remove(cl);
            }
        }
    }
}