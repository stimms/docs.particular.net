﻿using System;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NServiceBus;
using NServiceBus.Transports.SQLServer;
using NServiceBus.Persistence;

class Program
{
    static void Main()
    {
        Console.Title = "Samples.SQLNHibernateOutboxEF.Receiver";
        using (ReceiverDataContext ctx = new ReceiverDataContext())
        {
            ctx.Database.Initialize(true);
        }

        Configuration hibernateConfig = new Configuration();
        hibernateConfig.DataBaseIntegration(x =>
        {
            x.ConnectionStringName = "NServiceBus/Persistence";
            x.Dialect<MsSql2012Dialect>();
        });

        hibernateConfig.SetProperty("default_schema", "receiver");

        BusConfiguration busConfiguration = new BusConfiguration();
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.EndpointName("Samples.SQLNHibernateOutboxEF.Receiver");

        #region ReceiverConfiguration

        busConfiguration
            .UseTransport<SqlServerTransport>()
            .UseSpecificConnectionInformation(
                EndpointConnectionInfo.For("Samples.SQLNHibernateOutboxEF.Sender").UseSchema("sender"))
            .DefaultSchema("receiver");

        busConfiguration
            .UsePersistence<NHibernatePersistence>()
            .RegisterManagedSessionInTheContainer();

        busConfiguration.EnableOutbox();

        #endregion

        using (Bus.Create(busConfiguration).Start())
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
