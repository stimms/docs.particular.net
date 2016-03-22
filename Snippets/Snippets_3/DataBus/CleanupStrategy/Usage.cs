﻿namespace Snippets3.DataBus.CleanupStrategy
{
    using System;
    using System.IO;
    using Snippets3.DataBus.DataBusProperty;

    public class Usage
    {
        #region FileLocationForDatabusFiles
        public void Handle(MessageWithLargePayload message)
        {
            string filename = Path.Combine(@"c:\database_files\", message.LargeBlob.Key);
            Console.WriteLine(filename);
        }
        #endregion
    }
}