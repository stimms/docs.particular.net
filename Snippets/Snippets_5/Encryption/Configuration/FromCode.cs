﻿namespace Snippets5.Encryption.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NServiceBus;

    class FromCode
    {
        FromCode(BusConfiguration busConfiguration)
        {
#pragma warning disable 618
            #region EncryptionFromCode

            string encryptionKeyIdentifier = "2015-10";
            byte[] encryptionKey = Convert.FromBase64String("gdDbqRpqdRbTs3mhdZh9qCaDaxJXl+e6");
            List<byte[]> expiredKeys = new List<byte[]>
            {
                Encoding.ASCII.GetBytes("abDbqRpQdRbTs3mhdZh9qCaDaxJXl+e6"),
                Encoding.ASCII.GetBytes("cdDbqRpQdRbTs3mhdZh9qCaDaxJXl+e6")
            };
            busConfiguration.RijndaelEncryptionService(encryptionKeyIdentifier, encryptionKey, expiredKeys);

            #endregion
#pragma warning restore 618
        }
    }
}
