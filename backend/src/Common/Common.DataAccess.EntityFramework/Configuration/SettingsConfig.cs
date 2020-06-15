/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DataAccess.EntityFramework.Configuration.System;
using Common.Entities;

namespace Common.DataAccess.EntityFramework.Configuration
{
    public class SettingsConfig : BaseEntityConfig<Settings>
    {
        public SettingsConfig() : base("Settings")
        {
            Property(obj => obj.ThemeName).HasColumnName("ThemeName").IsRequired();
        }
    }
}