﻿using NServiceBus;
using NServiceBus.Settings;

public class WantToRunBeforeConfigurationIsFinalized :
    IFinalizeConfiguration
{
    public void Run(SettingsHolder settings)
    {
        Logger.WriteLine("Inside WantToRunBeforeConfigurationIsFinalized.Run");
    }
}