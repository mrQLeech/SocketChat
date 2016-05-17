
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
                thread = new Thread(ThreadProcessor);

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
                AppendMessage(new MessageModel(MessageType.MESSAGE, "error", "connection failed."  + ex.Message));
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


        public string RequestMessageLog()
        {
            return "";
        }


        public void CloseConnection()
        {
            thread.Abort();
            socket.Close();
        }


        private void Send(MessageModel message)
        {
            var buffer = ParseSendedMessage(message);
            socket.Send(buffer);
        }


        private  void ThreadProcessor()
        {
            while (true)
            {
                Thread.Sleep(333);

                var buffer = GetResponseSocketBuffer();
                var model = ParseRecieveMessage(buffer);
                AppendMessage(model);                 
            }
        }


        private byte[] GetResponseSocketBuffer()
        {
            var buffer = new byte[1024];
            var recieveSize = socket.Receive(buffer, 0, buffer.Length, 0);
            Array.Resize(ref buffer, recieveSize);
            return buffer;
        }


        protected  MessageModel ParseRecieveMessage(byte[] buffer)
        {
            MessageModel message;

            using (var stream = new MemoryStream(buffer))
            {
                IFormatter formatter = new BinaryFormatter();
              
                message = (MessageModel)formatter.Deserialize(stream);
                stream.Close();
            }

            return message;
        }


        protected  byte[] ParseSendedMessage(MessageModel message)
        {
            byte[] buffer = new byte[1024];

            using (var stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, message);
                buffer = stream.ToArray();
                stream.Close();
            }

            return buffer;
        }


        protected void AppendMessage(MessageModel message)
        {
            OnMessageRecieved(new RecievedMessageEventArgs(message));
        }


        protected virtual void OnMessageRecieved(EventArgs e)
        {
            if (MessageRecieved != null)
            {
                MessageRecieved(this, e);
            }
        }


        ~SocketClientProcessor()
        {
            CloseConnection();
        }

    }
}
