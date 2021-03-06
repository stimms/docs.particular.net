﻿namespace Snippets6.Headers
{
    using System.Threading.Tasks;
    using NServiceBus;

    #region header-outgoing-handler

    public class WriteHandler : IHandleMessages<MyMessage>
    {
        public async Task Handle(MyMessage message, IMessageHandlerContext context)
        {
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetHeader("MyCustomHeader", "My custom value");
            await context.Send(new SomeOtherMessage(), sendOptions);

            ReplyOptions replyOptions = new ReplyOptions();
            replyOptions.SetHeader("MyCustomHeader", "My custom value");
            await context.Reply(new SomeOtherMessage(), replyOptions);

            PublishOptions publishOptions = new PublishOptions();
            publishOptions.SetHeader("MyCustomHeader", "My custom value");
            await context.Publish(new SomeOtherMessage(), publishOptions);
        }
    }

    #endregion


}