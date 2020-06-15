/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the �docs� folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Services.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EntityFramework
{
    public class UserPhotoRepository : BaseRepository<UserPhoto, DataContext>, IUserPhotoRepository
    {
        public override async Task<bool> Exists(UserPhoto obj, ContextSession session)
        {
            using (var context = GetContext(session))
            {
                return await context.UserPhotos.Where(x => x.Id == obj.Id).AsNoTracking().CountAsync() > 0;
            }
        }

        public override async Task<UserPhoto> Get(int id, ContextSession session)
        {
            using (var context = GetContext(session))
            {
                return await context.UserPhotos.Where(obj => obj.Id == id).AsNoTracking().FirstOrDefaultAsync();
            }
        }
    }
}
