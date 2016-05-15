using SocketCommon;
using System;


namespace SocketClientController
{
    public class RecievedMessageEventArgs: EventArgs
    {
        public MessageModel Message { get; private set; }

        public RecievedMessageEventArgs(MessageModel message)
        {
            this.Message = message;
        }
    }
}
