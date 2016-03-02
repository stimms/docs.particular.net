﻿namespace Snippets5.RavenDB
{
    using System;
    using System.IO;
    using NServiceBus;
    using NServiceBus.Persistence;
    using Raven.Client.Document;
    using Raven.Client.Document.DTC;

    class ConfiguringTransactionRecoveryStorage
    {
        public void ConfiguringTransactionRecoveryStorageBasePath()
        {
            #region ConfiguringTransactionRecoveryStorageBasePath

            // Same value for all endpoints; must be writeable
            string transactionRecoveryBasePath = "%LOCALAPPDATA%"; 
            
            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.UsePersistence<RavenDBPersistence>()
                .SetTransactionRecoveryStorageBasePath(transactionRecoveryBasePath);

            #endregion
        }

        public void CustomizingDocumentStoreBeforeInit()
        {
            #region CustomizingRavenDocumentStoreBeforeInit
            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.UsePersistence<RavenDBPersistence>()
                .CustomizeDocumentStore(docStore =>
                {
                    // docStore can be customized here before NServiceBus
                    // calls docStore.Initialize();
                });
            #endregion
        }

        public void ManualDtcSettingExample()
        {
            string UrlToRavenDB = "http://localhost:8080";

            #region RavenDBManualDtcSettingExample
            // Value must uniquely identify endpoint on the machine and remain stable on process restarts
            Guid resourceManagerId = new Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx");

            string dtcRecoveryBasePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string recoveryPath = Path.Combine(dtcRecoveryBasePath, "NServiceBus.RavenDB", resourceManagerId.ToString());

            DocumentStore store = new DocumentStore
            {
                Url = UrlToRavenDB,
                ResourceManagerId = resourceManagerId,
                TransactionRecoveryStorage = new LocalDirectoryTransactionRecoveryStorage(recoveryPath)
            };
            store.Initialize();

            BusConfiguration endpointConfiguration = new BusConfiguration();
            endpointConfiguration.UsePersistence<RavenDBPersistence>()
                .SetDefaultDocumentStore(store);

            #endregion
        }
    }
}
