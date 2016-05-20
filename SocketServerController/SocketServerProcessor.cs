using SocketCommon;
using SortedLogger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServerController
{
    public class SocketServerProcessor
    {
        Dictionary<string, SocketServerClientObject> clientDictionary;
        TcpListener tcpListener;
        Thread serverThread;
        ILogger logger;

        public SocketServerProcessor(ILogger logger)
        {
            this.logger = logger;
            clientDictionary = new Dictionary<string, SocketServerClientObject>();
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            tcpListener = new TcpListener(ipAddr, 8888);
            serverThread = new Thread(Run);

            startServer();           
        }

        private void startServer()
        {
           
            tcpListener.Start();
            Console.WriteLine("Start of listening");            
            serverThread.Start();
            Console.WriteLine("Start server thread");
        }

       
        private void Run()
        {
            while (true)   
            {
                Socket clientSocket = tcpListener.AcceptSocket();
                if (clientSocket.Connected)
                {
                    var client = new SocketServerClientObject(clientSocket);
                    clientDictionary.Add(client.ID, client);
                    
                    client.MessageRecieved += ManageMessage;
                    Thread.Sleep(500);//error with sending first message. sometimes client not start main thread at this moment
                    client.SendMessage("", client.ID, MessageType.CONNECT);

                }
            }
        }
        

        private void ManageMessage(Object sender, EventArgs args)
        {
            var message = ((RecievedMessageEventArgs)args).Message;
            var clnt = (SocketServerClientObject)sender;
            switch (message.Type)
            {
              
                case MessageType.DISCONNECT:
                    {
                        var id = ((SocketServerClientObject)sender).ID;
                        clientDictionary[id].Disconnect();
                        clientDictionary.Remove(id);
                    }
                    break;

                case MessageType.LOG_DATA:
                   
                    clnt.SendMessage(logger.GetLog(), clnt.ID, MessageType.LOG_DATA);
                    break;

                default:
                    {
                        logger.LogRecord(message.Text + "(" + clnt.ID + ")");
                        foreach (var client in clientDictionary.Values)
                        {                           
                            client.SendMessage(message.Text, clnt.ID);
                        }
                    }
                    break;
            }
        }

        private void LogRec(string record)
        {
            if (logger == null)
            {
                logger.LogRecord(record);
            }
        }


        ~SocketServerProcessor()
        {
            tcpListener.Stop();
            serverThread.Abort();
            foreach( var client in clientDictionary.Values)
            {
                client.Disconnect();
                client.MessageRecieved -= ManageMessage;
            }
        }

    }

    

}