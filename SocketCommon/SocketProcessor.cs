using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketCommon
{
    public abstract class SocketProcessor
    {
       
        protected Socket socket;
        protected Thread thread;


        protected void ThreadProcessor() { }


        private byte[] GetResponseSocketBuffer()
        {
            var buffer = new byte[1024];
            var recieveSize = socket.Receive(buffer, 0, buffer.Length, 0);
            Array.Resize(ref buffer, recieveSize);
            return buffer;
        }

        protected void AppendMessage(MessageModel message){}


        protected virtual void OnMessageRecieved(EventArgs e){}


    }
}

