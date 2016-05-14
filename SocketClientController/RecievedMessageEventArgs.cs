using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketClientController
{
    public class RecievedMessageEventArgs: EventArgs
    {
        public SocketMessageModel.MessageModel Message { get; private set; }

        public RecievedMessageEventArgs(SocketMessageModel.MessageModel message)
        {
            this.Message = message;
        }
    }
}
