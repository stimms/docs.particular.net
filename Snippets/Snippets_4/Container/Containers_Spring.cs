﻿namespace Snippets4.Container
{
    using NServiceBus;
    using Spring.Context.Support;

    class Containers_Spring
    {
        void Simple(Configure configure)
        {
            #region Spring

            configure.SpringFrameworkBuilder();

            #endregion
        }

        void Existing(Configure configure)
        {
            #region Spring_Existing

            GenericApplicationContext applicationContext = new GenericApplicationContext();
            applicationContext.ObjectFactory.RegisterSingleton("MyService", new MyService());
            configure.SpringFrameworkBuilder(applicationContext);
            #endregion
        }

    }
}