using SocketCommon;
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
        public SocketServerProcessor()
        {
            clientDictionary = new Dictionary<string, SocketServerClientObject>();

            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            tcpListener = new TcpListener(ipAddr, 8888);
            tcpListener.Start();
            Console.WriteLine("Start of listening");
            serverThread = new Thread(ServerRun);
            serverThread.Start();
        }


        private void ServerRun()
        {
            while (true)   
            {
                Socket clientSocket = tcpListener.AcceptSocket();
                if (clientSocket.Connected)
                {
                    var client = new SocketServerClientObject(clientSocket);
                    clientDictionary.Add(client.ID, client);
                    client.MessageRecieved += ProcessMessage;
                }
            }
        }
        

        private void ProcessMessage(Object sender, EventArgs args)
        {
            var message = ((RecievedMessageEventArgs)args).Message;
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
                    break;

                default:
                    {
                        foreach (var client in clientDictionary.Values)
                        {
                            client.SendMessage(message.Text);
                        }
                    }
                    break;
            }
        }


        ~SocketServerProcessor()
        {
            tcpListener.Stop();
            serverThread.Abort();
            foreach( var client in clientDictionary.Values)
            {
                client.Disconnect();
                client.MessageRecieved -= ProcessMessage;
            }
        }

    }

    

}