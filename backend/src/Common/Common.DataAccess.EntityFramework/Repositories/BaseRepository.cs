/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System;
using System.Collections.Generic;
using Common.Entities;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EntityFramework
{
    public abstract class BaseRepository<TType, TContext>
        where TType : BaseEntity, new()
        where TContext : DataContext, new()
    {
        protected virtual IQueryable<TType> GetEntities(TContext context)
        {
            return context.Set<TType>().AsQueryable().AsNoTracking();
        }

        protected TContext GetContext(ContextSession session)
        {
            return new TContext {Session = session};
        }

        public virtual async Task<IEnumerable<TType>> GetList(ContextSession session)
        {
            using (var context = GetContext(session))
            {
                return await GetEntities(context).ToListAsync();
            }
        }

        public virtual async Task<TType> Get(int id, ContextSession session)
        {
            using (var context = GetContext(session))
            {
                return await GetEntities(context).Where(obj => obj.Id == id).FirstOrDefaultAsync();
            }
        }

        public virtual async Task<bool> Exists(TType obj, ContextSession session)
        {
            using (var context = GetContext(session))
            {
                return await GetEntities(context).Where(x => x.Id == obj.Id).CountAsync() > 0;
            }
        }

        public virtual async Task<TType> Edit(TType obj, ContextSession session)
        {
            var objectExists = await Exists(obj, session);
            using (var context = GetContext(session))
            {
                context.Entry(obj).State = objectExists ? EntityState.Modified : EntityState.Added;
                await context.SaveChangesAsync();
                return obj;
            }
        }

        public virtual async Task Delete(int id, ContextSession session)
        {
            using (var context = GetContext(session))
            {
                var itemToDelete = new TType {Id = id};
                context.Entry(itemToDelete).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
}