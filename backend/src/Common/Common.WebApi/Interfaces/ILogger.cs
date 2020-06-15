/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the �docs� folder for license information on type of purchased license.
*/

using System;

namespace Common.WebApi
{
    public interface ILogger
    {
        void LogError(string message, int userId, Exception ex = null);
        void LogInfo(string message, int userId);
    }
}