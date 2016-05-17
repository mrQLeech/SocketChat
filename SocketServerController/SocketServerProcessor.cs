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
        public SocketServerProcessor()
        {
            IPHostEntry ipHost = Dns.GetHostEntry("127.0.0.1");
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            TcpListener tcpListener = new TcpListener(ipAddr, 8888);
            tcpListener.Start();
            Console.WriteLine("Start of listening");


            while (true)    // Ожидание запроса приемника
            {

                Socket clientSocket = tcpListener.AcceptSocket();
                if (clientSocket.Connected)
                {
                    // создаем объект обработчика для данного клиента

                    ServerClientProcessor hand = new ServerClientProcessor(clientSocket);
                    // создаем поток – оболочку на метод Reader
                    Thread mythread = new Thread(
                    new ThreadStart(hand.Reader));
                    // и запускаем поток
                    mythread.Start();
                }
            }
        }
        

}

}