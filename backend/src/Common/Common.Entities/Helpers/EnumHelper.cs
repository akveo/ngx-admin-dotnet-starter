/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Common.Entities.Helpers
{
    public class EnumHelper<TEnum> where TEnum : struct, IConvertible
    {
        public static IEnumerable<TEnum> GetValuesContains(string str)
        {
            return typeof(TEnum).IsEnum
                ? Enum.GetValues(typeof(TEnum))
                    .Cast<TEnum>()
                    .Where(s => s.ToString(CultureInfo.InvariantCulture).ToLower().Contains(str.ToLower()))
                : Enumerable.Empty<TEnum>();
        }
    }
}