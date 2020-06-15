/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the �docs� folder for license information on type of purchased license.
*/

using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Common.WebApi
{
    public static class SerilogConfig
    {
        public static void Configure()
        {
            var logDb = ConfigurationManager.ConnectionStrings["localDb"].ConnectionString;
            const string logTable = "Logs";
            var columnOptions = new ColumnOptions
            {
                AdditionalDataColumns = new List<DataColumn>
                {
                    new DataColumn("UserId", typeof(int))
                }
            };
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.Store.Remove(StandardColumn.MessageTemplate);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(
                    connectionString: logDb,
                    tableName: logTable,
                    restrictedToMinimumLevel: LogEventLevel.Debug,
                    autoCreateSqlTable: true,
                    columnOptions:columnOptions
                ).CreateLogger();
        }
    }
}