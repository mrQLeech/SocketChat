using SocketCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServerController
{
  
    public class SocketServerClientObject
    {
        public string ID { get; private set; }

        private Socket clientSocket;
        private NetworkStream netStream;
        private BinaryWriter binWriter;
        private BinaryReader binReader;

        private Thread listenerThread;

        private int buffSize = 1024;

        public event EventHandler MessageRecieved;       


        public SocketServerClientObject(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            ID = generateID();

            listenerThread = new Thread(Listen);
            listenerThread.Start();
        }


        public string generateID()
        {
            return Guid.NewGuid().ToString("N");
        }


        private void Listen()
        {
           
            netStream = new NetworkStream(clientSocket);
            binWriter = new BinaryWriter(netStream);
            binReader = new BinaryReader(netStream);
            var strW = new StreamWriter(netStream);
            
            while (true)
            {
                var buff = new byte[buffSize];                
                var message = binReader.Read(buff, 0, buffSize);


                var mess = ModelConverter.BinaryToMessageModel(buff);
                MessageAppended(mess);
               
            }
        }

        protected void MessageAppended(MessageModel message)
        {
            OnMessageRecieved( new RecievedMessageEventArgs(message));
        }


        protected virtual void OnMessageRecieved( EventArgs e)
        {
            if (MessageRecieved != null)
            {
                MessageRecieved(this, e);
            }
        }


        public void Disconnect()
        {
            binWriter.Close();
            netStream.Close();
        }

        
        public void SendMessage(string message, MessageType type = MessageType.MESSAGE)
        {
            var model = new MessageModel(type, ID, message);
            var buff = ModelConverter.MessageModelToBinary(model);

            binWriter.Write(buff, 0, buff.Length);
        }


        ~SocketServerClientObject()
        {
            Disconnect();
            listenerThread.Abort();
        }

    }
}


