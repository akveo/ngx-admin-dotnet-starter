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
    public class UserClaimRepository: IUserClaimRepository<UserClaim>
    {
        protected DataContext GetContext(ContextSession session)
        {
            return new DataContext {Session = session};
        }

        public async Task<UserClaim> Add(UserClaim userClaim, ContextSession session)
        {
            using (var context = GetContext(session))
            {
                context.Entry(userClaim).State = EntityState.Added;
                await context.SaveChangesAsync();
                return userClaim;
            }
        }

        public async Task Delete(int userId, string claimType, string claimValue, ContextSession session)
        {
            using (var context = GetContext(session))
            {
                var itemsToDelete = await context.Set<UserClaim>().Where(cl =>
                    cl.UserId == userId && cl.ClaimType == claimType && cl.ClaimValue == claimValue).ToListAsync();
                context.Set<UserClaim>().RemoveRange(itemsToDelete);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IList<UserClaim>> GetByUserId(int userId, ContextSession session)
        {
            using (var context = GetContext(session))
            {
                var list = await context.Set<UserClaim>()
                    .AsNoTracking()
                    .Where(obj => obj.UserId == userId)
                    .ToListAsync();

                return list.ToList();
            }
        }

        public Task<IList<UserClaim>> EditMany(IList<UserClaim> userClaims, ContextSession session)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<UserClaim>> GetList(int userId, string claimType, string claimValue, ContextSession session)
        {
            throw new System.NotImplementedException();
        }
    }
}