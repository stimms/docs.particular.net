﻿namespace Snippets6
{
    using System;
    using System.Transactions;
    using NServiceBus;
    using NServiceBus.Settings;
    using NServiceBus.Transports;

    class TransportTransactions
    {
        void Unreliable(EndpointConfiguration endpointConfiguration)
        {
            #region TransactionsDisable
            endpointConfiguration.UseTransport<MyTransport>()
                .Transactions(TransportTransactionMode.None);
            #endregion
        }

        void TransportTransactionReceiveOnly(EndpointConfiguration endpointConfiguration)
        {
            #region TransportTransactionReceiveOnly
            endpointConfiguration.UseTransport<MyTransport>()
                .Transactions(TransportTransactionMode.ReceiveOnly);
            #endregion
        }

        void TransportTransactionAtomicSendsWithReceive(EndpointConfiguration endpointConfiguration)
        {
            #region TransportTransactionAtomicSendsWithReceive
            endpointConfiguration.UseTransport<MyTransport>()
                .Transactions(TransportTransactionMode.SendsAtomicWithReceive);
            #endregion
        }

        void TransportTransactionScope(EndpointConfiguration endpointConfiguration)
        {
            #region TransportTransactionScope
            endpointConfiguration.UseTransport<MyTransport>()
                .Transactions(TransportTransactionMode.TransactionScope);
            #endregion
        }

        void TransportTransactionsWithScope(EndpointConfiguration endpointConfiguration)
        {
            #region TransactionsWrapHandlersExecutionInATransactionScope
            endpointConfiguration.UnitOfWork()
                .WrapHandlersInATransactionScope();
            #endregion
        }

        void CustomTransactionIsolationLevel(EndpointConfiguration endpointConfiguration)
        {
            #region CustomTransactionIsolationLevel
            endpointConfiguration.UseTransport<MyTransport>()
                .Transactions(TransportTransactionMode.TransactionScope)
                .TransactionScopeOptions(isolationLevel: IsolationLevel.RepeatableRead);
            #endregion
        }

        void CustomTransactionTimeout(EndpointConfiguration endpointConfiguration)
        {
            #region CustomTransactionTimeout
            endpointConfiguration.UseTransport<MyTransport>()
                .Transactions(TransportTransactionMode.TransactionScope)
                .TransactionScopeOptions(timeout: TimeSpan.FromSeconds(30));
            #endregion
        }
    }

    public class MyTransport : TransportDefinition
    {
        protected override TransportInfrastructure Initialize(SettingsHolder settings, string connectionString)
        {
            throw new NotImplementedException();
        }

        public override string ExampleConnectionStringForErrorMessage { get; }
    }

    internal static class MyTransportConfigurationExtensions
    {
        public static void TransactionScopeOptions(this TransportExtensions<MyTransport> transportExtensions, TimeSpan? timeout = null, IsolationLevel? isolationLevel = null)
        {
        }
    }
}