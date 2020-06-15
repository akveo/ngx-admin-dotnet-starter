/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Services.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EntityFramework
{
    public class UserRepository : BaseDeletableRepository<User, DataContext>, IUserRepository<User>
    {
        public override async Task<User> Edit(User obj, ContextSession session)
        {
            var objectExists = await Exists(obj, session, true);
            using (var context = GetContext(session))
            {
                context.Entry(obj).State = objectExists ? EntityState.Modified : EntityState.Added;

                if (string.IsNullOrEmpty(obj.Password))
                {
                    context.Entry(obj).Property(x => x.Password).IsModified = false;
                }

                await context.SaveChangesAsync();
                return obj;
            }
        }

        public override async Task<User> Get(int id, ContextSession session, bool includeDeleted = false)
        {
            using (var context = GetContext(session))
            {
                return await GetEntities(context, includeDeleted)
                    .Where(obj => obj.Id == id)
                    .Include(u => u.UserRoles.Select(r => r.Role))
                    .Include(u => u.Settings)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<User> GetByLogin(string login, ContextSession session, bool includeDeleted = false)
        {
            using (var context = GetContext(session))
            {
                return await GetEntities(context, includeDeleted)
                    .Where(obj => obj.Login == login)
                    .Include(u => u.UserRoles.Select(r => r.Role))
                    .Include(u => u.Settings)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<User> GetByEmail(string email, ContextSession session, bool includeDeleted = false)
        {
            using (var context = GetContext(session))
            {
                return await GetEntities(context, includeDeleted)
                    .Where(obj => obj.Email == email)
                    .Include(u => u.UserRoles.Select(r => r.Role))
                    .Include(u => u.Settings)
                    .FirstOrDefaultAsync();
            }
        }
    }
}