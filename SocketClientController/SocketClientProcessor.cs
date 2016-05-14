using SocketMessageModel;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace SocketClientController
{
    public class SocketClientProcessor : IClientProcessor
    {

        public string ClientId { get; private set; }

        static Socket socket;
        static Thread thread;

        public SocketClientProcessor()
        {
            CreateConnection();
        }

        public event EventHandler MessageRecieved;

        private void CreateConnection()
        {
            thread = new Thread(ThreadProcessor);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var ip = IPAddress.Parse("127.0.0.1");
            var port = 8888;
            var endpP = new IPEndPoint(ip, port);
            socket.Connect(endpP);

            thread.Start();
        }

        public string GetClientId()
        {
            return ClientId;
        }

        public void SendMessage(string message)
        {
            throw new NotImplementedException();
        }

        public string GetMessagesLog()
        {
            throw new NotImplementedException();
        }

        public void CloseConnection()
        {
            throw new NotImplementedException();
        }


        private void ThreadProcessor()
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

        private MessageModel ParseRecieveMessage(byte[] buffer)
        {
            MessageModel model;

            using (var stream = new MemoryStream(buffer))
            {
                IFormatter formatter = new BinaryFormatter();
                model = (MessageModel)formatter.Deserialize(stream);
                stream.Close();
            }

            return model;            
        }

        private void AppendMessage(MessageModel message)
        {         
            OnMessageRecieved( new RecievedMessageEventArgs(message));
        }


        protected virtual void OnMessageRecieved(EventArgs e)
        {
            if (MessageRecieved != null)
            {
                MessageRecieved(this, e);
            }
        }

    }
}
