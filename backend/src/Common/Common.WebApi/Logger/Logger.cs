/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the �docs� folder for license information on type of purchased license.
*/

using Serilog;
using System;
using ILogger = Common.WebApi.ILogger;

namespace Common.WebApi
{
    public class Logger : ILogger
    {
        public void LogError(string message, int userId, Exception ex = null)
        {
            Log.Logger.ForContext("UserId", userId).Error(ex, message);
        }

        public void LogInfo(string message, int userId)
        {
            Log.Logger.ForContext("UserId", userId).Information(message);
        }
    }
}