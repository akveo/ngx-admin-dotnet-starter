/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;

namespace Common.DataAccess.EntityFramework.Configuration.System
{
    public class UserClaimConfig : BaseEntityConfig<UserClaim>
    {
        public UserClaimConfig() : base("UserClaims")
        {
            Property(obj => obj.ClaimType).HasColumnName("ClaimType").IsRequired();
            Property(obj => obj.ClaimValue).HasColumnName("ClaimValue").IsRequired();
        }
    }
}
