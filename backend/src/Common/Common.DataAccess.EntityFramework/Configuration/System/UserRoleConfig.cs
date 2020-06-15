/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Common.DataAccess.EntityFramework.Configuration.System
{
    public class UserRoleConfig : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfig()
        {
            ToTable("UserRoles");
            Property(obj => obj.RoleId).HasColumnName("RoleId").IsRequired();
            Property(obj => obj.UserId).HasColumnName("UserId").IsRequired();

            HasKey(key => new { key.UserId, key.RoleId });
        }
    }
}
