using SocketCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServerController
{
  
    public class ServerClientProcessor
    {
        Socket clientSocket; // сокет для связи с клиентом
        NetworkStream netStream; // сетевой поток
        BinaryWriter binWriter;  // поток для записи
        BinaryReader binReader;  // поток для чтения 

        public ServerClientProcessor(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
        }

        // Это метод для приема сообщений от клиента. Именно на 
        // него мы и будем «навешивать» отдельный поток в классе 
        // сервера
        public void Reader()
        {
            // создаем потоки для чтения-записи 
            netStream = new NetworkStream(clientSocket);
            binWriter = new BinaryWriter(netStream);
            binReader = new BinaryReader(netStream);
            var strW = new StreamWriter(netStream);
            // цикл приема сообщений от клиента
            while (true)
            {
                var buff = new byte[1024];
                // здесь возникает пауза, пока клиент 
                // не пришлет очередное сообщение 
                var message = binReader.Read(buff, 0, 1024);
                var mess = ModelConverter.BinaryToMessageModel(buff);
                // полученное сообщение выводим на экран
                Console.WriteLine("Received:" + mess.Text);
                // и отправляем обратно в качестве «эха»
                strW.Write(ModelConverter.MessageModelToBinary(mess));
                // при получении сообщения о выходе клиента
                if (message.Equals("quit"))
                {
                    // разрываем с ним соединение
                    Console.WriteLine("Client disconnected");
                    binWriter.Close();
                    netStream.Close();
                    break;
                }
            }
        }

    }
}


