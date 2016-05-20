
using SocketCommon;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace SocketClientController
{
    public class SocketClientProcessor : SocketProcessor, ISocketProcessor
    {
      
        public string ClientId { get; private set; }

     

        public event EventHandler MessageRecieved;


        public SocketClientProcessor()
        {           
        }


        public void CreateConnection()
        {
            try
            {
                thread = new Thread(ThreadProcess);

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                var ip = IPAddress.Parse("127.0.0.1");
                var port = 8888;
                var endpP = new IPEndPoint(ip, port);
                socket.Connect(endpP);
                if (socket.Connected)
                {
                    thread.Start();
                }
            }catch(Exception ex)
            {
                ProcessMessage(new MessageModel(MessageType.MESSAGE, "error", "connection failed."  + ex.Message));
            }           
        }


        public string GetClientId()
        {
            return ClientId;
        }


        public void SendMessage(string message)
        {
            MessageModel model = new MessageModel(MessageType.MESSAGE, GetClientId(), message);
            Send(model);
        }


        public void RequestMessageLog()
        {
            MessageModel message = new MessageModel(MessageType.LOG_DATA, GetClientId(), "");
            Send(message);
        }


        public void CloseConnection()
        {
            thread.Abort();
            thread = null;
            MessageModel message = new MessageModel(MessageType.DISCONNECT, GetClientId(), "...");
            Send(message);

            socket.Close();
            socket = null;
        }


        private void Send(MessageModel message)
        {
            if (!socket.Connected) return;
            var buffer = ModelConverter.MessageModelToBinary(message);
            socket.Send(buffer);
        }


        private  void ThreadProcess()
        {
            while (true)
            {
                var buffer = GetResponseSocketBuffer();
                var model = ModelConverter.BinaryToMessageModel(buffer);
                ProcessMessage(model);                 
            }
        }


        private byte[] GetResponseSocketBuffer()
        {
            var buffer = new byte[1024];
            var recieveSize = socket.Receive(buffer, 0, buffer.Length, 0);
            Array.Resize(ref buffer, recieveSize);
            return buffer;
        }
        

        protected void ProcessMessage(MessageModel message)
        {
            if (message.Type == MessageType.CONNECT)
            {
                this.ClientId = message.SenderName;
            }
            OnMessageRecieved(new RecievedMessageEventArgs(message));
        }


        protected virtual void OnMessageRecieved(EventArgs e)
        {
            if (MessageRecieved != null)
            {
                MessageRecieved(this, e);
            }
        }


        protected bool Connected()
        {
            if (socket != null && socket.Connected) return true;

            return false;
        }


        ~SocketClientProcessor()
        {
            if (Connected())
            {
                CloseConnection();
            }
            
        }

    }
}
