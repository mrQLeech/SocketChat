using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SocketCommon
{
    public static class ModelConverter
    {
        public static byte[] MessageModelToBinary(MessageModel model, uint bufferSyze = 1024)
        {
            var res = new byte[bufferSyze];

            using(var stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, model);
                res = stream.ToArray();
                stream.Close();
            }

            return res;
        }


        public static MessageModel BinaryToMessageModel(byte[] buffer)
        {
            MessageModel res;

            using (var stream = new MemoryStream(buffer))
            {
                IFormatter formatter = new BinaryFormatter();
                res = (MessageModel)formatter.Deserialize(stream);
                stream.Close();
            }

            return res;
        }

    }
}
