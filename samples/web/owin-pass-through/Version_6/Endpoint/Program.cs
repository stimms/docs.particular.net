﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using NServiceBus;
using NServiceBus.Logging;
using Owin;

static class Program
{

    static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        Console.Title = "Samples.OwinPassThrough";
        LogManager.Use<DefaultFactory>()
            .Level(LogLevel.Info);

        #region startbus

        EndpointConfiguration endpointConfiguration = new EndpointConfiguration("Samples.OwinPassThrough");
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.UseSerialization<JsonSerializer>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        endpointConfiguration.EnableInstallers();

        IEndpointInstance endpoint = await Endpoint.Start(endpointConfiguration);
        try
        {
            using (StartOwinHost(endpoint))
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
            await endpoint.Stop();

            #endregion
        }
        finally
        {
            await endpoint.Stop();
        }
    }

    #region startowin

    static IDisposable StartOwinHost(IEndpointInstance endpointInstance)
    {
        string baseUrl = "http://localhost:12345/";
        StartOptions startOptions = new StartOptions(baseUrl)
        {
            ServerFactory = "Microsoft.Owin.Host.HttpListener",
        };

        return WebApp.Start(startOptions, builder =>
        {
            builder.UseCors(CorsOptions.AllowAll);
            MapToBus(builder, endpointInstance);
            MapToMsmq(builder);
        });
    }

    static void MapToBus(IAppBuilder builder, IEndpointInstance endpointInstance)
    {
        OwinToBus owinToBus = new OwinToBus(endpointInstance);
        builder.Map("/to-bus", app => { app.Use(owinToBus.Middleware()); });
    }

    static void MapToMsmq(IAppBuilder builder)
    {
        string queue = Environment.MachineName + @"\private$\Samples.OwinPassThrough";
        OwinToMsmq owinToMsmq = new OwinToMsmq(queue);
        builder.Map("/to-msmq", app => { app.Use(owinToMsmq.Middleware()); });
    }

    #endregion

}