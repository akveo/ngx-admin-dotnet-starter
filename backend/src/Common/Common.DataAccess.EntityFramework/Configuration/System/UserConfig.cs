/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;

namespace Common.DataAccess.EntityFramework.Configuration.System
{
    public class UserConfig : BaseEntityConfig<User>
    {
        public UserConfig() : base("Users")
        {
            Property(obj => obj.FirstName).HasColumnName("FirstName").IsOptional();
            Property(obj => obj.LastName).HasColumnName("LastName").IsOptional();
            Property(obj => obj.Login).HasColumnName("Login").IsRequired();
            Property(obj => obj.Password).HasColumnName("Password").IsOptional();
            Property(obj => obj.Email).HasColumnName("Email").IsRequired();
            Property(obj => obj.Age).HasColumnName("Age").IsOptional();
            Property(obj => obj.IsDeleted).HasColumnName("IsDeleted").IsRequired();

            Property(obj => obj.AddressCity).HasColumnName("City").IsOptional();
            Property(obj => obj.AddressStreet).HasColumnName("Street").IsOptional();
            Property(obj => obj.AddressZipCode).HasColumnName("ZipCode").IsOptional();
            Property(obj => obj.AddressLat).HasColumnName("Lat").IsOptional();
            Property(obj => obj.AddressLng).HasColumnName("Lng").IsOptional();

            HasOptional(obj => obj.Photo).WithRequired(obj => obj.User).WillCascadeOnDelete(true);
            HasOptional(obj => obj.Settings).WithRequired(obj => obj.User).WillCascadeOnDelete(true);

            HasMany(obj => obj.UserRoles).WithRequired().HasForeignKey(obj => obj.UserId);
            HasMany(obj => obj.Claims).WithRequired().HasForeignKey(obj => obj.UserId);


        }


    }
}
