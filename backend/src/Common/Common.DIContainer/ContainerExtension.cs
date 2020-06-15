/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the �docs� folder for license information on type of purchased license.
*/

using Common.Services;
using Common.Services.Infrastructure;
using Common.DataAccess.EntityFramework;
using Common.Entities;
using Unity;
using Unity.Extension;

namespace Common.DIContainer
{
    public class ContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IUserPhotoRepository, UserPhotoRepository>();
            Container.RegisterType<ISettingsRepository, SettingsRepository>();
            Container.RegisterType<ISettingsService, SettingsService>();

            Container.RegisterType<IUserService, UserService<User>>();
            Container.RegisterType<IUserRepository<User>, UserRepository>();
            Container.RegisterType<IIdentityUserRepository<User>, IdentityUserRepository>();
            Container.RegisterType<IRoleRepository<Role>, RoleRepository>();
            Container.RegisterType<IUserRoleRepository<UserRole>, UserRoleRepository>();
            Container.RegisterType<IUserClaimRepository<UserClaim>, UserClaimRepository>();
        }
    }
}
