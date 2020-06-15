/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Services.Infrastructure;
using Common.Utils;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Common.IdentityManagement
{
    public class RoleStore<TRole, TApplicationRole> : IRoleStore<TApplicationRole, int>
        where TRole : Role, new()
        where TApplicationRole : TRole, IRole<int>
    {
        protected readonly IRoleRepository<TRole> repository;
        protected ICurrentContextProvider contextProvider;
        protected ContextSession session;

        public RoleStore(ICurrentContextProvider contextProvider, IRoleRepository<TRole> repository)
        {
            this.repository = repository;
            this.contextProvider = contextProvider;
            session = contextProvider.GetCurrentContext();
        }
        public async Task CreateAsync(TApplicationRole role)
        {
            var baseRole = role.MapTo<TRole>();
            var newRole = await repository.Edit(baseRole, session);
            role.Id = newRole.Id;
            role.Name = newRole.Name;
            role.UserRoles = newRole.UserRoles;
        }

        public async Task DeleteAsync(TApplicationRole role)
        {
            await repository.Delete(role.Id, session);
        }

        public async Task<TApplicationRole> FindByIdAsync(int roleId)
        {
            var baseRole = await repository.Get(roleId, session);
            return baseRole.MapTo<TApplicationRole>();
        }

        public async Task<TApplicationRole> FindByNameAsync(string roleName)
        {
            var baseRole = await repository.Get(roleName, session);
            return baseRole.MapTo<TApplicationRole>();
        }

        public async Task UpdateAsync(TApplicationRole role)
        {
            var baseRole = role.MapTo<TRole>();
            var newRole = await repository.Edit(baseRole, session);
            role.Id = newRole.Id;
            role.Name = newRole.Name;
            role.UserRoles = newRole.UserRoles;
        }

        public void Dispose()
        {
        }
    }
}
