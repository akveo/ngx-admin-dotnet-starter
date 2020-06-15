using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;

namespace Common.DataAccess.EntityFramework
{
    public abstract class BaseDeletableRepository<TType, TContext>
        where TType : DeletableEntity, new()
        where TContext : DataContext, new()
    {
        protected TContext GetContext(ContextSession session)
        {
            return new TContext {Session = session};
        }

        protected IQueryable<TType> GetEntities(TContext context, bool includeDeleted = false)
        {
            var query = context.Set<TType>().AsQueryable();
            if (!includeDeleted)
            {
                query = query.Where(obj => !obj.IsDeleted);
            }

            return query.AsNoTracking();
        }

        public virtual async Task<IEnumerable<TType>> GetList(ContextSession session, bool includeDeleted = false)
        {
            using (var context = GetContext(session))
            {
                return await GetEntities(context, includeDeleted).ToListAsync();
            }
        }

        public virtual async Task<TType> Get(int id, ContextSession session, bool includeDeleted = false)
        {
            using (var context = GetContext(session))
            {
                return await GetEntities(context, includeDeleted).Where(obj => obj.Id == id).FirstOrDefaultAsync();
            }
        }

        public virtual async Task<bool> Exists(TType obj, ContextSession session, bool includeDeleted = false)
        {
            using (var context = GetContext(session))
            {
                return await GetEntities(context, includeDeleted).Where(x => x.Id == obj.Id).CountAsync() > 0;
            }
        }

        public virtual async Task<TType> Edit(TType obj, ContextSession session)
        {
            var objectExists = await Exists(obj, session, true);
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
                context.Set<TType>().Attach(itemToDelete);
                await context.Entry(itemToDelete).ReloadAsync();
                itemToDelete.IsDeleted = true;
                context.Entry(itemToDelete).Property(obj => obj.IsDeleted).IsModified = true;
                await context.SaveChangesAsync();
            }
        }
    }
}