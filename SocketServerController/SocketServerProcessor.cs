using SocketCommon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServerController
{
    public class SocketServerProcessor : SocketProcessor
    {
        private Hashtable clientsList = new Hashtable();
        private TcpListener serverSocket;
        private TcpClient clientSocket;
        private int counter;

        public event EventHandler MessageRecieved;
      

        public void SendMessage(string msg, string uName, bool flag)
        {
            foreach (DictionaryEntry Item in clientsList)
            {
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient)Item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                Byte[] broadcastBytes = null;

                if (flag == true)
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(uName + " says : " + msg);
                }
                else
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(msg);
                }

                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
            }
        }

        public void CreateConnection()
        {
            serverSocket = new TcpListener(8888);
            clientSocket = default(TcpClient);
            counter = 0;

            serverSocket.Start();
            Console.WriteLine("Chat Server Started ....");
            counter = 0;

            thread = new Thread(ThreadProcessor);
            thread.Start();

        }

        protected void ThreadProcessor()
        {
            int requestCount = 0;
            byte[] bytesFrom = new byte[1024];
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string rCount = null;
            requestCount = 0;

            while ((true))
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine("From client - " + clNo + " : " + dataFromClient);
                    rCount = Convert.ToString(requestCount);

                    SendMessage(dataFromClient, clNo, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public string RequestMessageLog()
        {
            throw new NotImplementedException();
        }

        public void CloseConnection()
        {
            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine("exit");
            Console.ReadLine();
        }

        public string GetClientId()
        {
            throw new NotImplementedException();
        }
    }//end Main class
     /*

     public class handleClinet
     {
         TcpClient clientSocket;
         string clNo;
         Hashtable clientsList;

         public void startClient(TcpClient inClientSocket, string clineNo, Hashtable cList)
         {
             this.clientSocket = inClientSocket;
             this.clNo = clineNo;
             this.clientsList = cList;
             Thread ctThread = new Thread(doChat);
             ctThread.Start();
         }

         private void doChat()
         {
             int requestCount = 0;
             byte[] bytesFrom = new byte[10025];
             string dataFromClient = null;
             Byte[] sendBytes = null;
             string serverResponse = null;
             string rCount = null;
             requestCount = 0;

             while ((true))
             {
                 try
                 {
                     requestCount = requestCount + 1;
                     NetworkStream networkStream = clientSocket.GetStream();
                     networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                     dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                     dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                     Console.WriteLine("From client - " + clNo + " : " + dataFromClient);
                     rCount = Convert.ToString(requestCount);

                     SocketServerProcessor.broadcast(dataFromClient, clNo, true);
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine(ex.ToString());
                 }
             }
         }*/
}