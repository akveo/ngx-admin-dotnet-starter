/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Services.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EntityFramework
{
    public class UserRoleRepository: IUserRoleRepository<UserRole>
    {
        protected DataContext GetContext(ContextSession session)
        {
            return new DataContext { Session = session};
        }

        public async Task<UserRole> Add(UserRole userRole, ContextSession session)
        {
            using (var context = GetContext(session))
            {
                context.Entry(userRole).State = EntityState.Added;
                await context.SaveChangesAsync();
                return userRole;
            }
        }

        public async Task Delete(int userId, int roleId, ContextSession session)
        {
            using (var context = GetContext(session))
            {
                var itemToDelete = new UserRole { UserId = userId, RoleId = roleId};
                context.Entry(itemToDelete).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task<UserRole> Get(int userId, int roleId, ContextSession session)
        {
            using (var context = GetContext(session))
            {
                return await context.Set<UserRole>()
                    .Where(obj => obj.RoleId == roleId && obj.UserId == userId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<IList<string>> GetByUserId(int userId, ContextSession session)
        {
            using (var context = GetContext(session))
            {
                return await context.Set<UserRole>()
                    .AsNoTracking()
                    .Where(obj => obj.UserId == userId)
                    .Include(obj => obj.Role)
                    .Select(obj => obj.Role.Name)
                    .ToListAsync();
            }
        }
    }
}