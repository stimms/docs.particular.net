﻿namespace Snippets5.UpgradeGuides._5to6
{
    using NServiceBus;

    class Transactions
    {
        Transactions(EndpointConfiguration endpointConfiguration)
        {
            #region 2to3-enable-native-transaction

            endpointConfiguration
                .UseTransport<SqlServerTransport>()
                .Transactions(TransportTransactionMode.SendsAtomicWithReceive);

            #endregion
        }
    }
}