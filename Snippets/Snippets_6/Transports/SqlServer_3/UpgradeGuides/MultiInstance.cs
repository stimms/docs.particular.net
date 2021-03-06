﻿namespace Snippets6.UpgradeGuides._5to6
{
    using System.Data.SqlClient;
    using NServiceBus;
    using NServiceBus.Transports.SQLServer;

    class SqlServer
    {
        void MultiInstance(EndpointConfiguration endpointConfiguration)
        {
#pragma warning disable 0618
            #region sqlserver-multiinstance-upgrade [3.0,4.0)

            endpointConfiguration.UseTransport<SqlServerTransport>()
                .EnableLagacyMultiInstanceMode(async address =>
                {
                    string connectionString = address.Equals("RemoteEndpoint") ? "SomeConnectionString" : "SomeOtherConnectionString";
                    SqlConnection connection = new SqlConnection(connectionString);

                    await connection.OpenAsync();

                    return connection;
                });

            #endregion
#pragma warning restore 0618
        }
    }

}
