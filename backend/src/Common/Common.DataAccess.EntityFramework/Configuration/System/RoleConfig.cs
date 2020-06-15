/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;

namespace Common.DataAccess.EntityFramework.Configuration.System
{
    public class RoleConfig : BaseEntityConfig<Role>
    {
        public RoleConfig() : base("Roles")
        {
            Property(obj => obj.Name).HasColumnName("Name");

            HasMany(r => r.UserRoles).WithRequired().HasForeignKey(ur => ur.RoleId);
        }
    }
}
